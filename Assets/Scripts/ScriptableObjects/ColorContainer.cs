using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObject/Color Data", order = 0)]
public class ColorContainer : ScriptableObject
{
    [SerializeField] public List<Color> _color;
}
