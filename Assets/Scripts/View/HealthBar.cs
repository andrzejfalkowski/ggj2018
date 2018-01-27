using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float healthBarVisibilityTime = 2.0f;

    [SerializeField]
    private Transform Fill;

    private float timer = 0;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        timer = healthBarVisibilityTime;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        timer += Time.deltaTime;
        if (timer >= healthBarVisibilityTime &&
            transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.zero;
        }
    }

    public void SetFill(float value)
    {
        Fill.localScale = new Vector3(value, 1, 1);
        timer = 0;
        transform.localScale = Vector3.one;
    }
}
