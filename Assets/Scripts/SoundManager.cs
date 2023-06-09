using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _audioSource.Play();
    }
}