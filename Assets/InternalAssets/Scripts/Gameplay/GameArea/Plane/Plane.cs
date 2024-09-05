using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private bool flying = false;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float flyingForce;

    [SerializeField] private Vector3 rotationSpeed, maxRotation;

    private Vector3 currentAngles;

    public void StartFlying()
    {
        gameObject.SetActive(true);

        flying = true;
    }

    public void StopFlying()
    {
        gameObject.SetActive(false);

        flying = false;
    }

    private void FixedUpdate()
    {
        float horizontal = InputController.Instance.Horizontal;
        float vertical = InputController.Instance.Vertical;

        if (flying)
        {
            _rigidbody.velocity = transform.forward * flyingForce;
        }

        Vector3 eulers = transform.rotation.eulerAngles;

        if (vertical != 0)
        {
            currentAngles.x = Mathf.Clamp(currentAngles.x - (rotationSpeed.x * Time.fixedDeltaTime * vertical), -maxRotation.x, maxRotation.x);
            eulers.x = currentAngles.x;
        }

        float zDir;

        if (horizontal != 0)
        {
            currentAngles.y += rotationSpeed.y * Time.fixedDeltaTime * horizontal;
            eulers.y = currentAngles.y;

            zDir = horizontal;
        }
        else if(currentAngles.z != 0f)
        {
            zDir = Mathf.Sign(currentAngles.z);
        }
        else
        {
            zDir = 0f;
        }

        if (zDir != 0f)
        {
            float offset = rotationSpeed.z * Time.fixedDeltaTime * zDir;
            currentAngles.z = Mathf.Clamp(currentAngles.z - offset, -maxRotation.z, maxRotation.z);
            if(Mathf.Abs(eulers.z - currentAngles.z) > offset)
            {
                eulers.z = currentAngles.z;
            }
        }

        transform.rotation = Quaternion.Euler(eulers);
    }
}
