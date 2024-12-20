using System;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string[] animNames;

    private void Awake()
    {
        PlayerController.OnAttackAnimation += PlayerController_AttackAnimation;
    }
    private void Update()
    {
        PlayerMoveAnimation();
    }

    private void PlayerMoveAnimation()
    {
        if (!PlayerController.Instance._isAttack)
        {
            Vector2 isMoving = Vector2.zero;

            if (PlayerController.Instance._inputVector.x != isMoving.x)
            {
                SetAnimation("Move");
            }
            else
            {
                SetAnimation("Idle");
            }
        }
    }

    private void PlayerController_AttackAnimation()
    {
        animator.SetTrigger("Attack1");
    }

    private void SetAnimation(string animName)
    {
        foreach (string anim in animNames)
        {
            animator.SetBool(anim, false);
        }
        animator.SetBool(animName, true);
    }

    
}
