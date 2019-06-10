using UnityEngine;
using TMPro;
using GravityBat_Constants;

/// <summary>
/// Vinculado a los objetos de texto que muestran la puntuacion obtenida en un intento de un nivel.
/// </summary>
public class AttemptStatText : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private TextMeshProUGUI tMesh;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Estadistica a la que se encuentra vinculada el texto
    /// </summary>
    [SerializeField]
    AttemptStats statTag;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias a LM y TextMesh.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        tMesh = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Suscribe los eventos de LM.
    /// </summary>
    private void OnEnable()
    {
        LM.OnLevelCompleted += HandleLevelCompleted;
        LM.OnInfiniteGameOver += HandleLevelCompleted;
    }

    /// <summary>
    /// Desuscribe los eventos de LM.
    /// </summary>
    private void OnDisable()
    {
        LM.OnLevelCompleted -= HandleLevelCompleted;
        LM.OnInfiniteGameOver -= HandleLevelCompleted;
    }

    #region Event Handlers
    /// <summary>
    /// Asigna valor al texto al completar el nivel
    /// </summary>
    private void HandleLevelCompleted()
    {
        tMesh.text = LM.GetAttemptStatString(statTag);
    }
    #endregion
}
