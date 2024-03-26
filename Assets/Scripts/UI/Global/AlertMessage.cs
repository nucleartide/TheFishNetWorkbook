using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class AlertMessage : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text message;

    private Coroutine _showTimedMessage;
    private CanvasGroup _canvasGroup;
    private Color _originalBackgroundColor;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FirstInitialize()
    {
                _canvasGroup.alpha = 0;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
        
                if (backgroundImage != null)
                {
                    _originalBackgroundColor = backgroundImage.color;
                }
    }

    private void Start()
    {

    }

    public void ShowTimedMessage(string msg)
    {
        ShowTimedMessage(text: msg, color: Color.red, duration: 5f, alpha: .8f);
    }

    public void ShowTimedMessage(string text, Color color, float duration, float alpha)
    {
        if (_showTimedMessage != null)
        {
            StopCoroutine(_showTimedMessage);
            _showTimedMessage = null;
        }

        _showTimedMessage = StartCoroutine(__ShowTimedMessage(text, color, duration, alpha));
    }

    private IEnumerator __ShowTimedMessage(string text, Color color, float duration, float alpha)
    {
        // Enable the canvas group
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        
        // Set alert content
        message.text = text;
        if (backgroundImage != null)
        {
            backgroundImage.color = new Color(color.r, color.g, color.b, alpha);
        }
        
        // Wait
        yield return new WaitForSeconds(duration);

        // Indicate that coroutine has finished running
        _showTimedMessage = null;

        // Disable the alert message canvas group
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void ShowPersistentMessage(string text, Color color, float alpha = 1.0f)
    {
        // stop coroutine if one exists
        if (_showTimedMessage != null)
        {
            StopCoroutine(_showTimedMessage);
            _showTimedMessage = null;
        }

        // Enable the canvas group
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        // Set alert content
        message.text = text;
        if (backgroundImage != null)
        {
            backgroundImage.color = new Color(color.r, color.g, color.b, alpha);
        }
    }

    public void HidePersistentMessage()
    {
        // Disable the canvas group
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        // reset content
        Reset();
    }

    public void Reset()
    {
        message.text = "";
        if (backgroundImage != null)
        {
            backgroundImage.color = _originalBackgroundColor;
        }
    }
}
