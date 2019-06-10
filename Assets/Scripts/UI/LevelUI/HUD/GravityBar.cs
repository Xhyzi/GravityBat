using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Script asociado al componente grafico del HUD utilizado para mostrar la cantidad
/// de gravityPower disponible.
/// </summary>
public class GravityBar : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private RectTransform transformGreenLayer;
    private RectTransform TransformPurpleLayer;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Capa azul turquesa de la barra de energia
    /// </summary>
    [SerializeField]
    GameObject GreenLayer;    

    /// <summary>
    /// Capa morada de la barra de energia
    /// </summary>
    [SerializeField]
    GameObject PurpleLayer;
    #endregion

    /// <summary>
    /// Recoge la referencia a la instancia del Manager.
    /// Obtiene los RectTransform de cada uno de los layers de la barra del HUD.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        transformGreenLayer = GreenLayer.GetComponent<RectTransform>();   
        TransformPurpleLayer = PurpleLayer.GetComponent<RectTransform>();   
    }

    /// <summary>
    /// Se suscribe a los eventos de LM.
    /// </summary>
    private void OnEnable()
    {
        LM.OnTutorialTriggered += HandleSwapConsecuencesTutorialTriggered;
        LM.OnTutorialEnded += HandleSwapConsecuencesTutorialEnded;
    }

    /// <summary>
    /// Se desuscribe a los eventos de LM.
    /// </summary>
    private void OnDisable()
    {
        LM.OnTutorialTriggered -= HandleSwapConsecuencesTutorialTriggered;
        LM.OnTutorialEnded -= HandleSwapConsecuencesTutorialEnded;
    }

    /// <summary>
    /// Actualiza el componente del HUD para mostrar el gravityPower disponible.
    /// </summary>
    private void LateUpdate()
    {
        UpdateLayersScale();
    }

    #region Event Handlers
    /// <summary>
    /// Aumenta el tamanio de la barra en el HUD cuando se ejecuta el tutorial que muestra
    /// las consecuencias del cambio de gravedad.
    /// </summary>
    /// <param name="tutorial">tutorial que se ha cargado</param>
    private void HandleSwapConsecuencesTutorialTriggered(TutorialTags tutorial)
    {
        if (tutorial == TutorialTags.SWAP_CONSECUENCES)
            GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);      
    }

    /// <summary>
    /// Devuelve el tamanio de la barra en el HUD a sus dimensiones originales.
    /// </summary>
    /// <param name="tutorial">tutorial que se ha finalizado</param>
    private void HandleSwapConsecuencesTutorialEnded(TutorialTags tutorial)
    {
        if (tutorial == TutorialTags.SWAP_CONSECUENCES)
            GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Actualiza la escala de las capas roja y morada segun la energia disponible para mostrarla correctamente en el HUD.
    /// </summary>
    private void UpdateLayersScale()
    {
        int chunks = LM.Data.GravityPower / Constants.GRAVITY_CHUNK;
        int fragments = LM.Data.GravityPower % Constants.GRAVITY_CHUNK;

        Vector2 purpleAnchors = new Vector2(GetPurpleLayerXAnchor(chunks), 1f);
        Vector2 redAnchors = new Vector2(GetGreenLayerXAnchor(fragments, chunks), 1f);

        transformGreenLayer.anchorMax = redAnchors;
        TransformPurpleLayer.anchorMax = purpleAnchors;

    }

    /// <summary>
    /// Devuelve el anclaX de la capa morada en funcion de la energia disponible
    /// </summary>
    /// <param name="chunks">cantidad de grupos de energia disponible</param>
    /// <returns>Anchor MAX_X</returns>
    private float GetPurpleLayerXAnchor(int chunks)
    {
        return (Constants.GRAVITY_BAR_NULL_WIDTH + (Constants.GRAVITY_BAR_CHUNK + Constants.GRAVITY_BAR_CHUNK_SPACING) * chunks);
    }

    /// <summary>
    /// Devuelve el anclaX de la capa morada en funcion de la energia disponible
    /// </summary>
    /// <param name="fragments">cantidad de fragmentos de energia disponibles</param>
    /// <param name="chunks">cantidad de grupos de energia disponibles</param>
    /// <returns></returns>
    private float GetGreenLayerXAnchor(int fragments, int chunks)
    {
        return (GetPurpleLayerXAnchor(chunks) + fragments * Constants.GRAVITY_BAR_UNIT_WIDTH);
    }
    #endregion
}
