using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Path;
using UnityEngine;

public class TriangleProjectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;

    private GameManager _gm;
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _gm.gameOver += GameManager_GameOver;
    }

    private void OnDisable()
    {
        if(_gm != null)
            _gm.gameOver -= GameManager_GameOver;
    }


    private void Start()
    {
        //Must be ins tart because Projectile-rotation is not yet set in Awake.
        _rb.velocity = transform.up * _moveSpeed;
    }

    private void GameManager_GameOver(int score)
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Destroy(gameObject);
        Circle otherCircle = otherCollider.GetComponent<Circle>();
        if(otherCircle != null)
            Destroy(otherCircle.gameObject);
    }
}