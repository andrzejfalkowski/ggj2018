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
    private bool overrideAnimation = false;

    private Vector2 currentTarget = Vector2.zero;

    public EState CurrentState = EState.IDLE;
    public enum EState
    {
        IDLE,
        MOVE
    }

    public EDirection CurrentDirection = EDirection.LEFT;
    public enum EDirection
    {
        LEFT,
        RIGHT
    }

	// Use this for initialization
	public void HandleMovement (Vector2 target) 
    {
        currentTarget = target;
	}
	
	void Update () 
    {
        if (!overrideAnimation)
        {
            if (Mathf.Abs(((Vector2)this.gameObject.transform.localPosition - currentTarget).sqrMagnitude) < idleTreshold)
            {
                CurrentState = EState.IDLE;
            }
            else
            {
                CurrentState = EState.MOVE;
            }

            if (this.gameObject.transform.localPosition.x < currentTarget.x)
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
            new Vector3(CurrentDirection == EDirection.LEFT ? -1f: 1f, animator.gameObject.transform.localScale.y, 1f);
	}
}
