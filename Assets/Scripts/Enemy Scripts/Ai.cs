using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = Unity.Mathematics.Random;

public class Ai : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _range; //radius of sphere

    [SerializeField] private Transform _centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Debug.DrawLine(transform.TransformDirection(transform.forward), transform.TransformDirection(transform.forward) * 150, Color.red);

        if (_agent.remainingDistance <= _agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(_centrePoint.position, _range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                _agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;

        Vector3 forward = transform.forward;
        Vector3 toOther = randomPoint - transform.position;


        //TODO: How to contain the selected point within a viewcone??
        //bool validDestination = Vector3.Dot(forward, toOther) > 45f && Vector3.Dot(forward, toOther) < 90f;

        //Debug.Log(validDestination);

        //if (validDestination)

        //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
        //or add a for loop like in the documentation
        result = forward * 150f;
        return true;


        result = Vector3.zero;
        return false;
    }

    private IEnumerator ScanRoutine()
    {
        yield return new WaitForSeconds(1f);
    }
}