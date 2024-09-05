using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 planeOffset;
    [SerializeField] private float positionFollowSpeed = 1f;

    private Plane plane;

    void LateUpdate()
    {
        if (plane == null)
        {
            plane = GameController.Instance.Plane;
        }

        Vector3 target = plane.transform.position;
        target += plane.transform.forward * planeOffset.z;
        target += plane.transform.up * planeOffset.y;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * positionFollowSpeed);

        transform.position = target;

        Vector3 planeEulers = plane.transform.rotation.eulerAngles;

        planeEulers.z = 0f;
        transform.rotation = Quaternion.Euler(planeEulers);

    }
}
