using System;
using UnityEngine;
using UnityEngine.Assertions;

public class LoginCanvas : MonoBehaviour
{
    #region Serialized
    [SerializeField] private CanvasGroup loginForm;
    #endregion

    #region Private Methods
    private void SetActive(bool active)
    {
        // Enable the canvas group
        if (active)
        {
            loginForm.alpha = 1;
            loginForm.interactable = true;
            loginForm.blocksRaycasts = true;
        }
        else
        {
            loginForm.alpha = 0;
            loginForm.interactable = false;
            loginForm.blocksRaycasts = false;
        }
    }

    private void FirstInitialize()
    {
        SetActive(false);
    }
    #endregion

    #region Callbacks
    private void Start()
    {
        FirstInitialize();
    }

    private void OnValidate()
    {
        Assert.IsNotNull(loginForm, "[LoginCanvas] loginForm is null, did you forget to wire this up?");
    }
    #endregion
}
