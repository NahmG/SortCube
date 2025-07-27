using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObject/Data/CubeData", order = 0)]
public class CubeData : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<CUBE, Color> colors;
    public Dictionary<CUBE, Color> Colors => colors;

    public Color GetColor(CUBE type)
    {
        if (colors.ContainsKey(type))
            return colors[type];
        return Color.clear;
    }
}