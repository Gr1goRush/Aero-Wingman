using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HoopsController : Singleton<HoopsController>
{
    [Space]
    [SerializeField] private Hoop hoop;

    [SerializeField] private Vector3 startHoopPosition;
    [SerializeField] private int startHoopsCount = 10;
    [SerializeField] private Vector3RandomInterval hoopsDistanceInterval;

    [Space]
    [SerializeField] private int maxNotFuelHoopsCount = 3;
    [SerializeField] private float fuelHoopRate = 0.4f;

    [Space]
    [SerializeField] private Color fuelHoopColor;

    [SerializeField] private Color[] colors;

    private int notFuelHoopsCount = 0;

    private List<Hoop> hoops;

    public void StartSpawning()
    {
        hoop.Init();
        for (int i = 0; i < startHoopsCount; i++)
        {
            SpawnHoop();
        }
    }

    private void SpawnHoop()
    {
        if (hoops == null)
        {
            hoops = new List<Hoop>();
        }
        Vector3 lastHoopPos = hoops.Count == 0 ? startHoopPosition : hoops[hoops.Count - 1].transform.position;

        Hoop newHoop = hoop.Pull();
        newHoop.transform.position = lastHoopPos + hoopsDistanceInterval.Next();
        newHoop.transform.rotation = Quaternion.LookRotation(newHoop.transform.position - lastHoopPos);

        bool isFuel = Random.Range(0f, 1f) <= fuelHoopRate;
        if (!isFuel)
        {
            notFuelHoopsCount++;
            if(notFuelHoopsCount > maxNotFuelHoopsCount)
            {
                isFuel = true;
                notFuelHoopsCount = 0;
            }
        }
        else
        {
            notFuelHoopsCount = 0;
        }

        newHoop.SetDefault(isFuel);

        Color hoopColor = isFuel ? fuelHoopColor : colors[Random.Range(0, colors.Length)];
        newHoop.SetBorderColor(hoopColor);

        hoops.Add(newHoop);
    }

    public void OnHoopCompleted(bool hit, Hoop _hoop)
    {
        if (hit)
        {
            if (_hoop.IsFuel)
            {
                FuelController.Instance.FillFull();
            }
            else
            {
                GameController.Instance.AddScore();
            }

            SoundsController.Instance.PlaySuccess();
        }
        else
        {
            GameController.Instance.Lose();
        }

        hoops.Remove(hoop);
        hoop.Unpull(_hoop);

        SpawnHoop();
    }
}
