using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HidePasswordButton : MonoBehaviour
{
    public GameObject openedEye;
    public GameObject closedEye;
    public Toggle toggle;

    private void Awake()
    {
        openedEye.SetActive(false);
        closedEye.SetActive(true);
    }
    public void ChangePasswordState()
    {

        TMP_InputField parentInputField = GetComponent<TMP_InputField>();
        bool isEyeOpened = toggle.isOn;
        if (isEyeOpened)
        {
            parentInputField.contentType = TMP_InputField.ContentType.Standard;
            openedEye.SetActive(true);
            closedEye.SetActive(false);

        }
        else
        {
            parentInputField.contentType = TMP_InputField.ContentType.Password;
            openedEye.SetActive(false);
            closedEye.SetActive(true);
        }
        parentInputField.ForceLabelUpdate();
    }

}
