
/// <summary>
/// Script asociado al toggle que activa/desactiva los efectos de sonido
/// </summary>
public class ToggleSFX : _BaseToggle
{
    /// <summary>
    /// Inicializa el valor del toggle desde los datos del GM.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = GameManager.Instance.Data.Sfx;
    }

    /// <summary>
    /// Guarda el estado del toggle en los datos de GM y lo notifica a GM para elevar los eventos. -> guarda la configuracion
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        GameManager.Instance.Data.Sfx = toggle.isOn;
        GameManager.Instance.AudioM.Raise_SfxMutedChanged(!toggle.isOn);
        GameManager.Instance.Raise_UserConfigChanged();
    }
}