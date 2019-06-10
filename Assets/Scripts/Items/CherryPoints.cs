using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Items
{
    /// <summary>
    /// Script utilizado para el Item 'CherryCoin'. Hereda de BaseItem.
    /// Objeto basico del juego, proporciona una pequenia cantidad de puntos
    /// </summary>
    public class CherryPoints : _BaseItem
    {
        #region Override Methods
        protected override void OnItemObtainedAction()
        {
            base.OnItemObtainedAction();
            LM.Raise_CherryObtained(SCORE);
        }

        /// <summary>
        /// Se suscribe al evento de la habilidad para cambiar las cerezas por bloques
        /// </summary>
        protected override void SubscribeEvents()
        {
            LM.OnBlockChangerClick += HandleBlockChanger; 
        }

        /// <summary>
        /// Se desuscribe de los eventos
        /// </summary>
        protected override void UnsubscribeEvents()
        {
            LM.OnBlockChangerClick -= HandleBlockChanger;
        }
        #endregion

        #region Event Handlers
        private void HandleBlockChanger()
        {
            if (GetComponent<CircleCollider2D>().enabled)
            {
                Instantiate(Resources.Load(Constants.PHYSHIC_OBJECTS_PATH + Constants.CRATE_PATH) as GameObject,
                transform.position, transform.rotation, transform.parent.transform);
                Destroy(gameObject);
            } 
        }
        #endregion
    }
}
