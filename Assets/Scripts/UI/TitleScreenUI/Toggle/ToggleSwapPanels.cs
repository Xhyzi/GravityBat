using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script relacionado con el toggle que intercambia los paneles del nivel.
/// Se encarga de comunicar los cambios al GameManager
/// </summary>
public class ToggleSwapPanels : _BaseToggle
{
    /// <summary>
    /// Inicializa el valor del toggle desde los datos del GM.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = GameManager.Instance.Data.SwapLevelPanels;
    }

    protected override void OnEnableAditionalActions()
    {
        base.OnEnableAditionalActions();
        GameManager.Instance.OnTutorialToggle += HandleTutorialToggle;
    }

    protected override void OnDisableAditionalActions()
    {
        base.OnDisableAditionalActions();
        GameManager.Instance.OnTutorialToggle -= HandleTutorialToggle;

    }

    /// <summary>
    /// Guarda el estado del toggle en los datos de GM y se lo notifica para elevar el evento
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        GameManager.Instance.Data.SwapLevelPanels = toggle.isOn;
        GameManager.Instance.Raise_UserConfigChanged();
        GameManager.Instance.Raise_SwapControlsToggle(toggle.isOn);
    }

    private void HandleTutorialToggle(bool enabled)
    {
        if (enabled)
        {
            toggle.isOn = false;
            OnValueChangedChecked();
        }
    }
}
