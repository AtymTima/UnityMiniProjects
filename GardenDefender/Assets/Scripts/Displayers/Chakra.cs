using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chakra : MonoBehaviour
{
    [SerializeField] ChakraDIsplayer chakraDisplayer;
    [SerializeField] int currentChakraAmount;
    [SerializeField] int initialChakra = 80;

    public delegate void OnChakraChanged(int updatedChakra);
    public static event OnChakraChanged onChakraChanged = delegate { };


    private void OnEnable()
    {
        PlayerSpawner.onDefenderCreated += ReduceChakra;
        ShyHealer.onChakraReceived += AddToChakra;
    }

    private void OnDisable()
    {
        PlayerSpawner.onDefenderCreated -= ReduceChakra;
        ShyHealer.onChakraReceived -= AddToChakra;
    }

    private void Start()
    {
        UpdateChakra(initialChakra);
    }

    private void AddToChakra(int chakraReceived)
    {
        UpdateChakra(chakraReceived);
    }

    private void ReduceChakra(int cost, float health)
    {
        UpdateChakra(-cost);
    }

    private void UpdateChakra(int chakra)
    {
        currentChakraAmount += chakra;
        ChangeChakra();
        chakraDisplayer.UpdateChakraDisplayer(currentChakraAmount);
    }

    private void ChangeChakra()
    {
        onChakraChanged?.Invoke(currentChakraAmount);
    }
}
