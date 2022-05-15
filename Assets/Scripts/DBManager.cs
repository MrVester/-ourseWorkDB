using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Threading.Tasks;



public record Inputs
{
    public static string id = "id";
    public static string nickname = "nickname";
    public static string login = "login";
    public static string password = "password";
    public static string newpassword = "newpassword";
    public static string soundvalue = "soundvalue";
    public static string musicvalue = "musicvalue";
    public static string attack = "attack";
    public static string activeitem = "activeitem";
    public static string moveback = "moveback";
    public static string movefront = "movefront";
    public static string moveleft = "moveleft";
    public static string moveright = "moveright";
    public static string screenmode = "screenmode";
    public static string resolution = "resolution";
    public static string quality = "quality";
    public static string framerate = "framerate";

    //other video and binds values

}
public record InputFieldTags
{
    public static string login = "Login";
    public static string nickname = "Username";
    public static string password = "Password";
    public static string newpassword = "NewPassword";
    public static string repeatpassword = "RepeatPassword";
    public static string repeatnewpassword = "RepeatNewPassword";

}
public record TextTags
{
    public static string infotext = "InfoText";
}
public record API
{
    private static string host = "https://mrvester.games/files/coursework/";
    public static string postLogin = host + "login";
    public static string postRegister = host + "register";
    public static string postUpdate = host + "update";
    public static string postSetSettings = host + "setsettings";
    public static string postGetSettings = host + "getsettings";
    public static string postSetDefaultSettings = host + "setdefaultsettings";
}
[Serializable]
public struct Response<P>
{
#nullable enable
    public string? errorCode;
    public P payload;
}

public struct EmptyPayload { }
[Serializable]
public struct UserPayload
{
    public User user;
}
[Serializable]
public struct User
{
    
    public string nickname;
    public string login;
}

[Serializable]
public struct SettingsPayload
{
    public Settings settings;
}
[Serializable]
public struct Settings
{
    public AudioSettings audiosettings;
    public ControlSettings controlsettings;
    public VideoSettings videosettings;
}
[Serializable]
public struct AudioSettings
{
    public float soundvalue;
    public float musicvalue;
}
[Serializable]
public struct ControlSettings
{
    public string attack;
    public string activeitem;
    public string moveback;
    public string movefront;
    public string moveleft;
    public string moveright;
}
[Serializable]
public struct VideoSettings
{
    public string screenmode;
    public string resolution;
    public string quality;
    public string framerate;
}


public class DBManager : MonoBehaviour
{
    public UIMenuController controller;
    public string GetIFTextWithTag(string tag)
    {
        if (GameObject.FindGameObjectWithTag(tag))
            return GameObject.FindGameObjectWithTag(tag).GetComponent<TMP_InputField>().text;
        else
            return "";
    }
    public void SetTextWithTag(string tag, string info)
    {
        if (GameObject.FindGameObjectWithTag(tag))
            GameObject.FindGameObjectWithTag(tag).GetComponent<TMP_Text>().text = info;

    }
    public P GetComponentWithTag<P>(string tag)
    {
        return GameObject.FindGameObjectWithTag(tag).GetComponent<P>();
    }
    readonly Dictionary<string, string> Errors = new Dictionary<string, string>()
    {
        ["RouteNotFound"] = "���� � ������ �� ������",
        ["IncorrectLogin"] = "Login is not correct",
        ["IncorrectPassword"] = "Password is not correct",
        ["EmptyLogin"] = "Enter Login",
        ["EmptyPassword"] = "Enter Password",
        ["EmptyNickname"] = "Enter Nickname",
        ["UserExists"] = "This User already has account",
        ["NotLoggedIn"] = "Log In, please",
        ["PasswordsDoNotMatch"] = "Both entered passwords must be identical"

    };
    public void StartUpdateUser()
    {
        StartCoroutine(UpdateUser());
    }
    public void StartRegisterUser()
    {
        StartCoroutine(RegisterUser());
    }
    public void StartLoginUser()
    {
        StartCoroutine(Login());
    }
    public void StartGetSettings()
    {
        StartCoroutine(GetSettings());
    }
    public void StartSetSettings()
    {
        StartCoroutine(SetSettings());
    }

    private P? handleRequest<P>(UnityWebRequest request) where P : struct
    {
        // Check for and display Request error
        if (request.error != null)
        {
            displayError("Request", request.error);
            return null;
        }

        Response<P> response = JsonUtility.FromJson<Response<P>>(request.downloadHandler.text);

        // Check for and display Response error
        if (response.errorCode != null)
        {
            displayError("Response", Errors[response.errorCode]);//
            return null;
        }

        // Hide previous error if this request was successful
        hideInfo();

        return response.payload;
    }

    private void displayInfo(string message)
    {
        GetComponentWithTag<TMP_Text>(TextTags.infotext).color = Color.green;
        GetComponentWithTag<TMP_Text>(TextTags.infotext).text = message;

        // Do a timeout and then
        // hideInfo()
    }

    private void displayError(string type, string message)
    {
        GetComponentWithTag<TMP_Text>(TextTags.infotext).color = Color.red;
        GetComponentWithTag<TMP_Text>(TextTags.infotext).text = type + "Error: " + message;
    }

    private void hideInfo()
    {
        GetComponentWithTag<TMP_Text>(TextTags.infotext).text = null;
        GetComponentWithTag<TMP_Text>(TextTags.infotext).color = Color.black;
    }


    private IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.nickname, GetIFTextWithTag(InputFieldTags.nickname));
        form.AddField(Inputs.login, GetIFTextWithTag(InputFieldTags.login));
        if (GetIFTextWithTag(InputFieldTags.password) != GetIFTextWithTag(InputFieldTags.repeatpassword))
        {
            displayError("Request", Errors["PasswordsDoNotMatch"]);
            yield break;
        }
        form.AddField(Inputs.password, GetIFTextWithTag(InputFieldTags.password));


        using UnityWebRequest request = UnityWebRequest.Post(API.postRegister, form);
        yield return request.SendWebRequest();

        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user == null)
        {
            yield break;
        }
        Storage.user = payload?.user;
        StartCoroutine(SetDefaultSettings());

        controller.SetMainMenu();
    }
    private IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, GetIFTextWithTag(InputFieldTags.login));
        form.AddField(Inputs.password, GetIFTextWithTag(InputFieldTags.password));

        using UnityWebRequest request = UnityWebRequest.Post(API.postLogin, form);
        yield return request.SendWebRequest();
        //Debug.Log(request.downloadHandler.text);
        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user == null)
        {
            yield break;
        }
        Storage.user = payload?.user;
        
        controller.SetMainMenu();
    }
    private IEnumerator UpdateUser()
    {
        WWWForm form = new WWWForm();
        Debug.Log(Storage.user?.login);
        form.AddField(Inputs.login, Storage.user?.login);
        form.AddField(Inputs.password, GetIFTextWithTag(InputFieldTags.password));

        if (GetIFTextWithTag(InputFieldTags.newpassword) != GetIFTextWithTag(InputFieldTags.repeatnewpassword))
        {
            displayError("Request", Errors["PasswordsDoNotMatch"]);
            yield break;
        }
        form.AddField(Inputs.newpassword, GetIFTextWithTag(InputFieldTags.newpassword));

        using UnityWebRequest request = UnityWebRequest.Post(API.postUpdate, form);
        yield return request.SendWebRequest();

        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user == null)
        {
            yield break;
        }
        displayInfo("You updated your data!");

    }

    private IEnumerator SetSettings()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);

        form.AddField(Inputs.soundvalue, /*Enter soundvalue*/"13");
        form.AddField(Inputs.musicvalue, /*Enter musicvalue*/"13");

        form.AddField(Inputs.attack, /*Enter value*/"������ ��������");
        form.AddField(Inputs.activeitem, /*Enter value*/"������ ��������");
        form.AddField(Inputs.moveback, /*Enter value*/"������ ��������");
        form.AddField(Inputs.movefront, /*Enter value*/"������ ��������");
        form.AddField(Inputs.moveleft, /*Enter value*/"������ ��������");
        form.AddField(Inputs.moveright, /*Enter value*/"������ ��������");

        form.AddField(Inputs.screenmode, /*Enter value*/"3");
        form.AddField(Inputs.resolution, /*Enter value*/"3");
        form.AddField(Inputs.quality, /*Enter value*/"3");
        form.AddField(Inputs.framerate, /*Enter value*/"3");

        //other video and binds values

        using UnityWebRequest request = UnityWebRequest.Post(API.postSetSettings, form);
        yield return request.SendWebRequest();

        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user == null)
        {
            yield break;
        }
        displayInfo("You updated your settings!");
    }
    private IEnumerator GetSettings()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);

        using UnityWebRequest request = UnityWebRequest.Post(API.postGetSettings, form);
        yield return request.SendWebRequest();

        SettingsPayload? payload = handleRequest<SettingsPayload>(request);

        if (payload?.settings == null)
        {
            yield break;
        }
        //Debug.Log(payload?.settings.audiosettings.musicvalue);
        Storage.settings = payload?.settings;
        Debug.Log(Storage.settings?.audiosettings.musicvalue);
        displayInfo("�������� ��������� " + payload?.settings.audiosettings + " � ���������: " + payload?.settings.audiosettings.musicvalue);
    }
    private IEnumerator SetDefaultSettings()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);

        using UnityWebRequest request = UnityWebRequest.Post(API.postSetDefaultSettings, form);
        yield return request.SendWebRequest();

        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user == null)
        {
            yield break;
        }
        displayInfo("Default Settings Set");
    }
}
