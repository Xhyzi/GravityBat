using UnityEngine;
using System.Collections;

namespace GravityBat_Localization
{
    /// <summary>
    /// Hereda de TextMeshScript. Inicializa el texto con el componente en lugar de esperar al evento de carga del texto en memoria.
    /// </summary>
    public class TextMeshScript_Awake : TextMeshScript
    {
        private void Start()
        {
            LoadText();
        }
    }
}
