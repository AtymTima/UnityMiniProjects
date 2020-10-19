using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    //cashed reference
    Image manaBarImage;
    RawImage manaBarRawImage;
    Rect uvRect;
    Mana mana;
    RectTransform edgeRectTransfrom;

    bool isManaUsed;
    float speedOfMovingBar = 0.5f;
    float barMaskWidth;
    float currentManaNormalized;

    private void Awake()
    {
        manaBarImage = transform.Find("Bar").GetComponent<Image>();
        manaBarRawImage = transform.Find("Bar").Find("Mana Bar Filler").GetComponent<RawImage>();
        edgeRectTransfrom = transform.Find("Bar").Find("Edge").GetComponent<RectTransform>();

        mana = new Mana();
        manaBarImage.fillAmount = mana.GetManaNormalized();
        barMaskWidth = manaBarImage.rectTransform.rect.width;
    }

    private void Update()
    {
        if (!isManaUsed)
        {
            mana.RestoreMana();
        }
        else
        {
            mana.ReduceMana();
        }

        MovingBarAnimation();

        manaBarImage.fillAmount = mana.GetManaNormalized();
    }

    private void MovingBarAnimation()
    {
        uvRect = manaBarRawImage.uvRect;
        uvRect.x += speedOfMovingBar * Time.deltaTime;
        manaBarRawImage.uvRect = uvRect;

        currentManaNormalized = mana.GetManaNormalized();
        edgeRectTransfrom.anchoredPosition = new Vector2(barMaskWidth * currentManaNormalized, 0);
        edgeRectTransfrom.gameObject.SetActive(currentManaNormalized < 1f);
    }

    public void IsManaUsed(bool used)
    {
        isManaUsed = used;
    }

}
