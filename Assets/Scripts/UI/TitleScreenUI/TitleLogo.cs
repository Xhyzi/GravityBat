using UnityEngine;

/// <summary>
/// Script asociado al logo de la pantalla de titulo
/// </summary>
public class TitleLogo : MonoBehaviour
{
    /// <summary>
    /// Metodo llamado por el animator del logo cuando termina su animación.
    /// Notifica a TitleScreenManager para elevar el evento.
    /// </summary>
    public void AnimationFinished()
    {
        TitleScreenManager.Instance.Raise_TitleAnimationFinished();
    }
}
