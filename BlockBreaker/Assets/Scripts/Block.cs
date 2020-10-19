using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] damageSprites;

    //cashed references
    Level level;
    GameStatus gameStatus;

    //state params
    [SerializeField] int timesHits; //For debugging

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit();
    }

    private void HandleHit()
    {
        timesHits++;
        if (tag == "Breakable" && timesHits == maxHits)
        {
            DestroyBlock();
        }
        else
        {
            if (tag != "Unbreakable")
            {
                ChangeDamageSprite();
            }
        }
    }

    private void ChangeDamageSprite()
    {
        int indexDamage = timesHits;
        GetComponent<SpriteRenderer>().sprite = damageSprites[indexDamage];
    }

    private void DestroyBlock()
    {
        removeBlockAndAudio();
        level.CountBrokenBlocks();
        gameStatus.AddToScore();
        TriggerSparklesVFX();
    }

    private void removeBlockAndAudio()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(breakSound, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y));
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position = transform.position, transform.rotation = transform.rotation);
        Destroy(sparkles, 1f);
    }
}
