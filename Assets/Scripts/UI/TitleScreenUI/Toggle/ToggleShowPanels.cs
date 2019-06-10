
/// <summary>
/// Script encargado de comunicar al GameManager los cambios ocurridos en el toggle para
/// mostrar los paneles de nivel de la interfaz
/// </summary>
public class ToggleShowPanels : _BaseToggle
{
    /// <summary>
    /// Inicializa el valor del Toggle desde los datos del GM.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = GameManager.Instance.Data.ShowLevelPanels;
    }

    /// <summary>
    /// Guarda el estado del toggle en los datos del GM y lo notifica para elevar el evento -> guardar los datos.
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        GameManager.Instance.Data.ShowLevelPanels = toggle.isOn;
        GameManager.Instance.Raise_UserConfigChanged();
    }
}
