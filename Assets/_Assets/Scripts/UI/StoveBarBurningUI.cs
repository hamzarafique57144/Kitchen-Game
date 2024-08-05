using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBarBurningUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    [SerializeField] StoveCounter stoveCounter;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && e.progressNormalized > burnShowProgressAmount;
        animator.SetBool(IS_FLASHING, show);
    }

   
}
