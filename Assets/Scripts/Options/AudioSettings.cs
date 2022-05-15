using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings1 : MonoBehaviour
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


    public void Start()
    {


        Load();
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


    private void Load()
    {
        var data = SaveManager.Load<SaveData.SavePropertis.AudioSettings>(saveKey);

        _MusicVolume = data.MusicVolume;
        SetMusic(_MusicVolume);

        _SoundVolume = data.SoundVolume;
        SetSound(_SoundVolume);


    }
    public void ResetSettings()
    {
        var data = SaveManager.Load<SaveData.SavePropertis.AudioSettings>("Reset");
        _MusicVolume = data.MusicVolume;
        SetMusic(_MusicVolume);

        _SoundVolume = data.SoundVolume;
        SetSound(_SoundVolume);

    }
    private void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshots());
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


