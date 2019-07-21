using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SwipeMenu;

    /// <summary>
    /// Выводит название выбранного пункта меню
    /// </summary>
    public class UpdateNameMenu : MonoBehaviour
    {
        public Text txt;

        public void Update()
        {
            string Mname;
            Mname = Menu.instance.menuItems[Menu.instance.GetClosestMenuItemIndex()].name;
            if (Mname == "Food") Mname = "Объекты питания";
            if (Mname == "Medicine") Mname = "Медицина";
            if (Mname == "Hotels") Mname = "Гостиницы";
            if (Mname == "Places") Mname = "Достопримечательности";
            if (Mname == "Routes") Mname = "Пешеходные маршруты";
            if (Mname == "Museums") Mname = "Культура";
            txt.text = Mname;
        }
    }

