using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    // congif params
    [SerializeField] RunnerCollision runnerCollision;

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

    public void ChangeState(bool isAttacking, GameObject target)
    {
        animator.SetBool("isAttacking", isAttacking);
        currentTarget = target;
    }

    public void OnAttack()
    {
        runnerCollision.DamageOpponent(damageDealer.GetDamage());
    }

    public Health GetHealthComponent()
    {
        return healthComponent;
    }
}
