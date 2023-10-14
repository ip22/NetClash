using System;
using UnityEditor.VersionControl;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationUI : MonoBehaviour
{
    [SerializeField] private Authorization _authorization;
    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;
    [SerializeField] private Button _signIn;
    [SerializeField] private Button _signUp;
    [SerializeField] private Text _message;

    [SerializeField] private GameObject _authCanvas;
    [SerializeField] private GameObject _regCanvas;

    private void Awake() {
        _login.onEndEdit.AddListener(_authorization.SetLogin);
        _password.onEndEdit.AddListener(_authorization.SetPassword);

        _signIn.onClick.AddListener(SignInClick);
        _signUp.onClick.AddListener(SignUpClick);

        _authorization.ErrorSingIn += (error) => {
            _message.text = error;
            _signIn.gameObject.SetActive(true);
            _signUp.gameObject.SetActive(true);
        };

        _authorization.SuccessAuthorization += (success) => {
            _message.text = success;
        };
    }

    private void SignUpClick() {
        _message.text = "Registration of new user";
        _authCanvas.SetActive(false);
        _regCanvas.SetActive(true);
    }

    private void SignInClick() {
        _signIn.gameObject.SetActive(false);
        _signUp.gameObject.SetActive(false);
        _authorization.SignIn();
    }
}
