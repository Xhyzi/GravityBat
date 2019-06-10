using GravityBat_Constants;

namespace GravityBat_Items
{
    /// <summary>
    /// Script utilizado para el Item 'GemGravity'. Hereda de BaseItem.
    /// </summary>
    public class GemGravity : _BaseItem
    {
        /// <summary>
        /// Ejecutado cuando se obtiene el item.
        /// Aumenta el GravityPower y lo notifica a AudioManager para reproducir el sonido.
        /// </summary>
        protected override void OnItemObtainedAction()
        {
            LM.Raise_GravityGemObtained(SCORE);
            base.OnItemObtainedAction();
        }
    }
}
