using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    void Update()
    {
        if (_playerController != null)
        {
            Vector3 newPos = _playerController.transform.position;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }
}
