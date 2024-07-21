using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Core.Shop
{
    [CreateAssetMenu(fileName = "FiguresColors", menuName = "ScriptableObjects/FiguresColors", order = 1)]
    public class FiguresColors : SerializedScriptableObject
    {
        public Dictionary<string, Color> Figures = new Dictionary<string, Color>();
    }
}