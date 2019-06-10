using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script asociado al slider que controla el volumen de los efectos de sonido.
/// </summary>
public class SFXVolumeSlider : _BaseSlider
{
    /// <summary>
    /// Inicializa el valor del slider desde los datos del GM.
    /// </summary>
    protected override void InitSliderValue()
    {
        slider.value = GameManager.Instance.Data.SfxVolume;
    }

    /// <summary>
    /// Guarda el valor del slider en los datos del GM y lo notifica para elevar el evento.
    /// </summary>
    protected override void ValueChangedCheck()
    {
        GameManager.Instance.Data.SfxVolume = slider.value;
        GameManager.Instance.AudioM.Raise_SfxVolumeChanged(slider.value);
        GameManager.Instance.Raise_UserConfigChanged(); //TODO: Se provoca una sobrecarga al arrastrar el slider y guardar multiples partidas
    }
}