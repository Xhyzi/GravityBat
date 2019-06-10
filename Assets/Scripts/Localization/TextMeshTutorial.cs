using UnityEngine;
using GravityBat_Localization;
using GravityBat_Constants;

public class TextMeshTutorial : TextMeshScript
{
    /// <summary>
    /// Tag que identifica el tutorial del que se debe mostrar el texto.
    /// </summary>
    [SerializeField]
    TUTORIAL_TXT_TAG tutorial;

    private void Start()
    {
        tMesh.text = GetDisplayText();
    }

    /// <summary>
    /// Formato del texto a mostrar
    /// </summary>
    /// <returns>devuelve el string a mostrar</returns>
    protected override string GetDisplayText()
    {
        return preffix + Strings.GetTutorialString(tutorial) + suffix;
    }

    /// <summary>
    /// Se suscribe al evento de GameManager
    /// </summary>
    protected override void SubscribeEvents()
    {
        LevelManager.Instance.OnTutorialTextLoad += HandleOnTextLoadedToMemory;
    }

    /// <summary>
    /// Se desuscribe del evento de GameManager
    /// </summary>
    protected override void UnsubscribeEvents()
    {
        LevelManager.Instance.OnTutorialTextLoad -= HandleOnTextLoadedToMemory;
    }
}
