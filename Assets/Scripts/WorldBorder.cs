using System;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class WorldBorder : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<Player>();

            if (player)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}