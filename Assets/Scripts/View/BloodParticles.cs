using System;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Particles;
    [Space]
    [SerializeField]
    private int minParticlesInEmission;
    [SerializeField]
    private int maxParticlesInEmission;

    private Transform linkedBeing;

    public void Play(bool destroy)
    {
        if(linkedBeing == null)
        {
            linkedBeing = transform.parent;
            transform.SetParent(GameManager.Instance.ParticlesParent);
            transform.localScale = Vector3.one;
        }
        //
        try
        {
            if (this != null && gameObject != null && transform != null && linkedBeing != null && Particles != null)
            {
                transform.position = linkedBeing.position + Vector3.back * 0.5f;
                Particles.Emit(UnityEngine.Random.Range(minParticlesInEmission, maxParticlesInEmission));
                if (destroy)
                {
                    Destroy(gameObject, 1.0f);
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogWarning("NullReference catched " + ex.Message);
        }
    }
}