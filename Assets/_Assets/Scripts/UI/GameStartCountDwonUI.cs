using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountDwonUI : MonoBehaviour
{
    private const string Number_POP_UP = "NumberPopUp";

    [SerializeField] TextMeshProUGUI countDwonText;
    [SerializeField] GameObject countdownPanel;
    private Animator animator;
    private int previousCountDownNumber;
    private void Awake()
    {
      animator = countdownPanel.GetComponent<Animator>();
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }
    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
        countDwonText.text = countDownNumber.ToString();
        if(previousCountDownNumber != countDownNumber)
        {
            previousCountDownNumber = countDownNumber;
            animator.SetTrigger(Number_POP_UP);
            SoundManager.Instance.playCountDownSound();
        }
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountDownStartToActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        countdownPanel.SetActive(true);
    }
    private void Hide()
    { countdownPanel.SetActive(false); }
}
