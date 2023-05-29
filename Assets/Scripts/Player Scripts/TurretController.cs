using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RotateTurret();
    }

    private void RotateTurret()
    {
        // this creates a horizontal plane passing through this object's center
        Plane plane = new Plane(Vector3.up, transform.position);

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
            // some point of the plane was hit - get its coordinates
            var hitPoint = ray.GetPoint(distance);
            Debug.DrawLine(transform.position, hitPoint, Color.green);

            // use the hitPoint to aim your cannon
            var lookAt = (hitPoint - transform.position).normalized;
            // lookAt.x = 0f;
            //lookAt.z = 0f;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(lookAt, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speed);
        }
    }
}