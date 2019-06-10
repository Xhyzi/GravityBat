using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase base utilizada para ser heredada por un slider.
/// </summary>
public class _BaseSlider : MonoBehaviour
{
    protected Slider slider;

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge una referencia del slider e inicializa su valor
    /// </summary>
    private void Awake()
    {
        slider = GetComponent<Slider>();
        InitSliderValue();
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(delegate { ValueChangedCheck(); });
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(delegate { ValueChangedCheck(); });
    }

    #region Virtual Methods
    /// <summary>
    /// Metodo utilizado para herencia.
    /// Se ejecuta en el Awake, utilizado para dar el valor inicial al slider.
    /// </summary>
    protected virtual void InitSliderValue() { slider.value = 0; }

    /// <summary>
    /// Metodo utilizado para herencia.
    /// controla la accion a ejecutar ante el cambio de valor del slider
    /// </summary>
    protected virtual void ValueChangedCheck() { }
    #endregion

}
