using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] List<Defender> defenders;
    [SerializeField] GameController GameController;
    List<Vector2> busyArea;

    GameObject AllDefenders;
    const string DEFENDER_OBJECT_NAME = "All Defenders";

    int currentDefenderIndex;
    Vector2 defenderPosition;
    int updatedChakra;
    bool isGameStarted;

    public delegate void OnDefenderCreated(int cost, float health);
    public static event OnDefenderCreated onDefenderCreated = delegate { };

    private void Awake()
    {
        busyArea = new List<Vector2>();
        AllDefenders = new GameObject(DEFENDER_OBJECT_NAME);
    }

    private void OnEnable()
    {
        DefenderChooseBtn.onPlayerSpawned += DetermineIndex;
        Chakra.onChakraChanged += CheckIfEnoughChakra;
        Health.onPlayerDied += GetDiedCharacterPos;
    }

    private void OnDisable()
    {
        DefenderChooseBtn.onPlayerSpawned -= DetermineIndex;
        Chakra.onChakraChanged -= CheckIfEnoughChakra;
        Health.onPlayerDied -= GetDiedCharacterPos;
    }

    private void OnMouseDown()
    {
        if (!isGameStarted) { return; }
        defenderPosition = GetDefenderPosition();
        foreach (Vector2 cell in busyArea)
        {
            if (cell == defenderPosition)
            {
                return;
            }
        }

        SpawnDefenderIfPossible(defenderPosition);
    }

    private Vector2 GetDefenderPosition()
    {
        Vector2 mouseClick = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mouseClick);
        return SnapToGrid(mousePositionWorld);
    }

    private Vector2 SnapToGrid(Vector2 mousePositionWorld)
    {
        int newXPos = Mathf.RoundToInt(mousePositionWorld.x);
        float newYPos = Mathf.RoundToInt(mousePositionWorld.y) - 0.75f;
        Vector2 mousePositionInt = new Vector2(newXPos, newYPos);
        return mousePositionInt;
    }

    private void SetAreaCellBusy(Vector2 cellPos)
    {
        busyArea.Add(cellPos);
    }


    private void GetDiedCharacterPos(GameObject character)
    {
        GameObject diedCharacter = character;

        IncrementDiedCharacters(diedCharacter.tag);
        if (diedCharacter.tag == "Enemy") { return; }

        Vector2 characterPos = diedCharacter.transform.position;
        int newXPos = Mathf.RoundToInt(characterPos.x);
        float newYPos = Mathf.RoundToInt(characterPos.y) + 0.25f;
        Vector2 characterPosInt = new Vector2(newXPos, newYPos);
        SetAreaCellFree(characterPosInt);
    }

    private void IncrementDiedCharacters(string characterTag)
    {
        if (characterTag == "Enemy")
        {
            GameController.enemyDestroyed += 1;
            GameController.IsLevelComplete();
        }
    }

    private void SetAreaCellFree(Vector2 cellPos)
    {
        for (int i = 0; i < busyArea.Count; i++)
        {
            if (Mathf.Abs(busyArea[i].x - cellPos.x) <= Mathf.Epsilon && Mathf.Abs(busyArea[i].y - cellPos.y) <= Mathf.Epsilon)
            {
                busyArea.RemoveAt(i);
                return;
            }
        }

        //busyArea.Remove(cellPos);
    }

    private void SpawnDefenderIfPossible(Vector2 spawnPosition)
    {
        var currentDefender = defenders[currentDefenderIndex];
        if (currentDefender.GetDefenderStarCost() <= updatedChakra)
        {
            GameObject player = Instantiate(currentDefender.GetDefenderPrefab(), spawnPosition, Quaternion.identity);
            onDefenderCreated?.Invoke(currentDefender.GetDefenderStarCost(), currentDefender.GetDefenderHealth());
            SetAreaCellBusy(defenderPosition);
            player.transform.parent = AllDefenders.transform;
        }
        else
        {
            //Debug.Log("Not Enough");
        }

    }

    private void DetermineIndex(int currentIndex)
    {
        currentDefenderIndex = currentIndex;
        isGameStarted = true;
    }

    private void CheckIfEnoughChakra(int currentChakra)
    {
        updatedChakra = currentChakra;
    }

}
