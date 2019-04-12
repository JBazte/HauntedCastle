using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speedRotate = 0;

    void Update()
    {
       transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
    }
}
