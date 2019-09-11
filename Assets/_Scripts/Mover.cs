using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    Rigidbody2D rbd;
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        rbd.velocity = Vector3.up * speed;
    }
}
