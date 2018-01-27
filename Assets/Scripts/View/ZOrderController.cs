using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZOrderController : MonoBehaviour 
{
    public const float GRANULARITY = 100f;

    [SerializeField]
    private GameObject positionSurce = null;

    [SerializeField]
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private List<int> spriteShifts = new List<int>();

    [SerializeField]
    private bool updateOnlyOnStart = false;

    [SerializeField]
    private float zShift = 0f;

    void Start()
    {
        if (spriteRenderers.Count == 0)
        {
            spriteRenderers = this.gameObject.GetComponentsInChildren<SpriteRenderer>().ToList();
        }

        if (positionSurce == null)
        {
            positionSurce = this.gameObject;
        }

        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteShifts.Add(spriteRenderers[i].sortingOrder);
        }

        if (updateOnlyOnStart)
        {
            RefreshZ();
        }
    }

	void Update () 
    {
        if (!updateOnlyOnStart)
        {
            RefreshZ();
        }
	}

    void RefreshZ()
    {
        int order = (int)Mathf.Clamp(GRANULARITY * -positionSurce.transform.position.y + GRANULARITY * zShift, 
            short.MinValue, short.MaxValue);
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sortingOrder = order + spriteShifts[i];
        }
    }
}
