using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBehaviour : MonoBehaviour
{
    Renderer _renderer_material;
    public ColorContainer _container;
    [SerializeField]
    [Range(0, 5)] int indexNumber;

    private void Start()
    {
        _renderer_material = GetComponent<Renderer>();
        _renderer_material.material.color = _container._color[indexNumber];
    }
}
