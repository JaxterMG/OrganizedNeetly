using UnityEngine;

namespace Core.UI.ScreenController
{
    public class ScreenAnchorsController : MonoBehaviour
    {
        public RectTransform LeftAnchor;
        public RectTransform RightAnchor;
        public RectTransform UpAnchor;
        public RectTransform DownAnchor;
        public static ScreenAnchorsController Instance;

        void Awake()
        {
            Instance = this;
        }
    }
}
