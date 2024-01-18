using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "FieldColor", menuName = "ScriptableObjects/FieldColor", order = 1)]
public class FieldColor : SerializedScriptableObject
{
    public Color Color;
}