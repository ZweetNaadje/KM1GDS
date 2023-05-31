using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Plane = UnityEngine.Plane;

namespace Player_Scripts
{
    public class TurretController : MonoBehaviour
    {
        //  e.g. 0.2 = slow, 0.8 = fast, 2 = very fast, Infinity = instant
        [Tooltip("If rotationSpeed == 0.5, then it takes 2 seconds to spin 180 degrees")]
        [Range(0, 10)]
        [SerializeField] private float _rotationSpeed = 0.5f;

        [Tooltip("If isInstant == true, then rotationalSpeed == Infinity")] 
        [SerializeField] private bool _isInstant = false;

        [SerializeField] private Camera _camera;

        private Quaternion _targetRotation = Quaternion.identity;
        private Vector3 _lastRecordedMousePosition;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            InstantRotation();
        }

        private void UpdateMouse()
        {
            // this creates a horizontal plane passing through this object's center
            Plane plane = new Plane(Vector3.up, transform.position);
            Vector3 mouseDelta = Input.mousePosition - _lastRecordedMousePosition;

            if (Mathf.Abs(mouseDelta.x) <= 0)
            {
                return;
            }

            Debug.Log(mouseDelta);

            // create a ray from the mousePosition
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            var screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);

            // This prevents camera rotation when mouse is in 100 pixels circle in screen center.
            if ((Input.mousePosition - screenCenter).magnitude < 100f)
            {
                return;
            }

            // plane.Raycast returns the distance from the ray start to the hit point
            if (plane.Raycast(ray, out var distance))
            {
                var hitPoint = ray.GetPoint(distance);
                Debug.DrawLine(transform.position, hitPoint, Color.green);

                var lookAt = (hitPoint - transform.position).normalized;

                _targetRotation = Quaternion.LookRotation(lookAt, Vector3.up);
                _lastRecordedMousePosition = Input.mousePosition;
            }
        }


        private void InstantRotation()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            // Copy the ray's direction
            Vector3 mouseDirection = ray.direction;

            // Constrain it to stay in the X/Z plane    
            mouseDirection.y = 0;

            //Look for the constraint direction
            Quaternion targetRotation = Quaternion.LookRotation(mouseDirection);

            var screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f + 200);

            Debug.Log($"mousepos: {Input.mousePosition}");
            Debug.Log($"screenCenter: {screenCenter}");
            Debug.Log($"calculation (should be able to go below 100): {(Input.mousePosition - screenCenter).magnitude}");
            
            // This prevents camera rotation when mouse is in 100 pixels circle in screen center.
            if ((Input.mousePosition - screenCenter).magnitude < 100)
            {
                return;
            }

            if (_isInstant)
            {
                transform.rotation = targetRotation;
            }
            else
            {
                Quaternion currentRotation = transform.rotation;
                float angularDifference = Quaternion.Angle(currentRotation, targetRotation);

                // will always be positive (or zero)
                if (angularDifference > 0)
                {
                    transform.rotation = Quaternion.Slerp(
                        currentRotation,
                        targetRotation,
                        (_rotationSpeed * 180 * Time.deltaTime) / angularDifference
                    );
                }

                else
                {
                    transform.rotation = targetRotation;
                }
            }
        }
    }
}

//if mouse is x units away from screenwidth / 2,
//rotate
        
//rotate to new position
//Did the mouse position change since the last frame
//find position
//raycast to new position
//When mouse moved