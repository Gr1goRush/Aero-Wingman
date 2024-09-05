using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSkinManager : Singleton<PlaneSkinManager>
{
    public int SelectedColorIndex { get; private set; }

    public Color[] Colors => colors;

    [SerializeField] private Color[] colors;

    public event Action<Color> SelectedColorChanged;

    protected override void Awake()
    {
        base.Awake();

        SelectedColorIndex = PlayerPrefs.GetInt("PlaneColorIndex", 0);
    }

    public void SelectColor(int index)
    {
        SelectedColorIndex = index;
        PlayerPrefs.SetInt("PlaneColorIndex", SelectedColorIndex);

        SelectedColorChanged?.Invoke(GetSelectedColor());
    }

    public Color GetSelectedColor()
    {
        return colors[SelectedColorIndex];
    }
}
