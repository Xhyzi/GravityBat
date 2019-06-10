using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Items
{
    /// <summary>
    /// Script utilizado para el Item 'AbilityFruit'. Hereda de BaseItem.
    /// </summary>
    public class AbilityFruit : _BaseItem
    {
        #region Serialized Fields
        /// <summary>
        /// Habilidad aportada por el item.
        /// </summary>
        [SerializeField]
        Abilities ABILITY;
        #endregion

        /// <summary>
        /// Metodo sobrescrito para otorgar la habildad deseada.
        /// </summary>
        protected override void OnItemObtainedAction()
        {
            LM.Raise_AbilityFruitObtained(SCORE, ABILITY);
            GameManager.Instance.AudioM.Raise_ItemPickupBig();
        }
    }
}
