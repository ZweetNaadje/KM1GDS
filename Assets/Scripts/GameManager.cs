using System;
using System.Collections.Generic;
using Enemy_Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<Enemy> Enemies = new List<Enemy>();

    private void Update()
    {
        if (Enemies.Count <= 0)
        {
            //Show end screen
            Debug.Log("You won the game!");
        }
    }
}