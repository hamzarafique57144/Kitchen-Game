using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainerCounterVisual : MonoBehaviour
{
    private const string Open_Colse = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    private void Awake()
    {
        animator= GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabedObject += ContanerCounter_OnPlayerGrabedObject();
    }

    private EventHandler ContanerCounter_OnPlayerGrabedObject()
    {
        throw new NotImplementedException();
    }

    private void  ContanerCounter_OnPlayerGrabedObject(object sender,EventArgs e)
    {
        animator.SetTrigger(Open_Colse);
    }
}
