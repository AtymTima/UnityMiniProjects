using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float speedOfSpin = 360;

    private void Start()
    {
        if (Random.value < 0.5f)
        {
            speedOfSpin *= -1;
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
