using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager = null;
    public GameObject[] arPrefabs;
    private Dictionary<string, GameObject> instances;

    private void Awake()
    {
        instances = new Dictionary<string, GameObject>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // image detection
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            foreach (var prefab in arPrefabs)
            {
                if (string.Compare(prefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
                    && !instances.ContainsKey(imageName))
                {
                    var obj = Instantiate(prefab, trackedImage.transform);
                    instances.Add(imageName, obj);
                }
            }
        }
        
        // object activation / deactivation
        foreach (var trackedImage in eventArgs.updated)
        {
            var imageName = trackedImage.referenceImage.name;
            var trackingState = trackedImage.trackingState == TrackingState.Tracking;
            instances[imageName].SetActive(trackingState);
            instances[imageName].transform.position = trackedImage.transform.position;
        }
        
        // object deletion
        foreach (var trackedImage in eventArgs.removed)
        {
            var imageName = trackedImage.referenceImage.name;
            Destroy(instances[imageName]);
            instances.Remove(imageName);
        }
    }
}
