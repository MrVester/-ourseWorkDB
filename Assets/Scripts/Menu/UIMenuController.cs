using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private GameObject VideoSettingsMenu;
    [SerializeField]
    private GameObject AudioSettingsMenu;
    [SerializeField]
    private GameObject Header;
    [SerializeField]
    private GameObject ControlSettingsMenu;



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
        AudioSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(false);
        Header.SetActive(false);
        ControlSettingsMenu.SetActive(false);
    }
    public void SetHeader()
    {
        Header.SetActive(true);
    }
    public void SetVideoSettings()
    {
        MenuClearArea();
        VideoSettingsMenu.SetActive(true);

    }
    public void SetAudioSettings()
    {
        MenuClearArea();
        AudioSettingsMenu.SetActive(true);
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
  
    public void SetControlSettings()
    {
        MenuClearArea();
        ControlSettingsMenu.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
