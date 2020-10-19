using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    [SerializeField] private Transform platformTransform;
    [SerializeField] private float magnitudeOfPush = 300f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        var force = platformTransform.localPosition - collision.transform.localPosition;
        force.Normalize();
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force * magnitudeOfPush);
    }

}
