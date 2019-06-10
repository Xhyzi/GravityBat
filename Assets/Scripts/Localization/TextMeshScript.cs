using GravityBat_Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace GravityBat_Localization
{
    /// <summary>
    /// Script vinculado a un GameObject con un componente tipo TextMeshProUGUI.
    /// Se encarga de cargar el texto dependiendo del idioma y teniendo en cuenta la etiqueta
    /// de texto seleccionada en el editor, asi como sufijo y prefijo.
    /// Puede ser heredada por otras clases para modificar el comportamiento de GetDisplayText
    /// </summary>
    public class TextMeshScript : MonoBehaviour
    {
        #region Attributes
        protected TextMeshProUGUI tMesh;

        /// <summary>
        /// Recoge el index del scene en el que se inicializa el texto.
        /// Evita que los textos se recarguen en el lapso de tiempo que transcurre
        /// desde que se carga una nueva escena hasta que se destruyen los GO de la anterior uwu.
        /// </summary>
        protected int sceneIndex;
        #endregion

        #region Serialized Fields
        /// <summary>
        /// Tag identificador del texto a mostrar en el TextMesh
        /// </summary>
        [SerializeField]
        protected TEXT_TAG textTag;

        /// <summary>
        /// Sufijo del texto.
        /// </summary>
        [SerializeField]
        protected string suffix;

        /// <summary>
        /// Prefijo del texto.
        /// </summary>
        [SerializeField]
        protected string preffix;
        #endregion
        
        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour
        /// Recoge referencias
        /// </summary>
        protected void Awake()
        {
            tMesh = GetComponent<TextMeshProUGUI>();
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        /// <summary>
        /// Suscribe los eventos
        /// </summary>
        protected void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Desuscribe los eventos
        /// </summary>
        protected void OnDisable()
        {
            UnsubscribeEvents();
        }

        #region EventHandler
        /// <summary>
        /// Recarga el texto en el componente cuando se han recargado en memoria (generalmente cambio de idioma)
        /// </summary>
        protected void HandleOnTextLoadedToMemory()
        {
            if (sceneIndex == SceneManager.GetActiveScene().buildIndex)
                LoadText();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Carga los textos
        /// </summary>
        protected void LoadText()
        {
            tMesh.text = GetDisplayText();
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Metodo utilizado para modificar la funcionalidad mediante herencia
        /// </summary>
        /// <returns>Devuelve el texto que se mostrara en el TextMesh</returns>
        protected virtual string GetDisplayText()
        {
            return preffix + Strings.GetString(textTag) + suffix;
        }

        /// <summary>
        /// Metodo utilizado para suscribir eventos mediante herencia
        /// </summary>
        protected virtual void SubscribeEvents()
        {
            GameManager.Instance.OnTextLoadedToMemory += HandleOnTextLoadedToMemory;
        }

        /// <summary>
        /// Metodo utilizado para desuscribir eventos mediante herencia
        /// </summary>
        protected virtual void UnsubscribeEvents()
        {
            GameManager.Instance.OnTextLoadedToMemory -= HandleOnTextLoadedToMemory;
        }
        #endregion
    }
}