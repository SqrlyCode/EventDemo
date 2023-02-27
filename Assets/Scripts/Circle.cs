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

    public void Die()
    {
        _gm._score++;
        
        //TODO: use callbacks
        //----------------------------------------------------------------------------------------------
        StartCoroutine(_myTween.AnimatedScaleCR(Vector3.zero, 0.2f));
        Destroy(gameObject);
        Instantiate(_deathParticlesPrefab, transform.position, Quaternion.identity);
        //----------------------------------------------------------------------------------------------
    }
}