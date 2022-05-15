using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VideoSettings1 : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown ScreenmodedDropdown;

    [SerializeField]
    private TMP_Dropdown QualityDropdown;

    [SerializeField]
    private TMP_Dropdown FramerateDropdown;

    public int _Screenmoded;

    public int _Quality;
    
    public int _Resolution;

    public int _Framerate;

    Resolution resolution;

    


    private const string saveKey = "VideoSettings";
    public void Start()
    {


        Load();
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
        _Screenmoded = ScreenMode;
        ScreenmodedDropdown.value = _Screenmoded;
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


    private void Load()
    {
        var data = SaveManager.Load<SaveData.SavePropertis.VideoSettings>(saveKey);

        _Resolution = data.ScreenResolution;
        SetResolution(_Resolution);
        _Screenmoded = data.ScreenModed;
        ScreenMode(_Screenmoded);
        _Quality = data.Quality;
        SetQuality(_Quality);
        _Framerate = data.Framerate;
        SetFramerate(_Framerate);


    }
    public void ResetSettings()
    {
        var data = SaveManager.Load<SaveData.SavePropertis.VideoSettings>("Reset");

        _Resolution = data.ScreenResolution;
        SetResolution(_Resolution);
        _Screenmoded = data.ScreenModed;
        ScreenMode(_Screenmoded);
        _Quality = data.Quality;
        SetQuality(_Quality);
        _Framerate = data.Framerate;
        SetFramerate(_Framerate);
    }
    private void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshots());
    }

    private SaveData.SavePropertis.VideoSettings GetSaveSnapshots()
    {
        var data = new SaveData.SavePropertis.VideoSettings()
        {
            ScreenResolution = _Resolution,
            ScreenModed = _Screenmoded,
            Quality = _Quality,
            Framerate = _Framerate
        };
        return data;
    }
}
