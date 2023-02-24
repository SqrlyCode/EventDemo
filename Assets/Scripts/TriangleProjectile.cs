using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Path;
using UnityEngine;

public class TriangleProjectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;


    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * _moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Destroy(gameObject);
        Circle otherCircle = otherCollider.GetComponent<Circle>();
        if(otherCircle != null)
            Destroy(otherCircle.gameObject);
    }
}