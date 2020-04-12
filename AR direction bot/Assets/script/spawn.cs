using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
[RequireComponent(typeof(ARRaycastManager))]
public class spawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefab2;

    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;
    private bool spawned = false;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            if (!TryGetPosition(out Vector2 touchPosition))
            {
                return;
            }
            if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && !spawned)
            {
                var hitPose = hits[0].pose;
                Instantiate(prefab, hitPose.position, hitPose.rotation);
                spawned = true;

            }
        }
        else {


        }


    }
}