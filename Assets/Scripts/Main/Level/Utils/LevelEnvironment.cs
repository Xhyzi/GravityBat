using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado al entorno del nivel (fondo que se muestra en el parallax)
/// </summary>
public class LevelEnvironment : MonoBehaviour
{
    private LevelManager LM;

    private void Awake()
    {
        LM = LevelManager.Instance;
    }

    private void OnEnable()
    {
        LM.OnParallaxChangeTrigger += HandleChangeParallaxTrigger;
    }

    private void OnDisable()
    {
        LM.OnParallaxChangeTrigger -= HandleChangeParallaxTrigger;
    }

    #region Event Handlers
    private void HandleChangeParallaxTrigger(Biome biome)
    {
        switch (biome)
        {
            case Biome.CAVE:
                //TODO: Convertir los strings en constantes
                Instantiate(Resources.Load("prefabs/parallax/level/parallax_cave") as GameObject);
                break;

            case Biome.OUTSIDE:
                //TODO: Convertir los strings en constantes
                Instantiate(Resources.Load("prefabs/parallax/level/parallax_outside") as GameObject);
                break;
        }

        Destroy(gameObject);
    }
    #endregion
}
