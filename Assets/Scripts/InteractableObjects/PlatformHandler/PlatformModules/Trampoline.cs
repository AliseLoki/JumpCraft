using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private List<Renderer> _allRenderers;

    [SerializeField] private Material _red;
    [SerializeField] private Material _green;

    [SerializeField] private Platform _platform;

    public Platform Platform =>_platform;
   // из грин должно быть здесь

    public void SetMaterial(bool isGreen)
    {
        if (isGreen) SetForAllRenderers(_green);
        else SetForAllRenderers(_red);
    }

    private void SetForAllRenderers(Material mat)
    {
        foreach (Renderer renderer in _allRenderers)
        {
            renderer.material = mat;
        }
    }
}
