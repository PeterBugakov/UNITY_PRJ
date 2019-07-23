using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfotabTap : MonoBehaviour
{
    public GameObject infotxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                //Debug.Log("Something Hit");
                //if (raycastHit.collider.name == "Soccer")
                //{
                //    Debug.Log("Soccer Ball clicked");
                //}

                //OR with Tag

                if (raycastHit.collider.CompareTag("info_tab"))
                {
                    //infotxt.GetComponent<Text>().text = "Tapped";
                    GameObject touchedObject = raycastHit.transform.gameObject;
                    Transform[] ts = touchedObject.transform.GetComponentsInChildren<Transform>(true);
                    foreach (Transform t in ts)
                    {
                        if (t.gameObject.name == "Caption")
                        {
                            infotxt.GetComponent<Text>().text = t.gameObject.GetComponent<Text>().text;
                        }
                    }
                        Debug.Log("Soccer Ball clicked");
                }
            }
        }
    }
}
