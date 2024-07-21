using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.Shop
{
    [CreateAssetMenu(fileName = "BackGroundColor", menuName = "ScriptableObjects/BackGroundColor", order = 1)]
    public class BackGroundColor : SerializedScriptableObject
    {
        public Color Color;
    }
}