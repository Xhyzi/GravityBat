using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Tutorial {

    /// <summary>
    /// Script asociado a un tutorial.
    /// El tipo de tutorial puede ser escogido desde el editor de Unity, 
    /// seleccionando un valor para la variable 'tutorial';
    /// </summary>
    public class Tutorial : MonoBehaviour
    {
        #region Attributes
        private LevelManager LM;
        private Animator anim;

        /// <summary>
        /// Tag del tipo de tutorial.
        /// </summary>
        [SerializeField]
        TutorialTags tutorial;
        #endregion

        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour.
        /// Recoge referencias a LM y anim.
        /// </summary>
        private void Awake()
        {
            LM = LevelManager.Instance;
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Se suscribe a los eventos de LM en funcion del tipo de tutorial.
        /// </summary>
        private void OnEnable()
        {
            switch (tutorial)
            {
                case TutorialTags.TAP:
                    LM.OnTapButton += HandleTutorialCompleted;
                    break;

                case TutorialTags.SWAP:
                    LM.OnGravityButton += HandleGravityTutorialCompleted;
                    break;

                case TutorialTags.SWAP_CONSECUENCES:
                case TutorialTags.POINTS:
                case TutorialTags.MULTIPLIER:
                    LM.OnButtonOK += HandleTutorialCompleted;
                    break;
            }
        }

        /// <summary>
        /// Se desuscribe de los eventosd de LM en funcion del tipo de tutorial.
        /// </summary>
        private void OnDisable()
        {
            switch (tutorial)
            {
                case TutorialTags.TAP:
                    LM.OnTapButton -= HandleTutorialCompleted;
                    break;

                case TutorialTags.SWAP:
                    LM.OnGravityButton -= HandleGravityTutorialCompleted;
                    break;

                case TutorialTags.SWAP_CONSECUENCES:
                case TutorialTags.POINTS:
                case TutorialTags.MULTIPLIER:
                    LM.OnButtonOK -= HandleTutorialCompleted;
                    break;
            }
        }


        #region Event Handlers
        /// <summary>
        /// Lleva a cabo las acciones correspondientes a la finalizacion del tutorial.
        /// Desactiva el tutorial terminado.
        /// </summary>
        private void HandleTutorialCompleted()
        {
            LM.Raise_TutorialEnded(tutorial);
            anim.SetTrigger("Disabled");
        }

        /// <summary>
        /// Lleva a cabo las acciones correspondientes a la finalizacion del tutorial de 'Swap'.
        /// </summary>
        /// <param name="enabled">Unused</param>
        private void HandleGravityTutorialCompleted(bool enabled)
        {
            HandleTutorialCompleted();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Llamado cuando termina la animacion de FadeOut del canvas del tutorial.
        /// Destruye el GameObject una vez este ha desaparecido de la pantalla.
        /// </summary>
        private void FadeOutFinished()
        {
            Destroy(transform.gameObject);
        }
        #endregion
    }
}
