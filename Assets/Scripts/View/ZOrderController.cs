using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOrderController : MonoBehaviour 
{
    public const float GRANULARITY = 10f;

    [SerializeField]
    private GameObject positionSurce = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private bool updateOnlyOnStart = false;

    [SerializeField]
    private float zShift = 0f;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        if (positionSurce == null)
        {
            positionSurce = this.gameObject;
        }
    }

	void Update () 
    {
        if (!updateOnlyOnStart)
        {
            spriteRenderer.sortingOrder = (int)Mathf.Clamp(GRANULARITY * -positionSurce.transform.localPosition.y + GRANULARITY * zShift, 
                short.MinValue, short.MaxValue);
        }
	}
}
