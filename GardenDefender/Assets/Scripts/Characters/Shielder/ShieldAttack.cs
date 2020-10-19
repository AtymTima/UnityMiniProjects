using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    //config params
    EnemySpawner enemySpawnerOnMyLine;
    Animator animator;

    public delegate void OnShieldAttacked(Transform transform);
    public static event OnShieldAttacked onShieldAttacked = delegate { };

    //cashed reference
    EnemySpawner[] enemySpawners;
    Coroutine attackCoroutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }

    private void OnEnable()
    {
        EnemySpawner.onEnemySpawned += IsEnemyOnTheSameLevel;
        Health.onEnemyDied += CheckAgainWhenEnemyDied;
    }

    private void OnDisable()
    {
        EnemySpawner.onEnemySpawned -= IsEnemyOnTheSameLevel;
        Health.onEnemyDied -= CheckAgainWhenEnemyDied;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    private void Start()
    {
        //while (enemySpawnerOnMyLine)
        IsEnemyOnTheSameLevel(0);
    }

    public void ShieldAttacked(Transform transform)
    {
        onShieldAttacked?.Invoke(this.transform);
    }

    private void CheckAgainWhenEnemyDied(GameObject character)
    {
        IsEnemyOnTheSameLevel(0);
    }

    private void IsEnemyOnTheSameLevel(float health)
    {
        //if (attackCoroutine == null)
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            bool isCloseEnough = Mathf.Abs(transform.position.y - enemySpawner.transform.position.y) <= Mathf.Epsilon;
            if (isCloseEnough)
            {
                enemySpawnerOnMyLine = enemySpawner;
            }
        }
        attackCoroutine = StartCoroutine(WhileEnemyOnLine());
    }

    private IEnumerator WhileEnemyOnLine()
    {
        while (IsEnemyOnMyLine())
        {
            animator.SetBool("IsEnemyOnLine", true);
            yield return null;
        }
            animator.SetBool("IsEnemyOnLine", false);
    }

    private bool IsEnemyOnMyLine()
    {
        if (enemySpawnerOnMyLine.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }
}
