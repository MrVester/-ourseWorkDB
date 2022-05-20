using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject Header;
    [SerializeField]
    private Canvas Interface;

    [SerializeField]
    private GameObject UserMenu;
    [SerializeField]
    private GameObject SettingsMenu;
    [SerializeField]
    private GameObject UpdatePasswordMenu;
    [SerializeField]
    private GameObject VideoSettingsMenu;
    [SerializeField]
    private GameObject AudioSettingsMenu;
    [SerializeField]
    private GameObject ControlSettingsMenu;
    public bool isPaused = false;
    public InputActionAsset actions;

    private void Awake()
    {
        
        actions["Menu"].performed += context => PauseSwitch();

    }
    private void MenuClearArea()
    {
        UserMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        UpdatePasswordMenu.SetActive(false);
        AudioSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(false);
        ControlSettingsMenu.SetActive(false);
    }

    private void Start()
    {
        isPaused = false;
        MenuClearArea();
        Header.SetActive(false);

    }
    private void PauseSwitch()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Pause();
        }
        else
        {
            unPause();
        }
    }


    private void Pause()
    {
        Time.timeScale = 0f;
        
        Interface.enabled = false;

        Header.SetActive(true);
    }
    private void unPause()
    {
        Time.timeScale = 1;
       
        Interface.enabled = true;

        MenuClearArea();
        Header.SetActive(false);
    }
    public void BackToGame()
    {
        unPause();
        isPaused = false;
        
    }
    public void MainMenu()
    {
        unPause();
        SceneManager.LoadScene("Figma Scene");
        
        
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
}
