using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado a la pagina de estadisticas de un nivel.
/// Se encuentra dentro de la pantalla de seleccion de niveles.
/// </summary>
public class LevelStatsPage : MonoBehaviour
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
    /// Suscribe los eventos de LSM.
    /// </summary>
    private void OnEnable()
    {
        LSM.OnButtonBackClicked += HandleButtonBackClick;
        LSM.OnButtonLevelStatsClicked += HandleButtonStatsClick;
    }

    /// <summary>
    /// Desuscribe los eventos de LSM.
    /// </summary>
    private void OnDisable()
    {
        LSM.OnButtonBackClicked -= HandleButtonBackClick;
        LSM.OnButtonLevelStatsClicked -= HandleButtonStatsClick;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton back/back@android.
    /// </summary>
    /// <param name="state">Estado de LSM.</param>
    private void HandleButtonBackClick(LSMState state)
    {
        DisablePage();
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes al boton de estadisticas del nivel.
    /// </summary>
    private void HandleButtonStatsClick()
    {
        EnablePage();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina.
    /// </summary>
    private void EnablePage()
    {
        anim.SetBool("Enabled", true);
    }

    /// <summary>
    /// Deshabilita la pagina.
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
            anim.SetBool("Enabled", false);
    }
    #endregion
}
