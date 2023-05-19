using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;


public enum IntroSceneMenus { Main, Options}
public class IntroMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;


    private void Awake()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }

    public void SwitchToMain()
    {
        ChangeMenu(IntroSceneMenus.Main);
    }
    public void SwitchToOptions()
    {
        ChangeMenu(IntroSceneMenus.Options);
    }


    public void ChangeMenu(IntroSceneMenus newMenu)
    {
        TurnAllMenusOff();
        switch (newMenu)
        {
            case IntroSceneMenus.Main:
                menu.SetActive(true);
                break;
            case IntroSceneMenus.Options:
                options.SetActive(true);
                break;
        }

        //local
        void TurnAllMenusOff()
        {
            menu.SetActive(false);
            options.SetActive(false);
        }
    }

}
