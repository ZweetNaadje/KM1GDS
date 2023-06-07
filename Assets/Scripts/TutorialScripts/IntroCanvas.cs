using System;
using Player_Scripts;
using TMPro;
using UnityEngine;

namespace TutorialScripts
{
    public class IntroCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _introText;

        private void Start()
        {
            _introText.enabled = false;
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Stay");
        
            var player = other.GetComponent<Player>();

            if (player)
            {
                _introText.enabled = true;
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit");

            var player = other.GetComponent<Player>();

            if (player)
            {
                _introText.enabled = false;
            }
        }
    }
}
