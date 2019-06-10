using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Tutorial
{
    /// <summary>
    /// Asociado al objeto que controla el flujo de los tutoriales.
    /// Instanciado desde LevelManager si es necesario cargar los tutoriales.
    /// </summary>
    public class TutorialManager : MonoBehaviour
    {
        #region Attributes
        private LevelManager LM;
        private Canvas canvas;
        private GameObject tutorial;
        #endregion

        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour.
        /// Recoge referencias a LM y canvas. 
        /// Establece la camara y la capa del canvas.
        /// </summary>
        private void Awake()
        {
            LM = LevelManager.Instance;
            canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.sortingLayerName = "UI_1";
        }

        /// <summary>
        /// Se suscribe a los eventos de LM.
        /// </summary>
        private void OnEnable()
        {
            LM.OnTutorialTriggered += HandleTutorialTriggered;
            LM.OnTutorialEnded += HandleTutorialEnded;
        }

        /// <summary>
        /// Se desuscribe de los eventos de LM.
        /// </summary>
        private void OnDisable()
        {
            LM.OnTutorialTriggered -= HandleTutorialTriggered;
            LM.OnTutorialEnded -= HandleTutorialEnded;
        }

        #region Event Handlers
        /// <summary>
        /// Lleva a cabo las acciones correspondientes al evento que dispara el tutorial.
        /// Ejecuta el tutorial seleccionado. 
        /// </summary>
        /// <param name="tag">Tag del tutorial a inicializar.</param>
        private void HandleTutorialTriggered(TutorialTags tag)
        {
            if (tutorial == null)
            {
                switch (tag)
                {
                    case TutorialTags.TAP:
                        if (!GameManager.Instance.Data.IsTutorialTapDone)
                            TapTutorial();
                        break;

                    case TutorialTags.SWAP:
                        if (!GameManager.Instance.Data.IsTutorialSwapDone)
                            SwapTutorial();
                        break;

                    case TutorialTags.SWAP_CONSECUENCES:
                        if (!GameManager.Instance.Data.IsTutorialSwapConsecuencesDone)
                            SwapConsecuencesTutorial();
                        break;

                    case TutorialTags.POINTS:
                        if (!GameManager.Instance.Data.IsTutorialScoreDone)
                            PointsTutorial();
                        break;

                    case TutorialTags.MULTIPLIER:
                        if (!GameManager.Instance.Data.IsTutorialMultiplierDone)
                            MultiplierTutorial();
                        break;
                }
            }
        }

        /// <summary>
        /// Lleva a cabo las acciones corespondientes a la finalizacion de un tutorial.
        /// Carga el tutorial de 'SwapConsecuences' con un delay de 2s al terminar el tutorial de 'Swap'.
        /// </summary>
        /// <param name="tag">Tag del tutorial que ha finalizado.</param>
        private void HandleTutorialEnded(TutorialTags tag)
        {
            switch (tag)
            {
                case TutorialTags.SWAP:
                    Invoke("TriggerSwapConsecuencesTutorial", 2.5f);
                    break;

                case TutorialTags.SWAP_CONSECUENCES:
                    Invoke("TriggerMultiplierTutorial", 0.5f);
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Carga el tutorial de volar.
        /// </summary>
        private void TapTutorial()
        {
            InstantiateTutorial(Constants.TUTORIAL_RESOURCES_PATH + Constants.TUTORIAL_TAP, true);
        }

        /// <summary>
        /// Carga el tutorial de cambio de gravedad.
        /// </summary>
        private void SwapTutorial()
        {
            InstantiateTutorial(Constants.TUTORIAL_RESOURCES_PATH + Constants.TUTORIAL_SWAP, true);
        }

        /// <summary>
        /// Carga el tutorial sobre las consecuencias del cambio de gravedad.
        /// </summary>
        private void SwapConsecuencesTutorial()
        {
            InstantiateTutorial(Constants.TUTORIAL_RESOURCES_PATH + Constants.TUTORIAL_SWAP_CONSECUENCES, false);
        }

        /// <summary>
        /// Carga el tutorial sobre los objetos y los puntos.
        /// </summary>
        private void PointsTutorial()
        {
            InstantiateTutorial(Constants.TUTORIAL_RESOURCES_PATH + Constants.TUTORIAL_POINTS, false);
        }

        /// <summary>
        /// Carga el tutorial sobre las gemas de gravedad.
        /// </summary>
        private void MultiplierTutorial()
        {
            InstantiateTutorial(Constants.TUTORIAL_RESOURCES_PATH + Constants.TUTORIAL_MULTIPLIER, false);
        }

        /// <summary>
        /// Notifica a LM para levantar el evento del tutorial de 'SwapConsecuences'
        /// </summary>
        private void TriggerSwapConsecuencesTutorial()
        {
            if (LM.State == LevelState.RUNNING)
                LM.Raise_TutorialTriggered(TutorialTags.SWAP_CONSECUENCES);
        }

        private void TriggerMultiplierTutorial()
        {
            if (LM.State == LevelState.RUNNING)
            {
                LM.Raise_TutorialTriggered(TutorialTags.MULTIPLIER);
                Debug.LogWarning("Lanzando tutorial multiplier");
            }
        }

        /// <summary>
        /// Instancia el GameObject con el controlador del tutorial correspondiente.
        /// </summary>
        /// <param name="tutorialPath">Path del prefab a cargar</param>
        /// <param name="needsTap">Indica si el tutorial requiere hacer uso de los paneles de volar o cambiar gravedad.</param>
        private void InstantiateTutorial(string tutorialPath, bool needsTap)
        {
            tutorial = Instantiate(Resources.Load(tutorialPath) as GameObject);
            tutorial.transform.SetParent(transform, false);
            canvas.sortingLayerName = needsTap ? "UI_1" : "UI_2";
        }
        #endregion
    }
}
