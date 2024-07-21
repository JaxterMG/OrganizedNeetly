using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.SoundSystem.SoundManager
{
    [CreateAssetMenu(fileName = "SoundLibrary",
                    menuName = "Sounds")]
    public class SoundLibrary : SerializedScriptableObject
    {
        public Dictionary<string, AudioClip> _soundLibrary = new Dictionary<string, AudioClip>();
    }
}
