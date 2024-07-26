using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressGameObject;
    private IHasProgress hasProgress;
    [SerializeField] Image barImage;
    private void Start()
    {
       hasProgress= hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError(hasProgressGameObject + " does not have a componenet that implement IHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f || e.progressNormalized == 1f )
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
