using System.Collections.Generic;
using Enemy_Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TutorialScripts
{
    public class ShowEnemies : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private GameObject[] _gameObjects;
        [SerializeField] private AudioSource _audioSource;

        private bool _isPlaying;

        private void Start()
        {
            _textMeshProUGUI.enabled = false;
        }

        private void Update()
        {
            if (_enemy.Health <= 0)
            {
                _textMeshProUGUI.enabled = true;

                if (!_isPlaying)
                {
                    _audioSource.Play();
                    _isPlaying = true;
                }
                
                foreach (var enemy in _gameObjects)
                {
                    Debug.Log("should activate enemies");
                    enemy.gameObject.SetActive(true);
                }
            }
        }
    }
}