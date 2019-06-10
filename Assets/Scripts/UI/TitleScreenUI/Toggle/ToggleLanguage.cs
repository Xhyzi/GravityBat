using UnityEngine;
using UnityEngine.UI;
using GravityBat_Constants;

/// <summary>
/// Script asociado a un toggle de idioma.
/// Cada toggle corresponde con un idioma diferente, pudiendo ser utilizado para activarlo.
/// </summary>
public class ToggleLanguage : _BaseToggle
{
    #region Attributes
    private GameManager GM;
    private TitleScreenManager TSM;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Idioma del toggle
    /// </summary>
    [SerializeField]
    Languages language;
    #endregion

    #region Override
    /// <summary>
    /// Recoge las referencias de los manager e inicializa el valor del toggle.
    /// </summary>
    protected override void InitToggleValue()
    {
        GM = GameManager.Instance;
        TSM = TitleScreenManager.Instance;
        InitLanguageToggle();
    }

    /// <summary>
    /// Suscribe un evento en OnEnable
    /// </summary>
    protected override void OnEnableAditionalActions()
    {
        TSM.OnLanguageToggleActive += HandleToggleGroupToggleActive;
    }

    /// <summary>
    /// Desuscribe un evento en OnDisable
    /// </summary>
    protected override void OnDisableAditionalActions()
    {
        TSM.OnLanguageToggleActive -= HandleToggleGroupToggleActive;
        toggle.onValueChanged.RemoveAllListeners();
    }

    /// <summary>
    /// Lleva a cabo las acciones correspondientes al cambio de valor del toggle.
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        if (toggle.isOn)
            SetToggleActive();
    }
    #endregion

    #region Event Handlers
    /// <summary>
    /// Desactiva el resto de toggles no correspontientes al lenguage activo
    /// </summary>
    /// <param name="lang">Idioma activo</param>
    private void HandleToggleGroupToggleActive(Languages lang)
    {
        if (lang != language)
        {
            toggle.interactable = true;
            toggle.isOn = false;
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Inicializa cada uno de los toggles.
    /// Activa el del idioma activo, desactiva el resto.
    /// </summary>
    private void InitLanguageToggle()
    {
        if (GM.Data.Language == language)
        {
            toggle.isOn = true;
            toggle.interactable = false;
        }
        else
        {
            toggle.isOn = false;
            toggle.interactable = true;
        }
    }

    /// <summary>
    /// Activa el toggle y desabilita su interactividad, para no poder
    /// deseleccionar el idioma activo (es necesario seleccionar otro).
    /// </summary>
    private void SetToggleActive()
    {
        toggle.interactable = false;    //Evita que se pueda desmarcar el idioma sin marcar otro.
        GM.Data.Language = language;    //Cambia el idioma
        GameManager.Instance.Raise_LanguageChanged();
        GameManager.Instance.Raise_UserConfigChanged();
        TSM.Raise_ToggleGroupToggleActive();
    }
    #endregion
}