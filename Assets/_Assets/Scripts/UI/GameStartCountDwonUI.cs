using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountDwonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDwonText;
    

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }
    private void Update()
    {
        countDwonText.text = GameManager.Instance.GetCountDownToStartTimer().ToString("#");
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
        gameObject.SetActive(true);
    }
    private void Hide()
    { gameObject.SetActive(false); }
}
