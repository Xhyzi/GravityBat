using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Tutorial {

    /// <summary>
    /// Script asociado al Trigger de un tutorial.
    /// Al colisionar el jugador con este trigger se disparara un tutorial
    /// </summary>
    public class TutorialTrigger : MonoBehaviour
    {
        #region Attributes
        private BoxCollider2D boxCol;
        private LevelManager LM;

        /// <summary>
        /// Tag del tutorial activado por el trigger.
        /// </summary>
        [SerializeField]
        TutorialTags tutorial;
        #endregion

        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour.
        /// Recoge referencias al LM y al collider.
        /// </summary>
        private void Awake()
        {
            LM = LevelManager.Instance;
            boxCol = GetComponent<BoxCollider2D>();
        }

        /// <summary>
        /// Llamado cuando algun objeto entre en contacto con el collider de tipo trigger.
        /// Notifica a LM para que eleve el evento de inicio de tutorial.
        /// </summary>
        /// <param name="collision">Collider del objeto con el que se ha colisionado</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && LM.WorldIndex == 0 && LM.LevelIndex == 0 
                && !IsTutorialDone() && GameManager.Instance.Data.EnableTutorials)
            {
                boxCol.enabled = false;
                LM.Raise_TutorialTriggered(tutorial);
            }
        }

        private bool IsTutorialDone()
        {
            bool temp = true;

            switch (tutorial)
            {
                case TutorialTags.TAP:

                    temp = GameManager.Instance.Data.IsTutorialTapDone;
                    break;

                case TutorialTags.SWAP:
                    temp = GameManager.Instance.Data.IsTutorialSwapDone;
                    break;

                case TutorialTags.SWAP_CONSECUENCES:
                    temp = GameManager.Instance.Data.IsTutorialSwapConsecuencesDone;
                    break;

                case TutorialTags.MULTIPLIER:
                    temp = GameManager.Instance.Data.IsTutorialMultiplierDone;
                    temp = GameManager.Instance.Data.EnableTutorials;
                    break;

                case TutorialTags.POINTS:
                    temp = GameManager.Instance.Data.IsTutorialScoreDone;
                    break;
            }

            return temp;
        }
    }
}

