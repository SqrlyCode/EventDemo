using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private GameManager _gm;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
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
        if (this != null)
            Destroy(gameObject);
    }
}