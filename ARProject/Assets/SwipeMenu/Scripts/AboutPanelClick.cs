using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwipeMenu;

public class AboutPanelClick : MonoBehaviour
{
    private int clk=0;
    private void Start()
    {
        Panel.gameObject.SetActive(false);
        GameObject MainMenu = GameObject.Find("Menu");
        MainMenu.GetComponent<Menu>().ShowMenus();
    }

    public GameObject Panel;
    public void HidePanel()
    {
        Panel.gameObject.SetActive(false);
        GameObject MainMenu = GameObject.Find("Menu");
        MainMenu.GetComponent<Menu>().ShowMenus();
        
    }
    public void ShowPanel()
    {
        clk++;
        GameObject MainMenu = GameObject.Find("Menu");
        if (clk==1)
        {
            Panel.gameObject.SetActive(true);
            MainMenu.GetComponent<Menu>().HideMenus();
        }
        if (clk>=2)
        {
            Panel.gameObject.SetActive(false);
            MainMenu.GetComponent<Menu>().ShowMenus();
            clk = 0;
        }
       /* GameObject txt;
        txt = GameObject.Find("City");
        txt.GetComponent<TMPro.TextMeshProUGUI>().text=clk.ToString();*/
}
}


