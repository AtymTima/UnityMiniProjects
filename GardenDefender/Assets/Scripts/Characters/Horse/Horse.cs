using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    // congif params
    [SerializeField] HorseCollider horseCollider;
    //float health;
    int randomSpawnSound;
    GameObject currentTarget;

    //cashed reference
    Animator animator;
    DamageDealer damageDealer;
    Health healthComponent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
        healthComponent = GetComponent<Health>();
    }

    private void RandomSpawnSound()
    {
        randomSpawnSound = Random.Range(1, 3);
        switch (randomSpawnSound)
        {
            case 1:
                MyAudioManager.instance.PlayAudio("HorseSpawn1", false, 0, 0);
                break;
            case 2:
                MyAudioManager.instance.PlayAudio("HorseSpawn2", false, 0, 0);
                break;
            case 3:
                MyAudioManager.instance.PlayAudio("HorseSpawn3", false, 0, 0);
                break;
        }
    }

    public void ChangeState(bool isAttacking, GameObject target)
    {
        animator.SetBool("isAttacking", isAttacking);
        currentTarget = target;
    }

    public void OnAttack()
    {
        horseCollider.DamageOpponent(damageDealer.GetDamage());
    }

    public Health GetHealthComponent()
    {
        return healthComponent;
    }
}
