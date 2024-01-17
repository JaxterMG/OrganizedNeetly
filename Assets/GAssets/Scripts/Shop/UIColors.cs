using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "UIColors", menuName = "ScriptableObjects/UIColors", order = 1)]
public class UIColors : SerializedScriptableObject
{
    public Dictionary<string, Color> UI = new Dictionary<string, Color>();
}