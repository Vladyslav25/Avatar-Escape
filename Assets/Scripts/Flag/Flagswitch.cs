using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagswitch : MonoBehaviour
{
    [SerializeField]
    private List<Material> materials = new List<Material>();
    [SerializeField]
    private SkinnedMeshRenderer _meshRenderer;
    public int HP
    {
        set
        {
            _meshRenderer.material = materials[value];
        }
    }
    private void Start()
    {
        HP = 6;
    }
}
