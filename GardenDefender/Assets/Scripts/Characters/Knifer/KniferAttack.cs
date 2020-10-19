using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KniferAttack : MonoBehaviour
{
    //config params
    [SerializeField] KniferCollision kniferCollision;

    EnemySpawner enemySpawnerOnMyLine;
    Animator animator;
    float damagePerAttack;
    [SerializeField] float timeBeforeChecking = 3f;

    public delegate void OnKnifeAttacked(int damage);
    public static event OnKnifeAttacked onKnifeAttacked = delegate { };

    //cashed reference
    EnemySpawner[] enemySpawners;
    Coroutine attackingCoroutine;
    Health healthComponent;
    DamageDealer damageDealer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemySpawners = FindObjectsOfType<EnemySpawner>();
        healthComponent = GetComponent<Health>();
        damageDealer = GetComponent<DamageDealer>();
    }

    private void Start()
    {
        damagePerAttack = damageDealer.GetDamage();
    }

    public void AttackWithKnife(int damage)
    {
        kniferCollision.DamageOpponent(damagePerAttack);
    }

    public Health GetHealthComponent()
    {
        return healthComponent;
    }


    //public void KnifeAttacked(int damagePerAttack)
    //{
    //    onKnifeAttacked?.Invoke(this.damagePerAttack);
    //}

    public void ChangeState(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }
}
