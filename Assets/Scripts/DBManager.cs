using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using System.Threading.Tasks;


public class DBManager : MonoBehaviour
{
    //public new string name;
   // public string password;
    public InputField inputName;
    public InputField inputPassword;
    public Text regText;
    public Text logInText;
    public bool isLoggedIn=false;
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

    /*private IEnumerator Send()
    {
        WWWForm form = new WWWForm();
        form.AddField("test","Hello");
        WWW www=new WWW("https://mrvester.games/files/scripts/", form);
       
        yield return www;
        if (www.error!=null)
        {
            userInfo.text="Ошибка: " +www.error;
        }
        userInfo.text = "Ответ сервера: " + www.text;

    }*/


    private IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", inputName.text);
        form.AddField("password", inputPassword.text);
        WWW www = new WWW("https://mrvester.games/files/coursework/register.php", form);

        yield return www;
        if (www.error != null)
        {
            regText.color= Color.red;
            regText.text = "Ошибкa: " + www.error;
        }
        else
        {
            regText.color = Color.black;
            regText.text = "Ответ сервера:\n" + www.text;
        }
        

    }
    
    public IEnumerable<UnityWebRequest> WebRequestPOST(string path, WWWForm form)
    {
        using UnityWebRequest request = UnityWebRequest.Post(path, form);
        yield return (UnityWebRequest)request.SendWebRequest();

        logInText.text = null;
        if (request.error != null)
        {
            logInText.color = Color.red;
            logInText.text = "Ошибка: " + request.error;

            yield break;
        }

        yield return request;
    }

    private IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", inputName.text);
        form.AddField("password", inputPassword.text);

        UnityWebRequest request = WebRequestPOST("https://mrvester.games/files/coursework/login.php", form);

        WWWForm form = new WWWForm();
        form.AddField("name", inputName.text);
        form.AddField("password", inputPassword.text);

        /*WWWForm form = new WWWForm();
        form.AddField("name", inputName.text);
        form.AddField("password", inputPassword.text);
        WWW www = new WWW("https://mrvester.games/files/coursework/login.php", form);

        yield return www;*/


        /*if (request.error != null)
        {
            logInText.color = Color.red;
            logInText.text = "Ошибка: " + request.error;

            yield break;
        }*/

            logInText.color = Color.black;


            Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
            Debug.Log(response.payload.user.id);

            /* isLoggedIn = Convert.ToBoolean(www.text);
             if (!isLoggedIn)
             {
                 logInText.color = Color.red;
                 logInText.text = "Пользователь не найден или неправильный пароль";
             }
             else
             {


                 SceneManager.LoadScene("delete");
             }*/
    }

    public IEnumerator UpdateUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", inputName.text);
        form.AddField("password", inputPassword.text);
        WWW www = new WWW("https://mrvester.games/files/coursework/update.php", form);

        yield return www;
        if (www.error != null)
        {
            regText.color = Color.red;
            regText.text = "Ошибка: " + www.error;
        }
        else
        {
            regText.color = Color.black;
            regText.text = "Ответ сервера:\n" + www.text;
        }

    }

}

[Serializable]
public struct Response
{
    string error;
    IPayload payload;
}

[Serializable]
public struct IPayload
{
    IUser user;
}

[Serializable]
public struct IUser
{
    int id;
    string name;
}

public class FormRequest
{
    private readonly string url;
    private readonly string method;
    public WWWForm Form { get; set; }
    public string Age { get; set; }

    public FormRequest(string method, string url)
    {
        this.url = url;
        this.method = method;

        
    }
}
