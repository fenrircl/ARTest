using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager = null;
    [SerializeField] private GameObject[] prefabs;
    private Dictionary<string, GameObject> instances;

    private void Awake()
    {
        instances = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
#if UNITY_EDITOR
        DebugInitialValues();
#endif
        
        foreach (var prefab in prefabs)
        {
            var defaultPosition = Vector3.zero;
            var newPrefab = Instantiate(prefab, defaultPosition, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            instances.Add(newPrefab.name, newPrefab);
        }
    }

    private void DebugInitialValues()
    {
        Debug.Log($"referenceLibrary.Count: {trackedImageManager.referenceLibrary.count}");
        var size = trackedImageManager.referenceLibrary.count;
        for (var i = 0; i < size; i++)
        {
            var trackedImage = trackedImageManager.referenceLibrary[i];
            var trackedImageName = trackedImage.name;
            Debug.Log($"\ttrackedImage: {trackedImageName}");
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // image detection
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log($"referenceImage.name: {trackedImage.referenceImage.name}");
            // UpdateInstance(trackedImage);
        }
        
        // object activation / deactivation
        foreach (var trackedImage in eventArgs.updated)
        {
            // UpdateInstance(trackedImage);
        }
        
        // object deletion
        foreach (var trackedImage in eventArgs.removed)
        {
            // var imageName = trackedImage.referenceImage.name;
            // if(!instances.ContainsKey(imageName)) continue;
            // instances[imageName].SetActive(false);
        }
    }

    public void UpdateInstance(ARTrackedImage trackedImage, bool isActive = true)
    {
        Debug.Log(trackedImage);
        // var imageName = trackedImage.name;
        // if (!instances.ContainsKey(imageName)) return;
        //
        // var instance = instances[imageName];
        // instance.SetActive(true);
        // instance.transform.position = trackedImage.transform.position;
    }
    
}
