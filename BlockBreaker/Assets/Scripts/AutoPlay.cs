using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoPlay : MonoBehaviour
{
    //config params
    [SerializeField] TextMeshProUGUI autoPlayBtnText;

    //cashed references
    [SerializeField] bool isAutoPlayEnabled = false;

    public void ChangeAutoPlayMode()
    {
        if (isAutoPlayEnabled)
        {
            isAutoPlayEnabled = false;
            autoPlayBtnText.text = "Manual Play";
        }
        else
        {
            isAutoPlayEnabled = true;
            autoPlayBtnText.text = "Auto Play";
        }
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

}
