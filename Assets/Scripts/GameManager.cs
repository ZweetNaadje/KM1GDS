using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<Enemy> Enemies = new List<Enemy>();

    [SerializeField] private string _sceneName;
    
    private void Start()
    {
        var allEnemies = FindObjectsOfType<Enemy>(true);

        foreach (var enemy in allEnemies)
        {
            Enemies.Add(enemy);
        }
    }

    private void Update()
    {
        if (Enemies.Count <= 0)
        {
            SceneManager.LoadScene(_sceneName);
            Debug.Log("You won the game!");
        }
    }
}