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
    public static List<GameObject> spots;
    private  GameObject bot;
    private GameObject sign;
    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;
    private bool spawned = false;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Awake()
    {
        spots = new List<GameObject>();
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase != TouchPhase.Began) {
                touchPosition = default;
                return false;
            }
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
                bot = Instantiate(prefab, hitPose.position, hitPose.rotation);
                sign = bot.transform.GetChild(1).gameObject;
                spawned = true;

            }
        }
        else {
            if (!TryGetPosition(out Vector2 touchPosition))
            {
                return;
            }
            Debug.Log("markers");

            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hitObject;
            //if (spots.Count < 6) {
                if (Physics.Raycast(ray, out hitObject))
                {
                    GameObject placementObject = hitObject.transform.gameObject;

                    if (placementObject == bot) { Debug.Log("bot"); return; }
                    else if (placementObject == bot.GetComponent<Bot>().sign) { Debug.Log("sign"); Begin(); }
                    else
                    {
                        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && spawned)
                        {
                            var hitPose = hits[0].pose;
                            spots.Add(Instantiate(prefab2, hitPose.position, hitPose.rotation));
                            spawned = true;

                        }

                    }
                    }
                else
                {
                        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && spawned)
                        {
                            var hitPose = hits[0].pose;
                            spots.Add(Instantiate(prefab2, hitPose.position, hitPose.rotation));
                            spawned = true;

                        }
                }

            //}
            //else { Begin(); }

        }


    }

    public void Begin() {

        bot.GetComponent<Bot>().begin();

    }

}
