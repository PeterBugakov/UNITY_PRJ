﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SwipeMenu
{
	/// <summary>
	/// Attach to any menu item. 
	/// </summary>
	public class MenuItem : MonoBehaviour
	{
		/// <summary>
		/// The behaviour to be invoked when the menu item is selected.
		/// </summary>
		public Button.ButtonClickedEvent OnClick;

		/// <summary>
		/// The behaviour to be invoked when another menu item is selected.
		/// </summary>
		public Button.ButtonClickedEvent OnOtherMenuClick;

        public void SwitchScene(int MenuItem)
        {
            switch (MenuItem)
            {
                case 1: SceneManager.LoadScene("ARScene"); break;
                case 2: SceneManager.LoadScene("ARScene"); break;
                case 3: SceneManager.LoadScene("ARScene"); break;
                case 4: SceneManager.LoadScene("ARScene"); break;
                case 5: SceneManager.LoadScene("ARScene"); break;
                case 6: SceneManager.LoadScene("ARScene"); break;
            }
        }
    }
}