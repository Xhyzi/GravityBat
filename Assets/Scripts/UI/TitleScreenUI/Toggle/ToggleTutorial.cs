using UnityEngine;
using System.Collections;

/// <summary>
/// Toggle utilizado para activar/desactivar los tutoriales del nivel 1.
/// </summary>
public class ToggleTutorial : _BaseToggle
{
    /// <summary>
    /// Inicializa el valor del toggle desde los datos del GM.
    /// </summary>
    protected override void InitToggleValue()
    {
        toggle.isOn = GameManager.Instance.Data.EnableTutorials;
    }

    protected override void OnEnableAditionalActions()
    {
        base.OnEnableAditionalActions();
        GameManager.Instance.OnSwapControlsToggle += HandleControlSwapToggle;
    }

    protected override void OnDisableAditionalActions()
    {
        base.OnDisableAditionalActions();
        GameManager.Instance.OnSwapControlsToggle -= HandleControlSwapToggle;
    }

    /// <summary>
    /// Guarda el estado del toggle en los datos de GM y lo notifica a GM para elevar los eventos. -> guarda la configuracion
    /// </summary>
    protected override void OnValueChangedChecked()
    {
        GameManager.Instance.Data.EnableTutorials = toggle.isOn;
        GameManager.Instance.Raise_UserConfigChanged();
        GameManager.Instance.Raise_TutorialToggle(toggle.isOn);
    }

    private void HandleControlSwapToggle(bool enabled)
    {
        if (enabled)
        {
            toggle.isOn = false;
            OnValueChangedChecked();
        }
    }
}
