using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private GameObject _gameOverPanel;

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
        if(_gm != null)
            _gm.gameOver -= GameManager_GameOver;
    }

    private void GameManager_GameOver(int score)
    {
        _scoreTxt.text = score.ToString();
        _gameOverPanel.SetActive(true);
    }
}
