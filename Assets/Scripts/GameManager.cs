using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int _score;

    [SerializeField] private int _amountOfCirclesToSpawn = 10;
    [SerializeField] private Rect _spawnArea;
    [SerializeField] private GameObject _circlePrefab;

    //Raises event where the score is the parameter
    public event Action<int> gameOver;

    void Start()
    {
        for (int i = 0; i < _amountOfCirclesToSpawn; i++)
            SpawnCircle();
    }

    public void InvokeGameOver()
    {
        gameOver?.Invoke(_score);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }

    private void SpawnCircle()
    {
        Vector2 rndPos = new Vector2(_spawnArea.x + Random.Range(-_spawnArea.width / 2, _spawnArea.width / 2), _spawnArea.y + Random.Range(-_spawnArea.height / 2, _spawnArea.height / 2));
        Instantiate(_circlePrefab, rndPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawCube(_spawnArea.position, new Vector3(_spawnArea.width, _spawnArea.height, 0.1f));
    }
}