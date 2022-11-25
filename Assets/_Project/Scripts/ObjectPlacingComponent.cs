using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager), typeof(ARRaycastManager))]
public class ObjectPlacingComponent : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager = null;
    [SerializeField] private GameObject prefab = null;
    private GameObject _instance;
    private Vector2 _touchPosition;
    private List<ARRaycastHit> _hits;

    private void Awake()
    {
        _hits = new List<ARRaycastHit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        _instance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTapping(out _touchPosition))
        {
            if (raycastManager.Raycast(_touchPosition, _hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = _hits[0].pose;
                _instance.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                _instance.SetActive(true);
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
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            return true;
        }
        mousePosition = default;
        return false;
    }

    private bool IsTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    
    
    // TODO: Crear un script nuevo para agregar elementos en la escena
    // Normal: Agregar un objeto en la escena con click/tap sobre la superficie
    // Avanzado: Solo agregar objeto si la superficie es horizontal
}
