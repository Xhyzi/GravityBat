
/// <summary>
/// Script asociado al toggle que activa/desactiva la musica.
/// </summary>
public class ToggleMusic : _BaseToggle
{
    /// <summary>
    /// Inicializa el valor del toggle en Awake.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = GameManager.Instance.Data.Music;
    }

    /// <summary>
    /// Cambia el valor en los datos del GM, y notifica el cambio para elevar el evento.
    /// Eleva el evento de cambio en la configuracion de usuario, que guarda los datos.
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        GameManager.Instance.Data.Music = toggle.isOn;
        GameManager.Instance.AudioM.Raise_MusicMutedChanged(!toggle.isOn);
        GameManager.Instance.Raise_UserConfigChanged();
    }
}