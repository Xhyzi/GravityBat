using GravityBat_Localization;
using GravityBat_Constants;
using UnityEngine;
using TMPro;

/// <summary>
/// Script asociado a la pagina de nivel bloqueado de la pantalla de seleccion de nivel
/// </summary>
public class LevelBlockedPage : MonoBehaviour
{
    #region Attributes
    private TextMeshProUGUI[] tMeshArray;
    private Transform[] transArray;
    private LevelSelectionManager LSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias de LSM, transform de los hijos, TextMesh de los hijos y el animator.
    /// </summary>
    private void Awake()
    {
        LSM = LevelSelectionManager.Instance;
        transArray = GetComponentsInChildren<Transform>();
        tMeshArray = GetComponentsInChildren<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Suscribe los eventos de LSM.
    /// </summary>
    private void OnEnable()
    {
        LSM.OnLevelBlockedClick += HandleLevelBlockClick;
        LSM.OnButtonBackClicked += HandleBackButtonClicked;
    }

    /// <summary>
    /// Desuscribe los eventos de LSM.
    /// </summary>
    private void OnDisable()
    {
        LSM.OnLevelBlockedClick -= HandleLevelBlockClick;
        LSM.OnButtonBackClicked -= HandleBackButtonClicked;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton de nivel bloqueado.
    /// Habilita la pagina de nivel bloqeado.
    /// </summary>
    /// <param name="levelIndex">Indice del nivel cuya pagina se va a habilitar</param>
    private void HandleLevelBlockClick(int levelIndex)
    {
        EnablePage(levelIndex);
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton de back/back@android
    /// Desactiva la pagina de nivel bloqueado activa actualmente.
    /// </summary>
    /// <param name="state">Estado de LSM.</param>
    private void HandleBackButtonClicked(LSMState state)
    {
        DisablePage();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina
    /// </summary>
    /// <param name="levelIndex">Indice del nivel a cargar en la pagina</param>
    private void EnablePage(int levelIndex)
    {
        anim.SetBool("Enabled", true);

        //Asigna valor a los textos de la pagina
        for (int i = 0; i < transArray.Length; i++)
        {
            switch (transArray[i].name)
            {
                case "LevelText":
                    transArray[i].gameObject.GetComponent<TextMeshProUGUI>().text =
                        (Strings.GetString(TEXT_TAG.TXT_LEVEL)) + " " + (LSM.WorldIndex + 1) + "-" + (levelIndex + 1);
                    break;

                case "InfoText":
                    transArray[i].gameObject.GetComponent<TextMeshProUGUI>().text = levelIndex < 4 ?
                        Strings.GetString(TEXT_TAG.TXT_LEVEL_BLOCKED_INFO) : Strings.GetString(TEXT_TAG.TXT_SPECIAL_LEVEL_BLOCKED_INFO);
                    break;

                case "TextBlocked":
                    transArray[i].gameObject.GetComponent<TextMeshProUGUI>().text = Strings.GetString(TEXT_TAG.TXT_LEVEL_BLOCKED);
                    break;
            }
        }
    }

    /// <summary>
    /// Desabilita la pagina actualmente activa.
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
            anim.SetBool("Enabled", false);
    }
    #endregion

}
