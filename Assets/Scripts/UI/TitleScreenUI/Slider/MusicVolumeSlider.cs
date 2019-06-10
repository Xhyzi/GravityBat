using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script asociado al slider del volumen de la musica.
/// </summary>
public class MusicVolumeSlider : _BaseSlider
{
    /// <summary>
    /// Inicializa el valor del slider desde los datos del GM.
    /// </summary>
    protected override void InitSliderValue()
    {
        slider.value = GameManager.Instance.Data.MusicVolume;
    }

    /// <summary>
    /// Guarda el valor del slider en los datos del GM y nofitica el cambio para elevar el evento.
    /// </summary>
    protected override void ValueChangedCheck()
    {
        GameManager.Instance.Data.MusicVolume = slider.value;
        GameManager.Instance.AudioM.Raise_MusicVolumeChanged(slider.value);
        GameManager.Instance.Raise_UserConfigChanged();
    }
}