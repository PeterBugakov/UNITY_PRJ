using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routes : MonoBehaviour
{
    public GameObject f_route;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Route_visible()
    {
        if (f_route.GetComponent<Renderer>().enabled)
        {
            f_route.GetComponent<Renderer>().enabled = false;
            print("Click1");
        }
        else
        {
            f_route.GetComponent<Renderer>().enabled = true;
            print("Click2");
        }
    } 
}
