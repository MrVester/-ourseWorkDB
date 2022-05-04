using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using System.Threading.Tasks;



public record Inputs
{
    public static string name = "name";
    public static string password = "password";

}
public record API
{
    private static string host = "https://mrvester.games/files/coursework/";
    public static string postLogin = host + "login";
    public static string postRegister = host + "register";
    public static string postUpdate = host + "update";
}

public struct Response<P>
{
#nullable enable
    public string? errorCode;
    public P payload;
}

public struct EmptyPayload { }

public struct UserPayload
{
    public User user;
}

public struct User
{
    public int id;
    public string name;
}

public class DBManager : MonoBehaviour
{
    readonly Dictionary<string, string> errorsDictionary = new Dictionary<string, string>()
    {
        ["IncorrectName"] = "Имя пользователя введено неправильно",
        ["IncorrectPassword"] = "Введён неправильный пароль",
        ["EmptyName"] = "Введите Имя",
        ["EmptyPassword"] = "Введите Пароль"
    };
    public InputField inputName;
    public InputField inputPassword;
    public Text infoText;
    public bool isLoggedIn = false;
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
            displayError("Response", errorsDictionary[response.errorCode]);//
            return null;
        }

        // Hide previous error if this request was successful
        hideInfo();

        return response.payload;
    }

    private void displayInfo(string message)
    {
        infoText.color = Color.green;
        infoText.text = message;

        // Do a timeout and then
        // hideInfo()
    }

    private void displayError(string type, string message)
    {
        infoText.color = Color.red;
        infoText.text = type + "Error: " + message;
    }

    private void hideInfo()
    {
        infoText.text = null;
        infoText.color = Color.black;
    }


    private IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.name, inputName.text);
        form.AddField(Inputs.password, inputPassword.text);

        using UnityWebRequest request = UnityWebRequest.Post(API.postRegister, form);
        yield return request.SendWebRequest();

        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user != null)
        {
            displayInfo("You signed up!");
        }
    }
    private IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.name, inputName.text);
        form.AddField(Inputs.password, inputPassword.text);

        using UnityWebRequest request = UnityWebRequest.Post(API.postLogin, form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        UserPayload? payload = handleRequest<UserPayload>(request);

        if (payload?.user != null)
        {
            displayInfo("You logged in!");

            // Dispatch user ...
        }
    }

    private IEnumerator UpdateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField(Inputs.name, inputName.text);
        form.AddField(Inputs.password, inputPassword.text);

        using UnityWebRequest request = UnityWebRequest.Post(API.postUpdate, form);
        yield return request.SendWebRequest();

        EmptyPayload? payload = handleRequest<EmptyPayload>(request);

        if (payload != null)
        {
            displayInfo("You updated your data!");
        }
    }
}
