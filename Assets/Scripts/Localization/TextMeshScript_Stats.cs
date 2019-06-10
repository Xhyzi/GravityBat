using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Localization
{
    /// <summary>
    /// Script utilizado para cargar el texto de las estadisticas y datos.
    /// Hereda de TextMeshScript
    /// </summary>
    public class TextMeshScript_Stats : TextMeshScript
    {
        /// <summary>
        /// Tag identificador del dataText a cargar
        /// </summary>
        [SerializeField]
        DATA_TXT_TAG dataTag;

        /// <summary>
        /// Formato del texto a mostrar
        /// </summary>
        /// <returns>devuelve el string a mostrar</returns>
        protected override string GetDisplayText()
        {
            return preffix + Strings.GetDataString(dataTag) + suffix;
        }

        /// <summary>
        /// Se suscribe al evento de GameManager
        /// </summary>
        protected override void SubscribeEvents()
        {
            GameManager.Instance.OnDataTextLoadedToMemory += HandleOnTextLoadedToMemory;
        }

        /// <summary>
        /// Se desuscribe del evento de GameManager
        /// </summary>
        protected override void UnsubscribeEvents()
        {
            GameManager.Instance.OnDataTextLoadedToMemory -= HandleOnTextLoadedToMemory;
        }

    }
}