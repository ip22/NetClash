using UnityEngine;
using UnityEngine.UI;

public class RegistrationUI : MonoBehaviour
{
    [SerializeField] private Registration _registration;
    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;
    [SerializeField] private InputField _confirmPassword;
    [SerializeField] private Button _apply;
    [SerializeField] private Button _signIn;
    [SerializeField] private Text _message;

    [SerializeField] private GameObject _authCanvas;
    [SerializeField] private GameObject _regCanvas;

    private void Awake() {
        _login.onEndEdit.AddListener(_registration.SetLogin);
        _password.onEndEdit.AddListener(_registration.SetPassword);
        _confirmPassword.onEndEdit.AddListener(_registration.SetConfirmPassword);

        _apply.onClick.AddListener(SignUpClick);
        _signIn.onClick.AddListener(SignInClick);

        _registration.ErrorRegistration += (error) => {
            _message.text = error;
            _apply.gameObject.SetActive(true);
            _signIn.gameObject.SetActive(true);
        };

        _registration.SuccessRegistration += (success) => {
            _message.text = success;
            _signIn.gameObject.SetActive(true);
            Debug.Log("EVENT Registration is success");
        };
    }
    private void SignUpClick() {
        _apply.gameObject.SetActive(false);
        _signIn.gameObject.SetActive(false);
        _registration.SignUp();
    }

    private void SignInClick() {
        _message.text = "Enter your login and password";
        _regCanvas.SetActive(false);
        _authCanvas.SetActive(true);
    }
}
