using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimatedCharacter : MonoBehaviour 
{
    [SerializeField]
    private float idleTreshold = 0.01f;

    [SerializeField]
    private bool separateMoveAndAttackDirection = false;
    [SerializeField]
    private Transform separateAttackParent = null;
    [SerializeField]
    private Transform frontArmTransform;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator attackAnimator;
    [SerializeField]
    private List<SpriteRenderer> spriteRenderers;

    [SerializeField]
    private bool overrideAnimation = false;

    [SerializeField]
    private Vector2 currentTarget = Vector2.zero;
    [SerializeField]
    private Vector2 currentAttackTarget = Vector2.zero;
    private float defaultXScale = 1f;

    private Tweener frontArmTween = null;

    public EState CurrentState = EState.IDLE;
    public enum EState
    {
        IDLE,
        MOVE
    }

    public BaseBeing Owner;

    public EDirection CurrentDirection = EDirection.LEFT;
    public EDirection CurrentAttackDirection = EDirection.LEFT;
    public enum EDirection
    {
        LEFT,
        RIGHT
    }

    public void Init(BaseBeing _owner)
    {
        Owner = _owner;
    }

    void Start()
    {
        defaultXScale = animator.gameObject.transform.localScale.x;
    }

	public void SetMoveTarget (Vector2 target) 
    {
        currentTarget = target;
	}

    public void ChangeSpriteColor(Color color)
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].color = color;
        }
    }

    private void Update () 
    {
        if (!overrideAnimation)
        {
            if (Mathf.Abs(((Vector2)transform.parent.position - currentTarget).sqrMagnitude) < idleTreshold)
            {
                CurrentState = EState.IDLE;
            }
            else
            {
                CurrentState = EState.MOVE;
            }

            if (transform.position.x < currentTarget.x)
            {
                CurrentDirection = EDirection.LEFT;
            }
            else
            {
                CurrentDirection = EDirection.RIGHT;
            }

            if (separateMoveAndAttackDirection)
            {
                if (transform.position.x < currentAttackTarget.x)
                {
                    CurrentAttackDirection = EDirection.LEFT;
                }
                else
                {
                    CurrentAttackDirection = EDirection.RIGHT;
                }

                if (separateAttackParent != null)
                {
                    separateAttackParent.localScale = 
                        new Vector3(CurrentDirection == CurrentAttackDirection ? 1f : -1f, separateAttackParent.localScale.y, 1f);
                }
            }
        }

        animator.SetInteger("State", (int)CurrentState);

        animator.gameObject.transform.localScale = 
            new Vector3(CurrentDirection == EDirection.RIGHT ? -defaultXScale : defaultXScale, animator.gameObject.transform.localScale.y, 1f);
	}

    private void SetupFrontArmDirection()
    {
        if(frontArmTransform != null)
        {
            if(CurrentAttackDirection == EDirection.LEFT)
            {
                Vector2 offset = currentAttackTarget - (Vector2)transform.position;
                float angle = Mathf.Cos(Mathf.Abs(offset.x)/offset.magnitude);
                Quaternion endRot = Quaternion.Euler(0, 0, Mathf.Sign(offset.y) * angle * Mathf.Rad2Deg);
                if(frontArmTween != null)
                {
                    frontArmTween.Kill();
                }
                frontArmTween = frontArmTransform.DOLocalRotateQuaternion(endRot, SettingsService.GameSettings.frontArmAnimLength)
                                                 .OnComplete(()=>
                {
                    frontArmTween = frontArmTransform.DOLocalRotateQuaternion(Quaternion.identity, SettingsService.GameSettings.frontArmRestoreIdleLength)
                                                     .SetDelay(SettingsService.GameSettings.frontArmAnimLength)
                                                     .OnComplete(() =>
                                                     {
                                                         frontArmTween = null;
                                                     });
                });
            }
        }
    }

    public void AnimateAttack(Vector2 target)
    {
        SetupFrontArmDirection();
        if (attackAnimator != null)
        {
            attackAnimator.Play("attack");
        }
        else
        {
            animator.Play("attack");
        }
        currentAttackTarget = target;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
