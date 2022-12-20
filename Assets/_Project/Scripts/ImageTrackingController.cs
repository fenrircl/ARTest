using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    // Reference to AR tracked image manager component
    [SerializeField] private ARTrackedImageManager _trackedImagesManager = null;
    private Dictionary<string, GameObject> instances;


    // List of prefabs to instantiate - these should be named the same
    // as their corresponding 2D images in the reference image library 
    public GameObject[] ArPrefabs;


    //private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    //  foreach(var trackedImage in eventArgs.added){
    // Get the name of the reference image
    //   var imageName = trackedImage.referenceImage.name;
    //   foreach (var curPrefab in ArPrefabs) {
    // Check whether this prefab matches the tracked image name, and that
    // the prefab hasn't already been created
    //  if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
    //   && !_instantiatedPrefabs.ContainsKey(imageName)){
    // Instantiate the prefab, parenting it to the ARTrackedImage
    //  var newPrefab = Instantiate(curPrefab, trackedImage.transform);
     void Awake()
    {
       // _trackedImagesManager = GetComponent<ARTrackedImageManager>();
        instances = new Dictionary<string, GameObject>();

    }    

    void Start()
    {
        foreach (var prefab in ArPrefabs)
        {
            var defaultPosition = Vector3.zero;
            var newPrefab = Instantiate(prefab, defaultPosition, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            instances.Add(newPrefab.name, newPrefab);
        }
    }
    private void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log(eventArgs);
        // image detection
        foreach (var trackedImage in eventArgs.added)
        {
            //Debug.Log($"referenceImage.name: {trackedImage.referenceImage.name}");
            UpdateInstance(trackedImage);
        }
        
        // object activation / deactivation
        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log($"referenceImage.name: {trackedImage.referenceImage.name}");

            UpdateInstance(trackedImage);
        }
        
        // object deletion
        foreach (var trackedImage in eventArgs.removed)
        {
            var imageName = trackedImage.referenceImage.name;
            if(!instances.ContainsKey(imageName)) continue;
            instances[imageName].SetActive(false);
        }
    }

    public void UpdateInstance(ARTrackedImage trackedImage, bool isActive = true)
    {
        Debug.Log(trackedImage);
        var imageName = trackedImage.name;
        Debug.Log(imageName);
        if (!instances.ContainsKey(imageName)) return;
        print("pasa");
        var instance = instances[imageName];
        instance.SetActive(true);
        instance.transform.position = trackedImage.transform.position;
    }
    

}