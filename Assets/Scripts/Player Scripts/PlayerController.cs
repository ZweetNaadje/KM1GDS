using UnityEngine;

namespace Player_Scripts
{
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
            
            //Enable for persicopetoggle
            /*if (Input.GetMouseButtonDown(1))
            {
                _player.TogglePeriscope();
            }*/

            if (Input.GetMouseButtonDown(0) && !_player.IsBurrowed)
            {
                _shoot.ShootCannon();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _shoot.LaunchTorpedo();
            }

            if (_player.IsBurrowed && Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Align periscope!");
                var periscopeRotation = _player.PeriscopeCamera.transform.rotation;
                periscopeRotation.y = 0.0f;
            }
        }
    }
}