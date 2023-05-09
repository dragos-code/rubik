using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class FaceBehaviour : MonoBehaviour
{
    [SerializeField] Color color = Color.white;
    Renderer _renderer_material;

    private void Start()
    {
        _renderer_material = GetComponent<Renderer>();
        _renderer_material.material.color = color;


    }

    private void Update()
    {
       // _renderer_material.material.color = color;

    }
}
