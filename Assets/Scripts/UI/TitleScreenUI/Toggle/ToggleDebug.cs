using GravityBat_Debug;

/// <summary>
/// Toggle empleado para activar/desactivar el modo debug
/// </summary>
public class ToggleDebug : _BaseToggle
{

    /// <summary>
    /// Inicializa el valor del toggle desde los datos del GM.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = _Debug.enabledDebugUI;

        if (!_Debug.DEBUG_MODE)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    /// <summary>
    /// Guarda el estado del toggle en los datos de GM y lo notifica a GM para elevar los eventos. -> guarda la configuracion
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        _Debug.enabledDebugUI = toggle.isOn;
        GameManager.Instance.Raise_DebugUIEnabled(toggle.isOn);
    }
}
