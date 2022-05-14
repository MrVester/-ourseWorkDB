using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField]
    private GameObject VideoSettings;
    [SerializeField]
    private GameObject AudioSettings;
    void Start()
    {
        ShowVideoSettings();
    }
    public void ShowVideoSettings()
    {
        VideoSettings.SetActive(true);

        AudioSettings.SetActive(false);
    }
    public void ShowAudioSettings()
    {
        VideoSettings.SetActive(false);
  
        AudioSettings.SetActive(true);
    }
}
