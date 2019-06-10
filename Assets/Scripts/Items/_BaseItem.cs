using UnityEngine;
using TMPro;

namespace GravityBat_Items
{
    /// <summary>
    /// Clase base para heredar desde los items.
    /// Controla las colisiones con el item, la animacion correspondiente y su posterior destruccion, asi como la asignacion de los puntos por obtener el objeto.
    /// </summary>
    public class _BaseItem : MonoBehaviour
    {
        #region Attributes [protected]
        protected LevelManager LM;
        protected Animator anim;
        protected CircleCollider2D circleCollider;
        #endregion

        #region Serialized Fields
        [SerializeField]
        protected int SCORE;   //Cantidad de puntos obtenidos por recoger el objeto
        #endregion

        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour.
        /// Recoge referencias de LevelManager, animator y el collider del objeto.
        /// Ejecuta el metodo 'OnAwakeAction'
        /// </summary>
        protected void Awake()
        {
            LM = LevelManager.Instance;
            anim = GetComponent<Animator>();
            circleCollider = GetComponent<CircleCollider2D>();
            OnAwakeAction();
        }

        /// <summary>
        /// Suscribe los eventos
        /// </summary>
        protected void OnEnable()
        {
            SubscribeEvents();
        }

        /// <summary>
        /// Desuscribe los eventos
        /// </summary>
        protected void OnDisable()
        {
            UnsubscribeEvents();
        }

        #region Methods
        /// <summary>
        /// Ejecutado cuando un objeto colisiona con el item.
        /// Si se produce una colision con el jugador este obtendra el item
        /// obteniendo los puntos correspondientes asi como ejecutando OnTriggerEnterAction
        /// </summary>
        /// <param name="col">collider del objeto con el que se ha colisionado</param>
        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                circleCollider.enabled = false;     //Desactiva el collider (impide multiples activaciones)
                anim.SetTrigger("ObtainedItem");    //Activa el trigger del animator
                OnItemObtainedAction();             //Ejecuta metodo sobrescribible con acciones
            }
        }

        /// <summary>
        /// Llamado cuando finaliza la animacion de recogida del objeto.
        /// Muestra los puntos obtenidos al recoger el objeto
        /// </summary>
        protected void OnItemObtainedAnimation() 
        {
            ShowPoints();      
        }

        /// <summary>
        /// Destruye el item
        /// </summary>
        protected void DestroyItem()
        {
            Destroy(gameObject);
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Metodo para sobrescribir en las clases hijas. 
        /// Se ejecuta al recoger el item, por defecto lo notifica al AudioManager para reproducir el sonido correspondiente.
        /// </summary>
        protected virtual void OnItemObtainedAction() { GameManager.Instance.AudioM.Raise_ItemPickupSmall(); }

        /// <summary>
        /// Metodo para sobrescribir en las clases hijas.
        /// Realiza acciones adicionales durante el Awake del script.
        /// </summary>
        protected virtual void OnAwakeAction() { }

        /// <summary>
        /// Muestra una pequeña animacion con los puntos obtenidos al recoger el objeto.
        /// </summary>
        protected virtual void ShowPoints()
        {
            LM.Raise_ItemAnimationFinished(SCORE * LM.Data.Multiplier, transform.position);

            Invoke("DestroyItem", 1);
        }

        /// <summary>
        /// Eventos que debe suscribir en OnEnable
        /// </summary>
        protected virtual void SubscribeEvents() { }

        /// <summary>
        /// Eventos que debe desuscribir en OnDisable
        /// </summary>
        protected virtual void UnsubscribeEvents() { }
        #endregion
    }
}
