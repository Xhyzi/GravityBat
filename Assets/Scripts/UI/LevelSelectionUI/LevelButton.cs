using UnityEngine.EventSystems;
using GravityBat_Localization;
using GravityBat_Constants;
using GravityBat_Data;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


/// <summary>
/// Script relacionado con los botones de selección de nivel.
/// Cada boton representa un nivel.
/// </summary>
public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    #region Attributes
    private TextMeshProUGUI[] buttonTexts;  //Array con los distintos textos de cada boton
    private Level level;                    //Nivel que representa el boton
    #endregion

    #region Serialized fields
    /// <summary>
    /// Indice del nivel que representa el boton
    /// </summary>
    [SerializeField]
    int buttonLevelIndex;            

    /// <summary>
    /// Indice del mundo del nivel que representa el boton
    /// </summary>
    [SerializeField]
    int buttonWorldIndex;

    /// <summary>
    /// Game Object que contiene el diamante del boton
    /// </summary>
    [SerializeField]
    GameObject Diamond;         

    /// <summary>
    /// Indica si el nivel es infinito o no.
    /// </summary>
    [SerializeField]
    bool infinite = false;       //Variable que indica si es infinito o no (Editor)
    #endregion

    /// <summary>
    /// Llamado al instanciar el MonoBehaviour.
    /// Recoge referencias a los TextMeshProUGUI del boton y al objeto del nivel.
    /// </summary>
    private void Awake()
    {
        buttonTexts = GetComponentsInChildren<TextMeshProUGUI>();                           //Obtiene los textos
        level = GameManager.Instance.Data.LevelArray[buttonWorldIndex, buttonLevelIndex];   //Obtiene el nivel  
    }

    /// <summary>
    /// Llamado antes del primer frame Update.
    /// Inicializa los textos de los botones y la transparencia de los mismos (dependiendo de si estan activados o no).
    /// </summary>
    private void Start()
    {
        LoadButtonText();   
        SetLevelAlpha();
    }

    #region Methods
    /// <summary>
    /// Carga los textos del boton.
    /// </summary>
    private void LoadButtonText()
    {
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            switch (buttonTexts[i].tag)
            {
                case Constants.TEXT_LEVEL_GO_TAG:
                    buttonTexts[i].text = Strings.GetString(TEXT_TAG.TXT_LEVEL) + " " +
                                            (buttonWorldIndex + 1) + "-" + (buttonLevelIndex+ 1); 
                    break;

                case Constants.TEXT_SCORE_GO_TAG:
                    buttonTexts[i].text = level.Unlocked ? level.BestScore.ToString() : "";
                    break;

                case Constants.TEXT_DIAMONDS_GO_TAG:
                    buttonTexts[i].text = level.Unlocked ? level.DiamondsCount().ToString() : "";
                    break;
            }
        }
    }

    /// <summary>
    /// Aplica transparencia al boton si el nivel esta bloqueado
    /// </summary>
    private void SetLevelAlpha()
    {
        if (!level.Unlocked)    
        {
            Image img = GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0.4f);

            if (!infinite)
            {
                Diamond.GetComponent<Image>().enabled = false;
            }
        }
    }
    #endregion

    #region IPointerClickHandler
    /// <summary>
    /// Implementacion de la Interface IPointerClickhandler, 
    /// el metodo es llamado al clickar sobre el boton (en el release)
    /// </summary>
    /// <param name="eventData">datos del evento</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        LevelSelectionManager.Instance.Raise_LevelButtonClick(buttonLevelIndex, level.Unlocked);
    }
    #endregion
}
