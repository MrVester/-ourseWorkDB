using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject UserMenu;
    [SerializeField]  
    private GameObject SettingsMenu;
    [SerializeField]
    private GameObject PlayMenu;
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject NewGameMenu;
    [SerializeField]
    private GameObject LoginInMenu;
    [SerializeField]
    private GameObject RegisterMenu;
    [SerializeField]
    private GameObject UpdatePasswordMenu;


    void Start()
    {
        SetLoginInMenu();
    }

    private void MenuClearArea()
    {
        UserMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        PlayMenu.SetActive(false);
        MainMenu.SetActive(false);
        NewGameMenu.SetActive(false);
        LoginInMenu.SetActive(false);
        RegisterMenu.SetActive(false);
        UpdatePasswordMenu.SetActive(false);

    }
    public void SetMainMenu()
    {

            MenuClearArea();
            MainMenu.SetActive(true);
  
   
    }
    public void SetUserMenu()
    {
 
            MenuClearArea();
            UserMenu.SetActive(true);
        
    
    }
    public void SetSettingsMenu()
    {
        MenuClearArea();
        SettingsMenu.SetActive(true);
    }
    public void SetPlayMenu()
    {
        MenuClearArea();
        PlayMenu.SetActive(true);
    }
    public void SetNewGameMenu()
    {
        MenuClearArea();
        NewGameMenu.SetActive(true);
    }
    public void SetLoginInMenu()
    {
        MenuClearArea();
        LoginInMenu.SetActive(true);

    }
    public void SetRegisterMenu()
    {
        MenuClearArea();
        RegisterMenu.SetActive(true);

    }
    public void SetUpdatePasswordMenu()
    {
        MenuClearArea();
        UpdatePasswordMenu.SetActive(true);
    }
}
