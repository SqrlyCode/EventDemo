using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private GameObject _deathParticlesPrefab;


    private GameManager _gm;
    private MyTween _myTween;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _myTween = GetComponent<MyTween>();
    }

    private void OnEnable()
    {
        _gm.gameOver += GameManager_GameOver;
    }

    private void OnDisable()
    {
        if (_gm != null)
            _gm.gameOver -= GameManager_GameOver;
    }

    private void GameManager_GameOver(int score)
    {
        Die();
    }

    public void Die()
    {
        _gm._score++;
        
        //TODO: use callbacks
        //----------------------------------------------------------------------------------------------
        StartCoroutine(_myTween.AnimatedScaleCR(Vector3.zero, 0.4f));
        Destroy(gameObject);
        Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
        //----------------------------------------------------------------------------------------------
    }
}