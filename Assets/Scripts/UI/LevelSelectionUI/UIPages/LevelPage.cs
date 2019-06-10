using GravityBat_Localization;
using GravityBat_Constants;
using UnityEngine;
using TMPro;

/// <summary>
/// Script asociado a la pagina de nivel de la pantalla de seleccion de niveles
/// </summary>
public class LevelPage : MonoBehaviour
{
    #region Attributes
    private LevelSelectionManager LSM;
    private Animator anim;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias a LSM y anim.
    /// </summary>
    private void Awake()
    {
        LSM = LevelSelectionManager.Instance;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        LSM.OnLevelEnabledClick += HandleOnLevelEnabledClick;
        LSM.OnButtonBackClicked += HandleOnButtonBackClicked;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        LSM.OnLevelEnabledClick -= HandleOnLevelEnabledClick;
        LSM.OnButtonBackClicked -= HandleOnButtonBackClicked;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes a pulsar un boton de nivel desbloqueado.
    /// Activa la pagina del nivel seleccionado.
    /// </summary>
    /// <param name="levelIndex">indice del nivel del boton pulsado</param>
    private void HandleOnLevelEnabledClick(int levelIndex)
    {
        EnablePage(levelIndex);
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes a pulsar un boton de nivel bloqueado.
    /// Desactiva la pagina activa.
    /// </summary>
    /// <param name="state">Estado de LSM.</param>
    private void HandleOnButtonBackClicked(LSMState state)
    {
        if (anim.GetBool("Enabled") && state != LSMState.LEVEL_STATS_MENU)
            DisablePage();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina.
    /// </summary>
    /// <param name="levelIndex">Indice del nivel a cargar</param>
    private void EnablePage(int levelIndex)
    {
        anim.SetBool("Enabled", true);

        Transform[] t = GetComponentsInChildren<Transform>();

        for (int i = 0; i < t.Length; i++)
        {
            if (t[i].tag == "LevelText")
            {
                t[i].gameObject.GetComponent<TextMeshProUGUI>().text =
                   Strings.GetString(TEXT_TAG.TXT_LEVEL) + " " + (LSM.WorldIndex + 1) + "-" + (levelIndex + 1);
                return;
            }
        }
    }

    /// <summary>
    /// Desabilita la pagina activa actualmente.
    /// </summary>
    private void DisablePage()
    {
        anim.SetBool("Enabled", false);
    }
    #endregion
}
