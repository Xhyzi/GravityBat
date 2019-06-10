
/// <summary>
/// Script asociado al toggle del reintento automatico
/// </summary>
public class ToggleRetry : _BaseToggle
{
    /// <summary>
    /// Inicializa el valor del toggle desde los datos del GM.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = GameManager.Instance.Data.AutoRetry;
    }

    /// <summary>
    /// Guarda el estado del toggle en los datos del GM y eleva el evento para guardar los datos.
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        GameManager.Instance.Data.AutoRetry = toggle.isOn;
        GameManager.Instance.Raise_UserConfigChanged();
    }
}