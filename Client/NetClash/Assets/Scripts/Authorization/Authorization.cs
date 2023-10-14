using System;
using System.Collections.Generic;
using UnityEngine;

public class Authorization : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";

    private string _login;
    private string _password;

    public event Action<string> ErrorSingIn;
    public event Action<string> SuccessAuthorization;

    public void SetLogin(string login) { _login = login; }

    public void SetPassword(string password) { _password = password; }

    public void SignIn() {
        if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password)) {
            ErrorMessage("Login or(and) password is empty");
            ErrorSingIn?.Invoke("Login or(and) password is empty");
            return;
        }

        string uri = URLLibrary.MAIN + URLLibrary.AUTHORIZATION;

        Dictionary<string, string> data = new Dictionary<string, string>()
        {
            { LOGIN, _login},
            { PASSWORD, _password}
        };

        Network.Instance.Post(uri, data, SucessMessage, ErrorMessage);
    }

    private void SucessMessage(string data) {
        // если приходит массив от сервера (php)
        // echo 'ok|'.$user['id']
        // сплитим его и рузультаты сравниваем
         string[] result = data.Split('|');
         if (result.Length < 2 || result[0] != "ok") {
            ErrorMessage("Response of server: " + data);
            ErrorSingIn?.Invoke("Response of server: " + data);
            return;
        }

        if (int.TryParse(result[1], out int id)) {
            // do something (menu, etc.)

            UserInfo.Instance.SetID(id);
            SuccessAuthorization?.Invoke("User authorization success. user id: " + id);
            Debug.Log("Sucess. User ID: " + id);
        } else {
            ErrorMessage("Can't parse " + result[1] + " into INT. Full result: " + data);
            ErrorSingIn?.Invoke("Can't parse " + result[1] + " into INT. Full result: " + data);
        }
    }

    public void ErrorMessage(string error) {
        Debug.LogError(error);
        ErrorSingIn?.Invoke(error);
    }
}
