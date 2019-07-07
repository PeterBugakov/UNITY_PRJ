using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject cube;
    public GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.FindGameObjectWithTag("Cube");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
      /*  if (Input.GetKey(KeyCode.UpArrow))
        {
            float step = 1f * Time.deltaTime;
            MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position, MainCamera.transform., step);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float step = 1f * Time.deltaTime;
            MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position, -MainCamera.transform.forward, step);
        }
    */}
}