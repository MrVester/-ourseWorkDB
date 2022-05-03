using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using System.Threading.Tasks;

public record API
{
  private static string host = "https://mrvester.games/files/coursework/";
  public static string postLogin = host + "login.php";
  public static string postRegister = host + "register.php";
  public static string postUpdate = host + "update.php";
}

public struct Response<P>
{
  public string error { get; }
  public P payload;
}

public struct EmptyPayload { }

public struct UserPayload
{
  public User user;
}

// [Serializable]
public struct User
{
  public int id;
  public string name;
}

public class DBManager : MonoBehaviour
{
  //public new string name;
  // public string password;
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
    if (response.error != null)
    {
      displayError("Response", response.error);
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
    form.AddField("name", inputName.text);
    form.AddField("password", inputPassword.text);

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
    form.AddField("name", inputName.text);
    form.AddField("password", inputPassword.text);

    using UnityWebRequest request = UnityWebRequest.Post(API.postLogin, form);
    yield return request.SendWebRequest();

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
    form.AddField("name", inputName.text);
    form.AddField("password", inputPassword.text);

    using UnityWebRequest request = UnityWebRequest.Post(API.postUpdate, form);
    yield return request.SendWebRequest();

    EmptyPayload? payload = handleRequest<EmptyPayload>(request);

    if (payload != null)
    {
      displayInfo("You update your data!");
    }
  }
}
