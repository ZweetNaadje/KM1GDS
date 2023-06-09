using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f,0f,6f* 180f * Time.deltaTime);
    }
}
