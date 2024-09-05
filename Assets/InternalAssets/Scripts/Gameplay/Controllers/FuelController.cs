using System;
using UnityEngine;

public class FuelController : Singleton<FuelController>
{
    [SerializeField] private float consumptionOnSecond = 0.05f;

    private float currentAmount = 1f, consumptionOnFixedUpdate;
    private bool isActive = false;

    public event Action<float> FuelAmountUpdate;

    public void Activate()
    {
        consumptionOnFixedUpdate = consumptionOnSecond / (1f / Time.fixedDeltaTime);

        isActive = true;
        currentAmount = 1f;
    }

    void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }

        currentAmount -= consumptionOnFixedUpdate;
        if(currentAmount <= 0f)
        {
            isActive = false;
            GameController.Instance.Lose();
        }

        FuelAmountUpdate?.Invoke(currentAmount);
    }

    public void FillFull()
    {
        currentAmount = 1f;
    }
}