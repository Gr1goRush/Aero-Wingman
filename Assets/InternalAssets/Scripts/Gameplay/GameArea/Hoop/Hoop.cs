using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : PullObject<Hoop>
{
    public bool IsFuel => isFuel;

    [SerializeField] private HoopBorder border;

    private bool hitStarted = false, failed = false, isFuel = false;

    public void SetDefault(bool _fuel)
    {
        failed = false;
        hitStarted = false;
        isFuel = _fuel;
    }

    public void SetBorderColor(Color color)
    {
        border.SetColor(color);
    }

    public void OnHitEnter(Collider collider)
    {
        if (IsPlane(collider))
        {
           hitStarted = true;
        }
    }

    public void OnHitExit(Collider collider)
    {
        if (IsPlane(collider) && hitStarted && !failed)
        {
            hitStarted = false;
            HoopsController.Instance.OnHoopCompleted(true, this);
        }
    }

    public void OnBorderEnter(Collider other)
    {
        if (IsPlane(other) && !failed)
        {
            failed = true;
            hitStarted = false;
            HoopsController.Instance.OnHoopCompleted(false, this);
        }
    }

    private bool IsPlane(Collider collider)
    {
        return collider.gameObject == GameController.Instance.Plane.gameObject || collider.gameObject.TryGetComponent<PlanePart>(out _);
    }
}
