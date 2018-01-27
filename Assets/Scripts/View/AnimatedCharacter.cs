using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour 
{
    [SerializeField]
    private float idleTreshold = 0.01f;

    [SerializeField]
    private bool separateMoveAndAttackDirection = false;
    [SerializeField]
    private Transform separateAttackParent = null;

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

    public void AnimateAttack(Vector2 target)
    {
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
