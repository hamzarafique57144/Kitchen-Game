using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image IconImage;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Color successColor;
    [SerializeField] Color failedColor;
    [SerializeField] Sprite successSprite;
    [SerializeField] Sprite failedSprite;
    private const string POPUP = "PopUp";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagger_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManagger_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        IconImage.sprite = failedSprite;
        backgroundImage.color = failedColor;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        IconImage.sprite = successSprite;
        backgroundImage.color = successColor;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}
