using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase utilizada como base para ser heredada por los toggle del menu de configuracion.
/// </summary>
public class _BaseToggle : MonoBehaviour
{
    protected Toggle toggle;

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge la referencia del Toggle y establece su valor leyendo los datos del juego.
    /// </summary>
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        InitToggleValue();
    }

    /// <summary>
    /// Activa el listener
    /// </summary>
    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(delegate { OnValueChangedChecked(); });
        OnEnableAditionalActions();
    }

    /// <summary>
    /// Desactiva el listener
    /// </summary>
    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(delegate { OnValueChangedChecked(); });
        OnDisableAditionalActions();
    }

    #region Virtual Methods
    /// <summary>
    /// Metodo utilizado para herencia.
    /// Inicializa el valor del toggle en Awake.
    /// Valor inicial por defecto -> false
    /// </summary>
    protected virtual void InitToggleValue() { toggle.isOn = false; }

    /// <summary>
    /// Metodo utilizado para herencia.
    /// Actualiza el valor del dato representado por el Toggle en los datos del GameManager.
    /// </summary>
    protected virtual void OnValueChangedChecked() { }

    /// <summary>
    /// Metodo utilizado para herencia.
    /// Ejecuta acciones adicionales en OnEnable por parte de la clase hija.
    /// </summary>
    protected virtual void OnEnableAditionalActions() { }

    /// <summary>
    /// Metodo utilizado para herencia.
    /// Ejecuta acciones adicionales en OnDisable por parte de la clase hija.
    /// </summary>
    protected virtual void OnDisableAditionalActions() { }
    #endregion
}
