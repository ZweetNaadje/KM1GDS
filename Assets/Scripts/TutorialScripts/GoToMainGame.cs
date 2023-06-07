using System.Collections;
using System.Collections.Generic;
using Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.GetComponent<Player>();
        
        if (player)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
