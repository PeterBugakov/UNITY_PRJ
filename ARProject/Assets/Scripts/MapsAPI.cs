using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System;
using System.Net;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MapsAPI : MonoBehaviour
{
    public GameObject originalTable;
    public GameObject[] tables;
    public GameObject texterror;
    public float lan;
    public float lon;
    public GameObject maincam;

    float speed = 5.0f;
    //string param = ChooseMenu.Filter;
    string param = "type=food";
    Transform target;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        texterror.GetComponent<Text>().text = "RUN";
        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            print("Time out");
            yield break;
        }
        texterror.GetComponent<Text>().text = "Loc";
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            lan = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }
        Input.location.Stop();
        texterror.GetComponent<Text>().text = lan.ToString() + "," + lon.ToString();
        string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + lan.ToString() + "," + lon.ToString() + "&radius=500&" + param + "&key=AIzaSyBdHiBxdXr5M-DKeAP494aTcEUa9imgrPw";
        //string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=59.931167,30.360708&radius=500&" +param+"&key=AIzaSyBdHiBxdXr5M-DKeAP494aTcEUa9imgrPw";
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            Processjson(www.text);
        }
        else
        {
            texterror.GetComponent<Text>().text = "ERROR: " + www.error;
        }
    }

    private void Processjson(string jsonString)
    {
        int rad = 6372795;
        float llat1;
        float llong1;
        float llat2;
        float llong2;

        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        tables = new GameObject[jsonvale["results"].Count];
        //texterror.GetComponent<Text>().text = jsonvale["results"].Count.ToString();
        for (int i = 0; i < jsonvale["results"].Count; i++)
        {
            //gis-lab.info/qa/great-circles.html

            //Debug.Log(jsonvale["results"][i]["geometry"]["location"]["lat"].ToString());
            //Debug.Log(jsonvale["results"][i]["geometry"]["location"]["lng"].ToString());
            llat1 = lan;
            llong1 = lon;
            llat2 = float.Parse(jsonvale["results"][i]["geometry"]["location"]["lat"].ToString());
            llong2 = float.Parse(jsonvale["results"][i]["geometry"]["location"]["lng"].ToString());

            float lat1 = llat1 * (float)Math.PI / 180;
            float lat2 = llat2 * (float)Math.PI / 180;
            float long1 = llong1 * (float)Math.PI / 180;
            float long2 = llong2 * (float)Math.PI / 180;

            float cl1 = (float)Math.Cos(lat1);
            float cl2 = (float)Math.Cos(lat2);
            float sl1 = (float)Math.Sin(lat1);
            float sl2 = (float)Math.Sin(lat2);
            float delta = long2 - long1;
            float cdelta = (float)Math.Cos(delta);
            float sdelta = (float)Math.Sin(delta);

            float x = (cl1 * sl2) - (sl1 * cl2 * cdelta);
            float y = sdelta * cl2;
            float z = (float)Math.Atan(-y / x) * (180 / (float)Math.PI);
            if (x < 0)
            {
                z = z + 180;
            }

            float z2 = (z + 180) % 360 - 180;
            z2 = -z2 / (180 / (float)Math.PI);
            float anglerad2 = z2 - ((2 * (float)Math.PI) * (float)Math.Floor((z2 / (2 * (float)Math.PI))));
            float angledeg = (anglerad2 * 180) / (float)Math.PI;

            y = (float)Math.Sqrt(Math.Pow(cl2 * sdelta, 2) + Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
            x = sl1 * sl2 + cl1 * cl2 * cdelta;
            float ad = (float)Math.Atan2(y, x);
            float dist = ad * rad;

            float tablex = maincam.transform.position.x + dist / 60 * (float)Math.Cos(angledeg);
            float tablez = maincam.transform.position.z + dist / 60 * (float)Math.Sin(angledeg);

            tables[i] = Instantiate(originalTable, new Vector3(tablex, 1.96f, tablez), Quaternion.identity);
            //texterror.GetComponent<Text>().text = texterror.GetComponent<Text>().text+";"+tablex.ToString()+","+tablez.ToString();
            tables[i].transform.LookAt(maincam.transform);
            Transform[] ts = tables[i].transform.GetComponentsInChildren<Transform>(true);
            Material FoodMat = Resources.Load("Materials/Fastfood", typeof(Material)) as Material;
            Material opened = Resources.Load("Materials/opened", typeof(Material)) as Material;
            Material closed = Resources.Load("Materials/closed", typeof(Material)) as Material;//getting material
            Material hz = Resources.Load("Materials/hz", typeof(Material)) as Material;
            foreach (Transform t in ts)
            {
                Color obj_type_color;
                if (t.gameObject.name == "Caption")
                {
                    t.gameObject.GetComponent<Text>().text = jsonvale["results"][i]["name"].ToString();
                }
                if (t.gameObject.name == "Type")
                {
                    t.gameObject.GetComponent<Text>().text = dist.ToString() + " метра";
                }
                if (t.gameObject.name == "Icon")
                {
                    t.gameObject.GetComponent<RawImage>().material = FoodMat; //applying material
                }
                if (t.gameObject.name == "Status")
                {
                    try
                    {
                        if (jsonvale["results"][i]["opening_hours"]["open_now"].ToString() == "True")
                        {
                            GameObject.Find("Status").GetComponent<Renderer>().material = opened;
                            GameObject.Find("StatusText").GetComponent<Text>().text = "Открыто";
                        }
                        else
                        {
                            GameObject.Find("Status").GetComponent<Renderer>().material = closed;
                            GameObject.Find("StatusText").GetComponent<Text>().text = "Закрыто";
                        }
                    }
                    catch
                    {
                        GameObject.Find("Status").GetComponent<Renderer>().material = hz;
                        GameObject.Find("StatusText").GetComponent<Text>().text = "Нет данных";
                    }
                }
                if (t.gameObject.name == "Image")
                {
                    //Debug.Log(jsonvale["results"][i]["types"][1].ToString());
                    try
                    {
                        switch (jsonvale["results"][i]["types"][1].ToString())
                        {
                            case "political":
                                obj_type_color = Color.grey;
                                break;
                            case "establishment":
                                obj_type_color = Color.blue;
                                break;
                            case "store":
                                obj_type_color = Color.green;
                                break;
                            case "atm":
                                obj_type_color = Color.yellow;
                                break;
                            case "bank":
                                obj_type_color = Color.yellow;
                                break;
                            default:
                                obj_type_color = Color.magenta;
                                break;
                        }
                        t.gameObject.GetComponent<Image>().color = obj_type_color;
                    }
                    catch
                    {

                    }

                    
                }
            };
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
        }
        else
        {
            lan = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
        }

        if (Input.GetMouseButton(0))
        {
            transform.LookAt(maincam.transform);
            transform.RotateAround(maincam.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
        }
    }
}
