using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Shop
{
    [CreateAssetMenu(fileName = "FieldColor", menuName = "ScriptableObjects/FieldColor", order = 1)]
    public class FieldColor : SerializedScriptableObject
    {
        public Color Color;
    }
}