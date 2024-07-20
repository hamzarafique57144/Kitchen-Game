using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject counterViusal;   
    void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; 
    }
    private void Player_OnSelectedCounterChanged(object sender,PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter==baseCounter)
        {
            Debug.Log("Show the selected counter");
            Show();
        }
        else
        {
           
            Hide();
        }
    }
   private void Show()
    {
       counterViusal.SetActive(true);
    }
   private void Hide()
    {
       counterViusal.SetActive(false);

    }
}
