using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetAudioSettings : MonoBehaviour
{
    private const string saveKey = "AudioSettings";
    [SerializeField]
    private Slider MusicSlider;
    [SerializeField]
    private Slider SoundSlider;

    private float _MusicVolume;

    private float _SoundVolume;

    public AudioMixer Sound;

    public AudioMixer Music;
    DBManager dbmanager;
    [SerializeField]
    private GameObject AudioSettingsMenu;

    public void Start()
    {

        dbmanager=GetComponent<DBManager>();
    
    }

    public void SetMusic(float value)
    {
        Music.SetFloat("Music", value);
        _MusicVolume = value;
        MusicSlider.value = _MusicVolume;

        Save();

    }
    public void SetSound(float value)
    {
        Sound.SetFloat("Sound", value);
        _SoundVolume = value;
        SoundSlider.value = _SoundVolume;

        Save();
    }


    public void Load(float? _SoundVolume, float? _MusicVolume)
    {
        var data = SaveManager.Load<SaveData.SavePropertis.AudioSettings>(saveKey);

        
        SetMusic((float)_MusicVolume);

        
        SetSound((float)_SoundVolume);


    }
    public void ResetSettings()
    {
        var data = SaveManager.Load<SaveData.SavePropertis.AudioSettings>("Reset");
        _MusicVolume = data.MusicVolume;
        SetMusic(_MusicVolume);

        _SoundVolume = data.SoundVolume;
        SetSound(_SoundVolume);

        Storage.audiosettings = new AudioSettings(data.MusicVolume, data.MusicVolume);
        dbmanager.StartSetSettings();
    }
    public void Save()
    {
        if (AudioSettingsMenu.activeSelf)
        {
            SaveManager.Save(saveKey, GetSaveSnapshots());
            Storage.audiosettings = new AudioSettings(_SoundVolume, _MusicVolume );
            dbmanager.StartSetSettings();
        }
        
    }

    private SaveData.SavePropertis.AudioSettings GetSaveSnapshots()
    {
        var data = new SaveData.SavePropertis.AudioSettings()
        {
            MusicVolume = _MusicVolume,
            SoundVolume = _SoundVolume
        };
        return data;
    }
}


