using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountController : MonoBehaviour
{
    UIMenuController controller;
    DBManager dbmanager;
    void Start()
    {
        controller = GetComponent<UIMenuController>();
        dbmanager = GetComponent<DBManager>();
    }
    public void Logout()
    {
        dbmanager.StartClearItems();
        dbmanager.ClearLocalUser();
        controller.SetLoginInMenu();
    }
    public void Exit()
    {
        dbmanager.StartClearItems();
        dbmanager.ClearLocalUser();
        Application.Quit();

    }
}
