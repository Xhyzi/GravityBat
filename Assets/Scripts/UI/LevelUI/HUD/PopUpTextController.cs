using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Controla la aparición de textos PopUp en un canvas durante el nivel
/// </summary>
public class PopUpTextController : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Canvas canvas;
    #endregion

    private void Awake()
    {
        LM = LevelManager.Instance;
        canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        LM.OnReadyTextTrigger += HandleReadyTextTriggered;
        LM.OnStartTextTrigger += HandleStartTextTriggered;
        LM.OnAbilityAcquired += HandleAbilityAcquired;
    }

    private void OnDisable()
    {
        LM.OnReadyTextTrigger -= HandleReadyTextTriggered;
        LM.OnStartTextTrigger -= HandleStartTextTriggered;
        LM.OnAbilityAcquired -= HandleAbilityAcquired;
    }

    #region Event Handlers
    private void HandleStartTextTriggered()
    {
        Instantiate(Resources.Load(Constants.START_POPUP_TEXT_PATH) as GameObject, transform.position, transform.rotation, transform);
    }

    private void HandleReadyTextTriggered()
    {
        Instantiate(Resources.Load(Constants.READY_POPUP_TEXT_PATH) as GameObject, transform.position, transform.rotation, transform);
    }

    private void HandleAbilityAcquired(Abilities ability)
    {
        switch (ability)
        {
            case Abilities.BLOCK_CHANGER:
                Instantiate(Resources.Load(Constants.BUTTON_BLOCK_CHANGER) as GameObject,
                    transform.position, transform.rotation, transform).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                Debug.LogWarning(transform.position.x + " " + transform.position.y);
                break;

            case Abilities.GHOST:
                //Instantiate(Resources.Load("") as GameObject, new Vector3(0, 0, 0), transform.rotation, transform.parent.transform);
                break;
        }
    }
    #endregion
}
