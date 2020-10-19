using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesMovement : MonoBehaviour
{
    [SerializeField] ParticlesGround particlesGround;
    [SerializeField] float speedMovement = 1f;

    public void moveParticles()
    {
        float particlesSystemDuration = GetComponent<ParticleSystem>().main.duration;
        float currentTime = Time.time;
        float timeDelta = particlesSystemDuration + currentTime;

        StartCoroutine(moveParticlesRight(timeDelta));
    }

    IEnumerator moveParticlesRight(float timeDelta)
    {
        while (Time.time < timeDelta)
        {
            if (gameObject != null)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speedMovement);
                yield return null;
            }
        }
    }

}
