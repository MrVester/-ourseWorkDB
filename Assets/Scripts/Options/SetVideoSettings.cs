using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SetVideoSettings : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown ScreenmodedDropdown;

    [SerializeField]
    private TMP_Dropdown QualityDropdown;

    [SerializeField]
    private TMP_Dropdown FramerateDropdown;

    public int _Screenmode;

    public int _Quality;
    
    public int _Resolution;

    public int _Framerate;

    Resolution resolution;

    DBManager dbmanager;
    [SerializeField]
    private GameObject VideoSettingsMenu;
    private const string saveKey = "VideoSettings";
    public void Start()
    {

        dbmanager = GetComponent<DBManager>();
      
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        _Quality = qualityIndex;
        QualityDropdown.value = _Quality;
        Save();
    }
    public void ScreenMode(int ScreenMode)
    {
        switch (ScreenMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

        }
        _Screenmode = ScreenMode;
        ScreenmodedDropdown.value = _Screenmode;
        Save();


    }
    public void SetResolution(int ResolutionIndex)
    {
        switch (ResolutionIndex)
        {
            case 0:
                resolution.width = 1280;
                resolution.height = 720;

                break;
            case 1:
                resolution.width = 1600;
                resolution.height = 900;
                break;
            case 2:
                resolution.width = 1920;
                resolution.height = 1080;
                break;
            case 3:
                resolution.width = 2048;
                resolution.height = 1152;
                break;
            case 4:
                resolution.width = 2560;
                resolution.height = 1440;
                break;
            case 5:
                resolution.width = 3840;
                resolution.height = 2160;
                break;
        }

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);
        _Resolution = ResolutionIndex;
        resolutionDropdown.value = _Resolution;
        Save();
    }
    public void SetFramerate(int FramerateIndex)
    {
        switch (FramerateIndex)
        {
            case 0:
                resolution.refreshRate = 60;

                break;
            case 1:
                resolution.refreshRate = 75;
                break;
            case 2:
                resolution.refreshRate = 100;
                break;
            case 3:
                resolution.refreshRate = 120;
                break;
            case 4:
                resolution.refreshRate = 144;
                break;
            case 5:
                resolution.refreshRate = 160;
                break;
        }

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);
        _Framerate = FramerateIndex;
        FramerateDropdown.value = _Framerate;
        Save();
    }
    public void SetupVideoSettings()
    {
        Debug.Log(Storage.controlsettings?.attack);
        //Load();
    }

    public void Load( int? _Screenmode, int? _Resolution, int? _Quality,int? _Framerate)
    {
        var data = SaveManager.Load<SaveData.SavePropertis.VideoSettings>(saveKey);
        
       
        SetResolution((int)(_Resolution));
       
        
        ScreenMode((int)(_Screenmode));
        
        SetQuality((int)(_Quality));
        
        SetFramerate((int)(_Framerate));
       

    }
    public void ResetSettings()
    {
        var data = SaveManager.Load<SaveData.SavePropertis.VideoSettings>("Reset");

        _Screenmode = data.ScreenModed;
        ScreenMode(_Screenmode);
        _Resolution = data.ScreenResolution;
        SetResolution(_Resolution);
        _Quality = data.Quality;
        SetQuality(_Quality);
        _Framerate = data.Framerate;
        SetFramerate(_Framerate);

        Storage.videosettings = new VideoSettings(data.ScreenModed, data.ScreenResolution,data.Quality, data.Framerate);
        dbmanager.StartSetSettings();
    }
    private void Save()
    {
        if(VideoSettingsMenu.activeSelf)
        {
            SaveManager.Save(saveKey, GetSaveSnapshots());
            Storage.videosettings = new VideoSettings(_Screenmode,_Resolution , _Quality, _Framerate);
            dbmanager.StartSetSettings();
        }
 
    }

    private SaveData.SavePropertis.VideoSettings GetSaveSnapshots()
    {
        var data = new SaveData.SavePropertis.VideoSettings()
        {
            ScreenResolution = _Resolution,
            ScreenModed = _Screenmode,
            Quality = _Quality,
            Framerate = _Framerate
        };
        return data;
    }
}
