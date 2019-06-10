using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Script asocioado al collider tipo trigger de cada LevelFragment.
/// Es utilizado para saber en que momento ha de cargarse un nuevo fragmento
/// del nivel.
/// Hereda de _BaseLevelTrigger
/// </summary>
public class LevelFragmentTriggerCol : _BaseLevelTrigger
{
    /// <summary>
    /// Ejecutado al atravesar el collider tipo trigger.
    /// Notifica al LevelManager para que eleve el evento que marca 
    /// que han de cargarse y eliminarse fragmentos de nivel.
    /// </summary>
    protected override void TriggerAction()
    {
        LevelManager.Instance.Raise_LevelFragmentTrigger();
    }
}