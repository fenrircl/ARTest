using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectCreationComponent : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager = null;
    [SerializeField] private GameObject[] prefab = null;
    public static int numSpawned = 0;
    private GameObject _instance;
    private Vector2 _touchPosition;
    
    private List<ARRaycastHit> _hits;

        private void Awake()
    {
        _hits = new List<ARRaycastHit>();
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
     void Update()
    {
        if (IsTapping(out _touchPosition))
        {
            if (raycastManager.Raycast(_touchPosition, _hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = _hits[0].pose;
                // _instance.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                // _instance.SetActive(true);
                print(hitPose.up);
                //if(hitPose.up.Equals(Vector3.up))
                SpawnRandomObject(hitPose);

            }
        }
    }
    
        private bool IsTapping(out Vector2 tapPosition)
    {
#if UNITY_EDITOR
        return IsMousePosition(out tapPosition);
#else
        return IsTouchPosition(out tapPosition);
#endif
    }

    private bool IsMousePosition(out Vector2 mousePosition)
    {
        if (Input.GetMouseButtonUp(0))
        {
            mousePosition = Input.mousePosition;
            return true;
        }
        mousePosition = default;
        return false;
    }

    private bool IsTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }



    void SpawnRandomObject(Pose hits) 
     {    
         //spawns item in array position between 0 and 100
        int whichItem = Random.Range (0, prefab.Length);     
        print(whichItem);
        GameObject myObj = Instantiate (prefab [whichItem]) as GameObject; 
        numSpawned++;
        myObj.transform.SetPositionAndRotation(hits.position, hits.rotation);
         //
     }
}
