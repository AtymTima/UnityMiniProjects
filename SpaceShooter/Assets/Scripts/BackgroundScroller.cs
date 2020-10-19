using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    //config params
    [SerializeField] float backgroundScrollerSpeed = 2f;
    Material myBackgroundMaterial;
    Vector2 offSetBackground;

    // Start is called before the first frame update
    void Start()
    {
        myBackgroundMaterial = GetComponent<Renderer>().material;
        offSetBackground = new Vector2(0, backgroundScrollerSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myBackgroundMaterial.mainTextureOffset += offSetBackground * Time.deltaTime;
    }
}
