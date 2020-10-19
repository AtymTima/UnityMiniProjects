using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesGround : MonoBehaviour
{
    [SerializeField] ParticlesMovement particlesMovement;

    private void Awake()
    {
        particlesMovement.moveParticles();
    }
}
