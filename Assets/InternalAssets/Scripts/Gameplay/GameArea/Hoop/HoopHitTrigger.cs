using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopHitTrigger : MonoBehaviour
{
    [SerializeField] private Hoop owner;

    private void OnTriggerEnter(Collider other)
    {
        owner.OnHitEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        owner.OnHitExit(other);
    }
}
