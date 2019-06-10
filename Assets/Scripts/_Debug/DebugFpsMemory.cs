using UnityEngine;
using UnityEngine.UI;
using System.Text;
using GravityBat_Constants;

namespace GravityBat_Debug
{

    /// <summary>
    /// Script asociado al GameObject que muestra los fotogramas por segundo y
    /// la memoria en uso.
    /// </summary>
    public class DebugFpsMemory : MonoBehaviour
    {
        #region Attributes
        private float deltaTime;
        private Text infoText;
        #endregion

        /// <summary>
        /// Ejecutado el instanciar el MonoBehaviour.
        /// Recoge la referencia del texto y establece deltaTime en 0
        /// </summary>
        private void Awake()
        {
            infoText = GetComponentInChildren<Text>();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            deltaTime = 0;
        }

        private void OnEnable()
        {
            GameManager.Instance.OnDebugUIEnabled += HandleDebugUIEnabled;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnDebugUIEnabled -= HandleDebugUIEnabled;
        }

        /// <summary>
        /// Ejecutado una vez por frame Update.
        /// Actualiza el contenido del texto.
        /// </summary>
        private void Update()
        {
            infoText.text = BuildStatusString();
        }

        #region Event Handlers
        private void HandleDebugUIEnabled(bool enabled)
        {
            if (!enabled)
                Destroy(transform.gameObject);
        }
        #endregion

        #region Method
        /// <summary>
        /// Obtiene la cantidad de fps.
        /// </summary>
        /// <returns>string con los fps</returns>
        private string GetCurrentFps()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fps = fps < Constants.MAX_FPS ? fps : Constants.MAX_FPS;

            return Mathf.Floor(fps).ToString();
        }

        /// <summary>
        /// Obtiene la memoria en uso en MB
        /// </summary>
        /// <returns>string con la memoria en uso</returns>
        private string GetMemoryUsage()
        {
            return (System.GC.GetTotalMemory(false) >> 16).ToString() + "MB";
        }

        /// <summary>
        /// Construye el string que se mostrara en el texto, con los fps y la memoria en uso.
        /// </summary>
        /// <returns>string con el texto a mostrar</returns>
        private string BuildStatusString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  Fps: {0}\tMemory Used: {1}", GetCurrentFps(), GetMemoryUsage());
            return sb.ToString();
        }
        #endregion
    }
}
