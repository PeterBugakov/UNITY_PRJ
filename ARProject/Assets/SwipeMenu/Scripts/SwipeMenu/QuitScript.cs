using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
       /* if (Application.platform == RuntimePlatform.Android || 
            Application.platform == RuntimePlatform.WindowsPlayer || 
            Application.platform == RuntimePlatform.WindowsEditor)*/
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }
        }
    }
}
