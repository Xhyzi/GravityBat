using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado al trigger que provoca un cambio de escenario en el nivel
/// </summary>
public class LevelEnvironmentTrigger : _BaseLevelTrigger
{
    [SerializeField]
    Biome biome = Biome.CAVE;

    /// <summary>
    /// Notifica al LevelManager de que debe cargar el entorno parallax indicado
    /// </summary>
    protected override void TriggerAction()
    {
        LevelManager.Instance.Raise_ParallaxChangeTrigger(biome);
    }
}
