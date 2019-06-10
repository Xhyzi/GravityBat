using UnityEngine;

/// <summary>
/// Script asociado al HUD que muestra los diamantes especiales obtenidos.
/// </summary>
public class SecretDiamondHUD : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Animator anim;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Id del diamante.
    /// Toma un valor de 0-2 (hay 3 por nivel).
    /// </summary>
    [SerializeField]
    int diamondIndex;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge referencias de LM y animator.
    /// Establece la animacion inicial del diamante.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();

        CheckPreviouslyFound();
    }

    /// <summary>
    /// Suscribe los eventos
    /// </summary>
    private void OnEnable()
    {
        LM.OnDiamondObtained += HandleDiamondObtained;
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnDiamondObtained -= HandleDiamondObtained;
    }

    #region EventHandlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes a la obtencion del diamante
    /// </summary>
    /// <param name="id">id del diamante obtenido</param>
    private void HandleDiamondObtained(int score, int id)
    {
        if (id == diamondIndex)
            ObtainedDiamond();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Cambia la pagina de animaciones del diamante a la de 'Obtained'
    /// </summary>
    private void ObtainedDiamond()
    {
        anim.SetBool("Obtained", true);
    }

    /// <summary>
    /// Comprueba si el diamante ha sido encontrado con anterioridad y de ser asi cambia la pagina de animaciones.
    /// </summary>
    private void CheckPreviouslyFound()
    {
        if (GameManager.Instance.Data.LevelArray[LM.WorldIndex, LM.LevelIndex].SecretDiamonds[diamondIndex])
        {
            anim.SetBool("Found", true);
        }
    }
    #endregion

}
