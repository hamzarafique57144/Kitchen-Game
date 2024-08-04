using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keyMoveUpText;
    [SerializeField] TextMeshProUGUI keyMoveDownText;
    [SerializeField] TextMeshProUGUI keyMoveLeftText;
    [SerializeField] TextMeshProUGUI keyMoveRightText;
    [SerializeField] TextMeshProUGUI keyInteractText;
    [SerializeField] TextMeshProUGUI keyInteractAltText;
    [SerializeField] TextMeshProUGUI keyPauseText;
    [SerializeField] TextMeshProUGUI keyGamePadMoveText;
    [SerializeField] TextMeshProUGUI keyGamePadInteractText;
    [SerializeField] TextMeshProUGUI keyGamePadInteractAltText;
    [SerializeField] TextMeshProUGUI keyGamePadPasueText;

    [SerializeField] GameObject tutorialPanel;
    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsCountDownStartToActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {

        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up).ToString();
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down).ToString();
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left).ToString();
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right).ToString();
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact).ToString();
        keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate).ToString();
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause).ToString();
    }

    private void Show()
    {
        tutorialPanel.SetActive(true);
    }
    private void Hide()
    {
        tutorialPanel.SetActive(false);
    }
}
