using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using System;


public class SetControlSettings : MonoBehaviour
{
    [SerializeField]
    public InputActionAsset actions;

    [SerializeField]
    DBManager dbmanager;


    public void Start()
    {

        dbmanager = GetComponent<DBManager>();

    }
    public void Save()
    {

        var rebinds = actions.SaveBindingOverridesAsJson();
        Storage.controlsettings = new ControlSettings(rebinds);
        dbmanager.StartSetSettings();
        Debug.Log(rebinds);
    }
    public void Load(string binds)
    {
       // UpdateActionLabel();
        actions.LoadBindingOverridesFromJson(binds);
        

        /*int bindingIndex = actions["Attack"].GetBindingIndexForControl(actions["Attack"].controls[0]);

        AttackText.text = InputControlPath.ToHumanReadableString(
            actions["Attack"].bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);*/
    }
    /*private void UpdateActionLabel()
    {

            var action = actions["Attack"];
            AttackText.text = action != null ? action.name : string.Empty;
        
    }*/

}
