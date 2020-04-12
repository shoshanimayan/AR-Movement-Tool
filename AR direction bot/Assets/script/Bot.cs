using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject sign;
    private bool on = false;
    private GameObject[] GoTo;
    int count = 0;
    int total = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (on) {

            if (count < total)
            {
                transform.position = Vector3.MoveTowards(transform.position, GoTo[count].transform.position, 1f * Time.deltaTime);
                if (Vector3.Distance(transform.position, GoTo[count].transform.position) <0.001f)
                {
                    Destroy(GoTo[count]);
                    count++;
                }
            }
            else {
                foreach (GameObject spot in spawn.spots) { Destroy(spot); }
                on = false;
                spawn.spots = new List<GameObject>();
                count = 0;
            }


        } 
    }

    public void begin() {
        total= spawn.spots.ToArray().Length;
        on = true;
        GoTo = spawn.spots.ToArray();
        

    }
}
