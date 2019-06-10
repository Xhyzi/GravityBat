using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Trigger collider utilizado para marcar el final del nivel.
/// Hereda de _BaseLevelTrigger
/// </summary>
public class LevelEndTrigger : _BaseLevelTrigger
{
    /// <summary>
    /// Ejecutado al atravesar el collider tipo trigger.
    /// Notifica al LevelManager para que eleve el evento que marca el final del nivel.
    /// </summary>
    protected override void TriggerAction()
    {
        LevelManager.Instance.Raise_LevelCompletedTrigger();
    }
}