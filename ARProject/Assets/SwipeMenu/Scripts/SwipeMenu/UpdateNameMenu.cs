using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SwipeMenu;

    /// <summary>
    /// Выводит название выбранного пункта меню
    /// </summary>
    public class UpdateNameMenu : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI txt;

        public void Update()
        {
            string Mname;
            Mname = Menu.instance.menuItems[Menu.instance.GetClosestMenuItemIndex()].name;
            if (Mname == "Food") Mname = "объекты питания";
            if (Mname == "Medicine") Mname = "медицина";
            if (Mname == "Hotels") Mname = "гостиницы";
            if (Mname == "Places") Mname = "достопримечательности";
            if (Mname == "Routes") Mname = "пешеходные маршруты";
            if (Mname == "Culture") Mname = "культура";
            txt.text = Mname;
        }
    }

