using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _shootCooldown = 0.3f;
    [SerializeField] private GameObject _projectileGo;

    private float _shootTimer;
    private Rigidbody2D _rb;

    private Action<Vector2> _leftClickAction;
    private Action<Vector2> _rightClickAction;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _leftClickAction = ShootAtPosition;
        _rightClickAction = TeleportToPosition;
    }

    void Update()
    {
        //Movement
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(horInput, vertInput);
        _rb.velocity = moveInput * _moveSpeed;

        //Rotate towards mouse
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorldPos - _rb.position;
        transform.up = dir;

        //LeftClick
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _leftClickAction(mouseWorldPos);
        
        //Rightclick
        if (Input.GetKeyDown(KeyCode.Mouse1))
            _rightClickAction(mouseWorldPos);
        
        
        _shootTimer += Time.deltaTime;
    }
    
    
    public void ShootAtPosition(Vector2 position)
    {
        if (_shootTimer > _shootCooldown)
        {
            _shootTimer = 0;
            Vector2 shootOrigin = (Vector2)transform.position + (Vector2)transform.up * 0.5f;
            GameObject newProjectileGo = Instantiate(_projectileGo, shootOrigin, Quaternion.identity);
            newProjectileGo.transform.up = position - (Vector2)transform.position;
        }
    }

    private void TeleportToPosition(Vector2 position)
    {
        transform.position = position;
    }
}