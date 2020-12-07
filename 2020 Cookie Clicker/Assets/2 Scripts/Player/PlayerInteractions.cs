using UnityEngine;
using System;

public class PlayerInteractions : MonoBehaviour
{
    public static Action OnCookieClicked = delegate {};

    [Header("Cashed References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private DialogueManager dialogueManager;
    private Transform hitTransform;

    [Header("Parameters")]
    [SerializeField] private string cookie = "Cookie";
    private Vector2 mousePos2D;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos2D.x = mousePos.x;
            mousePos2D.y = mousePos.y;
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            hitTransform = hit.transform;
            if (hitTransform != null)
            {
                if (hitTransform.gameObject.CompareTag(cookie))
                {
                    OnCookieClicked();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.EndDialogue();
        }
    }
}
