using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderChooseBtn : MonoBehaviour
{
    //config params
    [SerializeField] List<GameObject> defenderTypes;
    int currentIndex;

    public delegate void OnPlayerSpawned(int index);
    public static event OnPlayerSpawned onPlayerSpawned;

    //cashed reference
    SpriteRenderer spriteRenderer;
    GameObject currentSprite;
    Color turnedOffColor;
    [SerializeField] List<Defender> defenders;

    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        turnedOffColor = spriteRenderer.color;
        SetCostForEachDefender();
    }

    private void OnMouseDown()
    {
        SetButtonActive();
    }

    private void ResetAllButtons()
    {
        foreach (GameObject defender in defenderTypes)
        {
            defender.GetComponent<SpriteRenderer>().color = turnedOffColor;
        }
    }

    private void SetCostForEachDefender()
    {
        for (int i = 0; i < defenderTypes.Count; i++)
        {
            Text costLabel = transform.GetChild(i)?.transform.GetChild(0)?.GetComponentInChildren<Text>();
            if (!costLabel) { Debug.LogError("Defenders Buttons Are Wrong"); }
            costLabel.text = defenders[i].GetDefenderStarCost().ToString();
        }
    }

    private void SetButtonActive()
    {
        Vector2 xPos = new Vector2(Input.mousePosition.x, 0);
        Vector2 xPosWorld = Camera.main.ScreenToWorldPoint(xPos);
        int xPosRounded = Mathf.RoundToInt(xPosWorld.x) - 2;

        if (xPosRounded > transform.childCount || xPosRounded <= 0) { return; }

        ResetAllButtons();

        currentSprite = transform.GetChild(xPosRounded - 1).gameObject;
        currentSprite.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 200);
        currentIndex = xPosRounded - 1;
        DetermineCurrentIndex(currentIndex);
    }

    private void DetermineCurrentIndex(int index)
    {
        onPlayerSpawned?.Invoke(index);
    }
}
