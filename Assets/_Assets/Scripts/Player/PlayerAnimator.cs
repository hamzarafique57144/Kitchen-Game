using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private const string IS_WALKING = "isWalking";
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
