using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObject/Data/CubeData", order = 0)]
public class CubeData : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<CUBE, Material> colors;
    public Dictionary<CUBE, Material> Colors => colors;

    public Material GetColor(CUBE type)
    {
        if (colors.ContainsKey(type))
            return colors[type];
        return null;
    }
}
