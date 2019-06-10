using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Script relacionado con la pagina de nivel completado de la pantalla de nivel.
/// Muestra datos sobre el intento completo del jugador en el nivel actual.
/// </summary>
public class LevelCompletedMenupage : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Animator anim;
    private bool animFinished;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias de LM y animator.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();        
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        LM.OnButtonBackClicked += HandleButtonBackClicked;
        LM.OnLevelCompleted += HandleLevelCompleted;

        animFinished = false;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnButtonBackClicked -= HandleButtonBackClicked;
        LM.OnLevelCompleted -= HandleLevelCompleted;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo la accion correspondiente a la finalizacion del nivel
    /// </summary>
    private void HandleLevelCompleted()
    {
        EnablePage();
    }

    /// <summary>
    /// Lleva a cabo la accion correspondiente al boton back.
    /// </summary>
    private void HandleButtonBackClicked()
    {
        if (animFinished && LM.State == LevelState.COMPLETED)
            LM.Raise_MenuButtonClick();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Habilita la pagina
    /// </summary>
    private void EnablePage()
    {
        anim.SetBool("Enabled", true);
    }

    /// <summary>
    /// Deshabilita la pagina
    /// </summary>
    private void DisablePage()
    {
        if (anim.GetBool("Enabled"))
            anim.SetBool("Enabled", false);
    }

    /// <summary>
    /// Activa los botones de menu y ranking cuando termina la animacion del menu
    /// </summary>
    public void OnAnimationFinished()   //TODO: repara animacion.
    {
        animFinished = true;
        LM.Raise_CompletedLevelPageAnimationFinished();
    }
    #endregion
}
