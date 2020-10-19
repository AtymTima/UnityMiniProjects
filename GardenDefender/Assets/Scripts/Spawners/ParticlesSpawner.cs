using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawner : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] GameObject groundParticlesPrefab;

    GameObject AllParticles;
    const string ALL_PARTICLES_NAME = "All Particles";

    private void Awake()
    {
        AllParticles = new GameObject(ALL_PARTICLES_NAME);
    }

    private void OnEnable()
    {
        ShieldAttack.onShieldAttacked += SummonGroundParticles;
    }

    private void OnDisable()
    {
        ShieldAttack.onShieldAttacked -= SummonGroundParticles;
    }

    private void SummonGroundParticles(Transform shieldTransform)
    {
        var particlePosition = shieldTransform.transform.GetChild(5).transform.GetChild(0).transform.position;
        GameObject groundParticles = Instantiate(groundParticlesPrefab, particlePosition, Quaternion.identity);
        SelfDestroyParticlesSystem(groundParticles);
    }

    private void SelfDestroyParticlesSystem(GameObject particles)
    {
        var particlesSystem = particles.GetComponent<ParticleSystem>();
        if (particlesSystem != null)
        {
            Destroy(particles.gameObject, particlesSystem.main.duration + particlesSystem.main.startLifetimeMultiplier);
        }
    }
}
