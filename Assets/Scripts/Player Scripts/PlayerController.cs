using System;
using System.Collections;
using System.Collections.Generic;
using Player_Scripts;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// This script is solely responsible for player input. No actual player logic will be performed here.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Shoot _shoot;

    // Update is called once per frame
    void Update()
    {
        _player.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.ToggleBurrowed();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _shoot.Shooting();
        }
    }
}