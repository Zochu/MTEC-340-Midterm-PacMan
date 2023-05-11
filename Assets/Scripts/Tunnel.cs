using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] Transform direction;

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = direction.transform.position;
    }
}
