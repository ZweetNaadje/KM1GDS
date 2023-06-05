using System;
using System.Collections;
using Player_Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace Sketches
{
    public class AiCannonLogic : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Shoot _shoot;
        //[SerializeField] private Transform _turretTransform;

        private void Update()
        {
            EngagePlayer();
        }

        private void EngagePlayer()
        {
            if (_player == null)
            {
                return;
            }

            if (!_player.IsBurrowed)
            {
                var point = _player.transform.position;
                //var cannon = _turretTransform.rotation.x;

                //cannon = 0f;
                
                point.y = 0f;
                transform.LookAt(point);
                _shoot.ShootCannon();
            }
        }

       
    }
}