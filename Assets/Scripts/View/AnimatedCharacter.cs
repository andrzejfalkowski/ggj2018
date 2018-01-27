using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour 
{
    [SerializeField]
    private float idleTreshold = 0.01f;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator attackAnimator;
    [SerializeField]
    private List<SpriteRenderer> spriteRenderers;

    [SerializeField]
    private bool overrideAnimation = false;

    private Vector2 currentTarget = Vector2.zero;
    private float defaultXScale = 1f;

    public EState CurrentState = EState.IDLE;
    public enum EState
    {
        IDLE,
        MOVE
    }

    public bool Attacking = false;

    public EDirection CurrentDirection = EDirection.LEFT;
    public enum EDirection
    {
        LEFT,
        RIGHT
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
            if (Mathf.Abs(((Vector2)transform.parent.localPosition - currentTarget).sqrMagnitude) < idleTreshold)
            {
                CurrentState = EState.IDLE;
            }
            else
            {
                CurrentState = EState.MOVE;
            }

            if (transform.parent.localPosition.x < currentTarget.x)
            {
                CurrentDirection = EDirection.LEFT;
            }
            else
            {
                CurrentDirection = EDirection.RIGHT;
            }
        }

        animator.SetInteger("State", (int)CurrentState);

        animator.gameObject.transform.localScale = 
            new Vector3(CurrentDirection == EDirection.RIGHT ? -defaultXScale : defaultXScale, animator.gameObject.transform.localScale.y, 1f);
	}

    public void AnimateAttack()
    {
        if (attackAnimator != null)
        {
            attackAnimator.Play("attack");
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
