using UnityEngine;

namespace GravityBat_Items
{
    /// <summary>
    /// Script utilizado para el Item 'SecretDiamond'. Hereda de BaseItem
    /// </summary>
    public class DiamondCoin : _BaseItem
    {
        /// <summary>
        /// Id del diamante.
        /// Va de 0-2 (3 por nivel)
        /// </summary>
        [SerializeField]
        int coinID;

        /// <summary>
        /// Destruye el Diamante en caso de que el nivel sea infinito. (no tienen diamantes).
        /// </summary>
        protected override void OnAwakeAction()
        {
            if (LM.IsInfinite)
                Destroy(transform.gameObject);
        }

        /// <summary>
        /// Ejecutado al recoger el diamante. 
        /// Lo notifica a LevelManager para actualizar los datos del nivel.
        /// Lo notifica a AdioManager para reproducir el sonido correspondiente.
        /// </summary>
        protected override void OnItemObtainedAction()
        {
            LM.Raise_DiamondObtained(SCORE, coinID);
            GameManager.Instance.AudioM.Raise_ItemPickupBig();
        }
    }
}
