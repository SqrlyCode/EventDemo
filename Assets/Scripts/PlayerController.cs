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

    private Action _leftClickAction;
    private Action _rightClickAction;
    private Action _leftShiftAction;
    
    private Vector2 _mouseWorldPos;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _leftClickAction = ShootAtPosition;
        _rightClickAction = TeleportToPosition;
        _leftShiftAction = ToggleRunning;
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
            _leftClickAction();
        
        //Rightclick
        if (Input.GetKeyDown(KeyCode.Mouse1))
            _rightClickAction();
        
        //Shiftclick
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _leftShiftAction();
        
        _shootTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "InputShuffle")
        {
            Destroy(other.gameObject);
            ShuffleInput();
        }
    }


    public void ShootAtPosition()
    {
        if (_shootTimer > _shootCooldown)
        {
            _shootTimer = 0;
            Vector2 shootOrigin = (Vector2)transform.position + (Vector2)transform.up * 0.5f;
            GameObject newProjectileGo = Instantiate(_projectileGo, shootOrigin, Quaternion.identity);
            newProjectileGo.transform.up = _mouseWorldPos - (Vector2)transform.position;
        }
    }

    private void TeleportToPosition()
    {
        transform.position = _mouseWorldPos;
    }

    private void ToggleRunning()
    {
        _isRunning = !_isRunning;
    }

    private void ShuffleInput()
    {
        List<Action> actionList = new List<Action>();
        actionList.Add(ShootAtPosition);
        actionList.Add(TeleportToPosition);
        actionList.Add(ToggleRunning);
        List<Action> randomizedActionList = actionList.OrderBy((x) => Random.Range(0, 2)).ToList();

        _leftClickAction = randomizedActionList[0];
        _rightClickAction = randomizedActionList[1];
        _leftShiftAction = randomizedActionList[2];
    }
}