using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace GravityBat_Debug
{

    /// <summary>
    /// Script asociado a la consola del modo debug.
    /// Muestra el log de la consola de Unity en un Panel que puede ser
    /// arrastrado de forma tactil.
    /// </summary>
    public class DebugIngameConsole : MonoBehaviour
    {
        #region Attributes
        private TextMeshProUGUI tMesh;
        private string myLog;
        private List<string> logStack;
        private Vector3 screenPoint;
        private Vector3 offset;
        #endregion

        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour.
        /// Recoge la referencia de TextMesh y crea la lista de logs.
        /// </summary>
        private void Awake()
        {
            tMesh = GetComponentInChildren<TextMeshProUGUI>();
            logStack = new List<string>();
        }

        /// <summary>
        /// Se suscribe al evento lanzado por la consola al entrar un log.
        /// </summary>
        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
            GameManager.Instance.OnDebugUIEnabled += HandleDebugUIEnabled;
        }

        /// <summary>
        /// Se desuscribe al evento lanzado por la consola al entrar un log.
        /// </summary>
        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
            GameManager.Instance.OnDebugUIEnabled -= HandleDebugUIEnabled;
        }

        #region Event Handler
        private void HandleDebugUIEnabled(bool enabled)
        {
            if (!enabled)
                Destroy(transform.gameObject);
        }
        #endregion

        /// <summary>
        /// Lleva a cabo las acciones correspondientes a la entrada de un log en la consola.
        /// Aniade el nuevo log a la lista y refresca el texto mostrado en el Panel volcando
        /// el contenido de la lista siguiendo LIFO.
        /// Elimina todos los elementos de la lista pasado cierto punto, para evitar tener un log
        /// demasiado grande.
        /// </summary>
        /// <param name="logString">Cadena de texto con el log que entra a la consola</param>
        /// <param name="stackTrace">Traza del log que entra en la consola, es impresa en el panel cuando se produce una excepcion</param>
        /// <param name="type">Tipo de log que entra en la consola. Permite diferenciar entre Log/Warning/Exception...</param>
        private void HandleLog(string logString, string stackTrace, LogType type)
        {

            string formatLog = "\n [" + type + "] : " + logString;

            logStack.Add(formatLog);

            if (type == LogType.Exception)
                logStack.Add("\n" + stackTrace);

            myLog = string.Empty;

            foreach (string log in logStack)
            {
                string temp = log;
                temp += myLog;

                myLog = temp;
            }

            while (logStack.Count > 20)
                logStack.RemoveAt(0);
        }

        /// <summary>
        /// Se ejecuta al refrescar la UI.
        /// actualiza el valor del log en la consola mostrada por pantalla.
        /// </summary>
        private void OnGUI()
        {
            tMesh.text = myLog;
        }


        /// <summary>
        /// Se ejecuta cuando se pulsa dentro del collider que contiene el panel.
        /// </summary>
        private void OnMouseDown()
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        /// <summary>
        /// Se ejecuta cuando se arrastra en la pantalla, permite desplazar el panel.
        /// </summary>
        private void OnMouseDrag()
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

        }
    }
}
