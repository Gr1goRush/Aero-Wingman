using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMainPanel : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider fuelAmountSlider;

    void Start()
    {
        GameController.Instance.OnScoreChanged += SetScoreText;
        FuelController.Instance.FuelAmountUpdate += SetFuelAmount;

        SetScoreText();
        SetFuelAmount(1f);
    }

    void SetScoreText()
    {
        scoreText.text = GameController.Instance.CurrentScore.ToString();
    }

    void SetFuelAmount(float amount)
    {
        fuelAmountSlider.value = amount;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnScoreChanged -= SetScoreText;
        FuelController.Instance.FuelAmountUpdate -= SetFuelAmount;
    }
}