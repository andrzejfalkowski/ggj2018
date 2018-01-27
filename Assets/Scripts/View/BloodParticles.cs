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

    private void Start()
    {
        linkedBeing = transform.parent;
        transform.SetParent(GameManager.Instance.ParticlesParent);
        transform.localScale = Vector3.one;
    }

    public void Play(bool destroy)
    {
        transform.position = linkedBeing.position + Vector3.back*0.5f;
        Particles.Emit(Random.Range(minParticlesInEmission, maxParticlesInEmission));
        if (destroy)
        {
            Destroy(gameObject, 1.0f);
        }
    }
}