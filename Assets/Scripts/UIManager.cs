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
}
