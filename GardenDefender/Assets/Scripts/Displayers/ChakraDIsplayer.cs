using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChakraDIsplayer : MonoBehaviour
{
    //congif params
    [SerializeField] Chakra chakra;
    int currentChakraToDisplay;

    //cashed reference
    Text chakraText;

    private void Awake()
    {
        chakraText = GetComponent<Text>();
    }

    private void Start()
    {
        //chakraText.text = 0.ToString();
    }

    public void UpdateChakraDisplayer(int newChakra)
    {
        chakraText.text = newChakra.ToString();
    }
}
