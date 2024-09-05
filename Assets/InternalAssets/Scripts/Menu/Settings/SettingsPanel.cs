using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private CustomToggle soundsToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button colorButtonOriginal;
    [SerializeField] private Transform colorButtonsParent;

    private Button[] colorButtons;

    void Start()
    {
        OnVolumeChanged();

        soundsToggle.onPointerToggled.AddListener(OnSoundsToggleValueChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);

        AudioManager.Instance.OnVolumeChanged += OnVolumeChanged;

        Color[] planeColors = PlaneSkinManager.Instance.Colors;
        colorButtons = new Button[planeColors.Length];
        for (int i = 0; i < planeColors.Length; i++)
        {
            Button newColorButton = i == 0 ? colorButtonOriginal : Instantiate(colorButtonOriginal, colorButtonsParent);
            Image _image = newColorButton.GetComponent<Image>();
            _image.color = planeColors[i];

            int index = i;

            newColorButton.onClick.RemoveAllListeners();
            newColorButton.onClick.AddListener(() => OnColorSelected(index));

            colorButtons[i] = newColorButton;
        }

        SetSelectedColorButton(PlaneSkinManager.Instance.SelectedColorIndex);
    }

    private void OnVolumeChanged()
    {
        volumeSlider.value = AudioManager.Instance.Volume;

        SetSoundsToggleValue();
    }

    private void SetSoundsToggleValue()
    {
        soundsToggle.IsOn = AudioManager.Instance.Volume > 0;
    }

    private void OnSoundsToggleValueChanged(bool v)
    {
        AudioManager.Instance.SetVolume(v ? 1f : 0f);
    }

    private void OnVolumeSliderValueChanged(float v)
    {
        AudioManager.Instance.SetVolume(v);
    }

    private void OnColorSelected(int index)
    {
        SetSelectedColorButton(index);
        PlaneSkinManager.Instance.SelectColor(index);
    }

    private void SetSelectedColorButton(int index)
    {
        for (int i = 0; i < colorButtons.Length; i++)
        {
            colorButtons[i].interactable = index != i;
        }
    }

    private void OnDestroy()
    {
        AudioManager.Instance.OnVolumeChanged -= OnVolumeChanged;
    }
}
