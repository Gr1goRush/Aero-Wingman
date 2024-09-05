using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopBorder : MonoBehaviour
{
    [SerializeField] private Hoop owner;
    [SerializeField] private MeshRenderer _renderer;

    private void OnTriggerEnter(Collider other)
    {
        owner.OnBorderEnter(other);
    }

    public void SetColor(Color color)
    {
        Material mat = new Material(_renderer.material);
        mat.color = color;
        _renderer.material = mat;
    }
}
