using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<Enemy> Enemies = new List<Enemy>();

    private void Start()
    {
        var allEnemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in allEnemies)
        {
            Enemies.Add(enemy);
        }
    }

    private void Update()
    {
        if (Enemies.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
            Debug.Log("You won the game!");
        }
    }
}