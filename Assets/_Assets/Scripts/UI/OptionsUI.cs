using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameInput;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Button soundEffectButton;
    [SerializeField] Button musicButton;
    [SerializeField] TextMeshProUGUI soundEffecctText;
    [SerializeField] TextMeshProUGUI musicText;
    [Header("Options Texts")]
    [SerializeField] TextMeshProUGUI moveUpText;
    [SerializeField] TextMeshProUGUI moveDownText;
    [SerializeField] TextMeshProUGUI moveLeftText;
    [SerializeField] TextMeshProUGUI moveRightText;
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] TextMeshProUGUI interactAltText;
    [SerializeField] TextMeshProUGUI pauseText;
    [Header ("Options Buttons")] 
    [SerializeField] Button moveUpButton;
    [SerializeField] Button moveDownButton;
    [SerializeField] Button moveLeftButton;
    [SerializeField] Button moveRightButton;
    [SerializeField] Button interactButton;
    [SerializeField] Button interactAltButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Transform pressToRebindKeyTransform;

    private void Awake()
    {
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });

    }

    private void Start()
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        soundEffecctText.text = "Sound Effect: "+Mathf.Round(SoundManager.Instance.GetVolume() * 10).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10).ToString();

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up).ToString();
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down).ToString();
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left).ToString();
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right).ToString();
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact).ToString();
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate).ToString();
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause).ToString();
    }

    public void OnCloseBtnClick()
    {
        
        Time.timeScale = 1f;
        optionsPanel.SetActive(false);
    }
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
