using System.Text;
using UnityEngine;
using TMPro;

/// <summary>
/// Script asociado al texto que muestra la puntuacion del nivel en el HUD.
/// </summary>
public class ScoreText : MonoBehaviour
{
    private LevelManager LM;

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencia de LM.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
    }

    /// <summary>
    /// Suscribe los eventos de LM.
    /// </summary>
    private void OnEnable()
    {
        LM.OnScoreChanged += HandleOnScoreChanged;
    }

    /// <summary>
    /// Desuscribe los eventos de LM.
    /// </summary>
    private void OnDisable()
    {
        LM.OnScoreChanged -= HandleOnScoreChanged;
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes al cambio de puntuacion en el LevelManager.
    /// </summary>
    private void HandleOnScoreChanged()
    {
        UpdateScoreDisplay();
    }

    /// <summary>
    /// Actualiza el valor de la puntuacion en el componente grafico.
    /// Se muestran los '0' por defecto de la puntuacion tipo super mario.
    /// </summary>
    private void UpdateScoreDisplay()
    {
        StringBuilder builder = new StringBuilder("000000000", 8);
        builder.Insert(9 - LM.Data.Score.ToString().Length, LM.Data.Score.ToString());
        GetComponent<TextMeshProUGUI>().text = builder.ToString().Substring(0, 9);
    }
}
