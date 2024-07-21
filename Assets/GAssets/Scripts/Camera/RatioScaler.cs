using UnityEngine;

namespace Core.Camera
{
    public class RatioScaler : MonoBehaviour
    {
        private const float MOBILEFOV = 4.7f;
        private const float TABLETFOV = 3.8f;
        void OnEnable()
        {
            UnityEngine.Camera camera = GetComponent<UnityEngine.Camera>();
            camera.orthographicSize = IsTablet()? TABLETFOV : MOBILEFOV;
        }
        bool IsTablet()
        {
            // Получаем размеры экрана
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float size = Mathf.Sqrt(screenWidth * screenWidth + screenHeight * screenHeight);

            // Предположим, что устройства с диагональю больше 6.5 дюймов - это планшеты
            return size >= 6.5f;
        }
    }
}
