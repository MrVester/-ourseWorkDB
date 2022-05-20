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
    public static string bindkey = "bindkey";

    public static string screenmode = "screenmode";
    public static string resolution = "resolution";
    public static string quality = "quality";
    public static string framerate = "framerate";
    public static string difficulty = "difficultyvalue";
    public static string character = "charactername";
    public static string item = "itemname";

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
    public static string postGetDifficultyParams = host + "getdifficultyparams";
    public static string postGetCharacterParams = host + "getcharacterparams";
    public static string postGetActiveItemParams = host + "getactiveitemparams";
    public static string postGetPassiveItemParams = host + "getpassiveitemparams";
    public static string postClearItemsbyLogin = host + "clearitems";
}
[Serializable]
public struct Response<P>
{
#nullable enable
    public string? errorCode;
    public P payload;
}

[Serializable]
public struct UserPayload
{
    public User user;
}
[Serializable]
public struct User
{
    public User(float prikol)
    {
        nickname = null;
        login = null;
    }
    public string? nickname;
    public string? login;
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
    public AudioSettings(float soundvalue, float musicvalue)
    {
        this.soundvalue = soundvalue;
        this.musicvalue = musicvalue;
    }
    public float soundvalue;
    public float musicvalue;
}
[Serializable]
public struct ControlSettings
{
    public ControlSettings(string bindkey)
    {
        this.bindkey = bindkey;

    }
    public string bindkey;

}
[Serializable]
public struct VideoSettings
{
    public VideoSettings(int screenmode, int resolution, int quality, int framerate)
    {
        this.screenmode = screenmode;
        this.resolution = resolution;
        this.quality = quality;
        this.framerate = framerate;
    }
    public int screenmode;
    public int resolution;
    public int quality;
    public int framerate;
}

[Serializable]
public struct CharacterPayload
{
    public Character character;
}
[Serializable]
public struct Character
{
    public float hp;
    public float damage;
    public float speed;
    public float armor;
}

[Serializable]
public struct DifficultyPayload
{
    public Difficulty difficulty;
}
[Serializable]
public struct Difficulty
{
    public float mobshpmult;
    public float mobsdamagemult;
}

[Serializable]
public struct ActiveItemPayload
{
    public ActiveItem activeitem;
}
[Serializable]
public struct ActiveItem
{
    public float damage;
    public float hp;
}

[Serializable]
public struct PassiveItemPayload
{
    public PassiveItem passiveitem;
}
[Serializable]
public struct PassiveItem
{
    public float damage;
    public float hpboost;
    public float hp;
    public float speed;
    public float armor;
}

public class DBManager : MonoBehaviour
{
    public Player player;
    public UIMenuController controller;

    public SetVideoSettings setVideoSetttings;

    public SetAudioSettings setAudioSetttings;

    public SetControlSettings setControlSetttings;
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
        /*if(!GameObject.FindGameObjectWithTag(tag))
        {
            return default(P);
        }*/
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
        ["PasswordsDoNotMatch"] = "Both entered passwords must be identical",
        ["DifficultyNotFound"] = "There is no such difficulty in DataBase",
        ["CharacterNotFound"] = "There is no such character in DataBase",
        ["ItemNotFound"] = "There is no such item in DataBase",

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
    public void StartGetDifficulty(string dif)
    {
        StartCoroutine(GetDifficulty(dif));
    }
    public void StartGetCharacter(string character)
    {
        StartCoroutine(GetCharacter(character));
    }
    public void StartGetnSetActiveItem(Collider2D collider)
    {
        if (collider)
            StartCoroutine(GetnSetActiveItem(collider));
    }
    public void StartGetnSetPassiveItem(Collider2D collider)
    {
        if (collider)
            StartCoroutine(GetnSetPassiveItem(collider));
    }
    public void StartClearItems()
    {
        StartCoroutine(ClearItems());
    }
    public void ClearLocalUser()
    {
        ClearUser();
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

        StartCoroutine(GetSettings());

        controller.SetMainMenu();

    }
    private IEnumerator Login()
    {
        Debug.Log(Storage.user?.login);
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

        Debug.Log(Storage.user?.login);

        StartCoroutine(GetSettings());


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
        Debug.Log(Storage.user?.login);
        form.AddField(Inputs.login, Storage.user?.login);

        form.AddField(Inputs.soundvalue, Storage.settings?.audiosettings.soundvalue.ToString());
        form.AddField(Inputs.musicvalue, Storage.settings?.audiosettings.musicvalue.ToString());
        Debug.Log(Storage.settings?.audiosettings.soundvalue.ToString());


        form.AddField(Inputs.bindkey, Storage.settings?.controlsettings.bindkey);

        form.AddField(Inputs.screenmode, Storage.settings?.videosettings.screenmode.ToString());
        form.AddField(Inputs.resolution, Storage.settings?.videosettings.resolution.ToString());
        form.AddField(Inputs.quality, Storage.settings?.videosettings.quality.ToString());
        form.AddField(Inputs.framerate, Storage.settings?.videosettings.framerate.ToString());


        using UnityWebRequest request = UnityWebRequest.Post(API.postSetSettings, form);
        yield return request.SendWebRequest();

        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user == null)
        {
            yield break;
        }
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

        setVideoSetttings.Load(Storage.settings?.videosettings.screenmode,
            Storage.settings?.videosettings.resolution,
            Storage.settings?.videosettings.quality,
            Storage.settings?.videosettings.framerate);
        setAudioSetttings.Load(Storage.settings?.audiosettings.soundvalue,
            Storage.settings?.audiosettings.musicvalue);
        setControlSetttings.Load(Storage.settings?.controlsettings.bindkey);
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

    private IEnumerator GetDifficulty(string difficultyvalue)
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.difficulty, difficultyvalue);

        using UnityWebRequest request = UnityWebRequest.Post(API.postGetDifficultyParams, form);
        yield return request.SendWebRequest();
        //Debug.Log(request.downloadHandler.text);
        DifficultyPayload? payload = handleRequest<DifficultyPayload>(request);


        if (payload?.difficulty == null)
        {
            yield break;
        }
        Enemy.mult = (Difficulty)payload?.difficulty;
        // Debug.Log(payload?.difficulty.mobshpmult + "  " + payload?.difficulty.mobsdamagemult);
    }
    private IEnumerator GetCharacter(string charactername)
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);
        form.AddField(Inputs.character, charactername);

        using UnityWebRequest request = UnityWebRequest.Post(API.postGetCharacterParams, form);
        yield return request.SendWebRequest();
        //Debug.Log(request.downloadHandler.text);
        CharacterPayload? payload = handleRequest<CharacterPayload>(request);


        if (payload?.character == null)
        {
            yield break;
        }
        Player.character = (Character)payload?.character;
        //Debug.Log(payload?.character.hp + "  " + payload?.character.damage + "  " + payload?.character.speed + "  " + payload?.character.armor);
    }
    public IEnumerator GetnSetActiveItem(Collider2D collider)
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);
        form.AddField(Inputs.item, collider.name);

        using UnityWebRequest request = UnityWebRequest.Post(API.postGetActiveItemParams, form);
        yield return request.SendWebRequest();
        //Debug.Log(request.downloadHandler.text);
        ActiveItemPayload? payload = handleRequest<ActiveItemPayload>(request);


        if (payload?.activeitem == null)
        {
            yield break;
        }
        Player.SetActiveStats((ActiveItem)(payload?.activeitem));
        player.OnActive(collider);
        //Debug.Log(payload?.activeitem.hp + "  " + payload?.activeitem.damage);
    }

    public IEnumerator GetnSetPassiveItem(Collider2D collider)
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);
        form.AddField(Inputs.item, collider.name);

        using UnityWebRequest request = UnityWebRequest.Post(API.postGetPassiveItemParams, form);
        yield return request.SendWebRequest();
        // Debug.Log(request.downloadHandler.text);
        PassiveItemPayload? payload = handleRequest<PassiveItemPayload>(request);


        if (payload?.passiveitem == null)
        {
            yield break;
        }
        Player.SetPassiveStats((PassiveItem)(payload?.passiveitem));
        player.OnPassive(collider);
        //Debug.Log(payload?.passiveitem.hp + "  " + payload?.passiveitem.hpboost + "  " + payload?.passiveitem.damage + "  " + payload?.passiveitem.speed + "  " + payload?.passiveitem.armor);
    }

    private IEnumerator ClearItems()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.login, Storage.user?.login);

        using UnityWebRequest request = UnityWebRequest.Post(API.postClearItemsbyLogin, form);
        yield return request.SendWebRequest();
        UserPayload? payload = handleRequest<UserPayload>(request);


        if (payload?.user == null)
        {
            yield break;
        }
        controller.SetLoginInMenu();
    }
    private void ClearUser()
    {
        Storage.user = new User(0.23f);
    }
}
