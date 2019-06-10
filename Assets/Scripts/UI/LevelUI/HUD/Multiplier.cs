using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script asociado al multiplicador de puntos mostrado en la interfaz
/// </summary>
[System.Obsolete("La clase está obsoleta -> A ser posible utilizar Multiplier_Text.")]
public class Multiplier : MonoBehaviour
{
    #region Attriutes
    private LevelManager LM;
    private Animator anim;
    private Image xImage;       //Referencia al Image x
    private Image digitImage1;  //Referencia al Image del primer digito
    private Image digitImage2;  //Referencia al Image del segundo digito
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Mutiplicacion
    /// </summary>
    [SerializeField]
    GameObject X;

    /// <summary>
    /// Primer digito
    /// </summary>
    [SerializeField]
    GameObject Digit1;

    /// <summary>
    /// Segundo digito
    /// </summary>
    [SerializeField]
    GameObject Digit2;

    /// <summary>
    /// Contiene los sprites de los digitos
    /// </summary>
    [SerializeField]
    Sprite[] DigitSprites; 
    
    /// <summary>
    /// Contiene el digito vacio
    /// </summary>
    [SerializeField]
    Sprite EmptySprite;
    #endregion

    /// <summary>
    /// Ejecutado al instanciar el Monobehaviour.
    /// Recoge referencias a LM, animator y a los componentes Image.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
        anim = GetComponent<Animator>();

        xImage = X.GetComponent<Image>();
        digitImage1 = Digit1.GetComponent<Image>();
        digitImage2 = Digit2.GetComponent<Image>();
    }

    /// <summary>
    /// Se suscribe a los eventos de LM.
    /// </summary>
    private void OnEnable()
    {
        LM.OnMultiplierUpdate += HandlerOnMultiplierUpdate;
    }

    /// <summary>
    /// Se desuscribe a los eventos de LM.
    /// </summary>
    private void OnDisable()
    {
        LM.OnMultiplierUpdate -= HandlerOnMultiplierUpdate;
    }

    #region Event Handlers
    /// <summary>
    /// Actualiza el valor del multiplicador
    /// </summary>
    void HandlerOnMultiplierUpdate()
    {
        LM.Data.Multiplier += 1;
        anim.SetTrigger("StartAnimation");
        SetMultiplierGraphics();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Es llamado por el animator cuando termina la animacion del multiplicador sin ser encadenada.
    /// Restablece el multiplicador a 1.
    /// </summary>
    public void OnMultiplierAnimEnd()
    {
        LM.Data.Multiplier = 1;
    }

    /// <summary>
    /// Establece los graficos del multiplicador en la GUI.
    /// </summary>
    private void SetMultiplierGraphics()
    {
        if (LM.Data.Multiplier < 10)
        {
            digitImage1.sprite = DigitSprites[LM.Data.Multiplier];
            digitImage2.sprite = EmptySprite;
        }
        else
        {
            digitImage1.sprite = DigitSprites[LM.Data.Multiplier / 10];
            digitImage2.sprite = DigitSprites[LM.Data.Multiplier % 10];
        }
    }
    #endregion
}
