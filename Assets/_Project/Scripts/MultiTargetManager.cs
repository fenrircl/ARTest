using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiTargetManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager ARTrackedImageManager;
    [SerializeField] private GameObject[] ARPrefabsToPlace;
    private Dictionary<string, GameObject> ARPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> prefabState = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ARPrefab in ARPrefabsToPlace)
        {
            GameObject newARinstance = Instantiate(ARPrefab, Vector3.zero, Quaternion.identity);
            newARinstance.name = ARPrefab.name;
            ARPrefabs.Add(newARinstance.name, newARinstance);
            newARinstance.SetActive(false);
            prefabState.Add(newARinstance.name, false);
        }
    }

    private void OnEnable()
    {
        ARTrackedImageManager.trackedImagesChanged += ImageFound;
    }
        private void OnDisable()
    {
        ARTrackedImageManager.trackedImagesChanged -= ImageFound;
    }

    private void ImageFound(ARTrackedImagesChangedEventArgs eventData)
    {
        foreach (var trackedImage in eventData.added)
        {
            ShowARObject(trackedImage);
        }
        foreach (var trackedImage in eventData.updated)
        {
            if(trackedImage.trackingState == TrackingState.Tracking)
            {
                ShowARObject(trackedImage);
            }
            else if(trackedImage.trackingState == TrackingState.Limited)
            {
                HideARObject(trackedImage);
            }
        }

    }

    private void ShowARObject(ARTrackedImage trackedImage)
    {
        print(trackedImage.referenceImage.name);
        bool isObjectActivated = prefabState[trackedImage.referenceImage.name];
        if(!isObjectActivated)
        {
            GameObject ARObject = ARPrefabs[trackedImage.referenceImage.name];
            ARObject.transform.position = trackedImage.transform.position;
            ARObject.SetActive(true);
            prefabState[trackedImage.referenceImage.name] = true;
        }
        else
        {
            GameObject ARObject = ARPrefabs[trackedImage.referenceImage.name];
            ARObject.transform.position = trackedImage.transform.position;
        }
    }

    private void HideARObject(ARTrackedImage trackedImage)
    {
        bool isObjectActivated = prefabState[trackedImage.referenceImage.name];
        if(!isObjectActivated)
        {
            GameObject ARObject = ARPrefabs[trackedImage.referenceImage.name];
            ARObject.SetActive(false);
            prefabState[trackedImage.referenceImage.name] = false;
        }
    }
    
}
