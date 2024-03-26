using System;
using Exercises._05_Auth;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LoginForm : MonoBehaviour
{
    private LoginCanvas loginCanvas;
    private Button loginButton;
    private CanvasGroup canvasGroup;

    [SerializeField] private GameObject ValidationBox;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_Text validationText;

    private void OnEnable()
    {
#if UNITY_EDITOR
        Assert.IsNotNull(ValidationBox);
        Assert.IsNotNull(usernameInput);
        Assert.IsNotNull(validationText);
#endif
    }

    private void Start()
    {
        Reset();
    }

    /// <summary>
    /// Reset the state of the UI to its starting state.
    /// </summary>
    private void Reset()
    {
        ValidationBox.SetActive(false);
    }

    public void OnSubmit()
    {
        Debug.Log("submitted login form");
        Debug.Log("submitted username" + usernameInput.text);

        // TODO(jason): Sanitize username before sending off to server.
        var newUsername = usernameInput.text;
        var validationMessage = "";
        var isValid = LobbyNetwork.ValidateUsername(newUsername, ref validationMessage);
        if (!isValid)
        {
            ShowValidationMessage(message: validationMessage);
            return;
        }

        Reset();
        LobbyNetwork.Instance.SignIn(newUsername);
    }

    private void ShowValidationMessage(string message)
    {
        ValidationBox.SetActive(true);
        validationText.text = message;
    }

    // TODO: loading state
}
