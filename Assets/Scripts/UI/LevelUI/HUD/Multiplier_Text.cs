using UnityEngine;
using TMPro;

/// <summary>
/// Script asociado al multiplicador de puntos del HUD dentro del nivel.
/// Se encarga de actualizar al valor mostrado en el HUD y de reiniciar la animación,
/// asi como de resetear el multiplicador a la finalizacion de la misma.
/// </summary>
public class Multiplier_Text : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Animator anim;
    private TextMeshProUGUI tMesh;
    #endregion

    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();
        tMesh = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Se suscribe a los eventos de LevelManager
    /// </summary>
    private void OnEnable()
    {
        LM.OnMultiplierUpdate += HandleMultiplierUpdate;
    }

    /// <summary>
    /// Desuscribe los eventos de LM
    /// </summary>
    private void OnDisable()
    {
        LM.OnMultiplierUpdate -= HandleMultiplierUpdate;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al evento de actualizacion del multiplicador.
    /// </summary>
    private void HandleMultiplierUpdate()
    {
        SetMultiplierText();
        anim.SetTrigger("StartAnimation");
    }
    #endregion

    #region Methods
    /// <summary>
    /// Metodo llamado cuando finaliza la animacion del multiplicador.
    /// Devuelve el valor del multiplicador a su valor inicial.
    /// </summary>
    public void MultiplierAnimEnd()
    {
        LM.Raise_MultiplierTimeFinished();
    }

    /// <summary>
    /// Establece el valor del texto del multiplicador.
    /// </summary>
    private void SetMultiplierText()
    {
        tMesh.text = "x" + LM.Data.Multiplier.ToString();
    }
    #endregion

}
