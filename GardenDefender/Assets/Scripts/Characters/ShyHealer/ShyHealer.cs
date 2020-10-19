using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyHealer : MonoBehaviour
{
    //Config params
    [SerializeField] ShyHealerCollision shyHealerCollision;
    float healthHealer;

    public delegate void OnChakraReceived(int chakra);
    public static event OnChakraReceived onChakraReceived = delegate { };


    public void ChakraReceived(int chakra)
    {
        onChakraReceived?.Invoke(chakra);
    }
}
