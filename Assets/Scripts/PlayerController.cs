using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _shootCooldown = 0.3f;
    [SerializeField] private GameObject _projectileGo;

    private float _shootTimer;
    private Rigidbody2D _rb;
    private bool _isRunning;

    private Vector2 _mouseWorldPos;
    private GameManager _gameManager;
    private MyTween _myTween;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gameManager = FindObjectOfType<GameManager>();
        _myTween = GetComponent<MyTween>();
    }

    void Update()
    {
        //Movement
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(horInput, vertInput);
        _rb.velocity = moveInput * _moveSpeed;
        if (_isRunning)
            _rb.velocity *= 2f;

        //Rotate towards mouse
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = _mouseWorldPos - _rb.position;
        transform.up = dir;

        //LeftClick
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();

        //Rightclick
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Teleport();

        //Shiftclick
        if (Input.GetKeyDown(KeyCode.LeftShift))
            ToggleRunning();

        _shootTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InputShuffle inputShuffle = other.GetComponent<InputShuffle>();

        if (inputShuffle != null)
        {
            inputShuffle.Die();
            ShuffleInput();
        }

        Circle circle = other.GetComponent<Circle>();
        if (circle != null)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Shoot()
    {
        if (_shootTimer > _shootCooldown)
        {
            _shootTimer = 0;
            Vector2 shootOrigin = (Vector2)transform.position + (Vector2)transform.up * 0.5f;
            GameObject newProjectileGo = Instantiate(_projectileGo, shootOrigin, Quaternion.identity);
            newProjectileGo.transform.up = _mouseWorldPos - (Vector2)transform.position;
        }
    }
    
    private void Teleport()
    {
        //TODO: Use Callbacks to animate the Teleport
        //----------------------------------------------------------------------------------------------
        // StartCoroutine(_myTween.AnimatedScaleCR(Vector3.zero, 0.15f));
        transform.position = _mouseWorldPos;
        //----------------------------------------------------------------------------------------------
    }

    private void ToggleRunning()
    {
        _isRunning = !_isRunning;
    }

    private void ShuffleInput()
    {
        //TODO: Shuffle the input
    }
}