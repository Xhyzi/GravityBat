using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Trigger collider utilizado para marcar el inicio del nivel.
/// Hereda de _BaseLeveltrigger
/// </summary>
public class LevelStartTrigger : _BaseLevelTrigger
{
    /// <summary>
    /// Ejecutado al atravesar el collider tipo trigger.
    /// Notifica al LevelManager para que eleve el evento que marca el inicio del nivel.
    /// </summary>
    protected override void TriggerAction()
    {
        LevelManager.Instance.Raise_StartLevelTrigger();
    }
}