using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConstantRotatingTransform : MonoBehaviour
{
    [SerializeField] private Vector3 rotateSpeed;

    private Vector3 eulers;

    private void Start()
    {
        eulers = transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        eulers += rotateSpeed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(eulers);
    }
}
