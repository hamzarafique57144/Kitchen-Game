using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Button soundEffectButton;
    [SerializeField] Button musicButton;
    [SerializeField] TextMeshProUGUI soundEffecctText;
    [SerializeField] TextMeshProUGUI musicText;

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
    }

    private void Start()
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        soundEffecctText.text = "Sound Effect: "+Mathf.Round(SoundManager.Instance.GetVolume() * 10).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10).ToString();
    }
}
