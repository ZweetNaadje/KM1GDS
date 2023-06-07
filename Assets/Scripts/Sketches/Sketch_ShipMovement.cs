using System;
using UnityEngine;

namespace Sketches
{
    public class Sketch_ShipMovement : MonoBehaviour
    {
        public float turnAngSpeed = 0.4f; // direction changing speed
        public float forwardSpeed = 40f; // full forward speed
        private float turnForwardSpeed; // forward speed while turning
        private Vector2 forward;
        public Transform targetPosition;

        private void Start()
        {
            turnForwardSpeed = forwardSpeed * 0.6f;
        }

        private void Update()
        {
            forward = transform.forward;
            Vector2 dir = targetPosition.position - transform.position; // ship --> target direction (Vector2)
            float angle = Vector2.SignedAngle(dir, forward); // angle between target direction and current forward ship vector
            if (angle < 0)
            {
                angle += 360f; // some workaround to have all positive values
            }

            if (angle > 0.05f) // if angle difference
            {
                float rotationAmount = turnAngSpeed * Time.deltaTime;
                if (angle < 180f)
                {
                    // turn right
                    forward = Quaternion.Euler(0f, 0f, rotationAmount * Mathf.Rad2Deg) * forward;
                }
                else
                {
                    // turn left
                    forward = Quaternion.Euler(0f, 0f, -rotationAmount * Mathf.Rad2Deg) * forward;
                }

                // apply turnForwardSpeed
                float moveAmount = turnForwardSpeed * Time.deltaTime;
                transform.position += (Vector3)(forward * moveAmount);
            }
            else
            {
                // apply forwardSpeed
                float moveAmount = forwardSpeed * Time.deltaTime;
                transform.position += (Vector3)(forward * moveAmount);
            }
        }
    }
}