using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSkinOwner : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] renderers;

    void Start()
    {
        Color color = PlaneSkinManager.Instance.GetSelectedColor();
        SetMaterials(color);

        PlaneSkinManager.Instance.SelectedColorChanged += SetMaterials;
    }

    private void SetMaterials(Color color)
    {
        foreach (var _renderer in renderers)
        {
            Material mat = new Material(_renderer.material);
            mat.color = color;
            _renderer.material = mat;
        }
    }

    private void Reset()
    {
       MeshRenderer[] allRenderers = GetComponentsInChildren<MeshRenderer>();
        List<MeshRenderer> meshRenderersList = new List<MeshRenderer>();

        foreach (var _renderer in allRenderers)
        {
            if(_renderer.sharedMaterial.name.Contains("Plane"))
            {
                meshRenderersList.Add(_renderer);
            }
        }

        renderers = meshRenderersList.ToArray();
    }

    private void OnDestroy()
    {
        PlaneSkinManager.Instance.SelectedColorChanged -= SetMaterials;
    }

}
