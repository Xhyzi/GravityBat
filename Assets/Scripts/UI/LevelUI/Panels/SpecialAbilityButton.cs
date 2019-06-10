using UnityEngine;
using UnityEngine.EventSystems;
using GravityBat_Constants;

/// <summary>
/// Script asociado al boton que controla la habilidad adquirida.
/// </summary>
public class SpecialAbilityButton : MonoBehaviour, IPointerDownHandler
{
    #region Attributes
    private LevelManager LM;
    #endregion

    #region Serialized Fields
    [SerializeField]
    Abilities ability;
    #endregion

    private void Awake()
    {
        LM = LevelManager.Instance;
        Invoke("DestroyButton", 10);
    }

    /// <summary>
    /// Se suscribe a los eventos
    /// </summary>
    private void OnEnable()
    {
        LM.OnAbilityAcquired += HandleAbilityAcquired;
    }

    /// <summary>
    /// Se desuscribe de los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnAbilityAcquired -= HandleAbilityAcquired;
    }

    #region IPointerDownClick
    /// <summary>
    /// Ejecutado al pulsar el boton
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        LM.Raise_SpecialButtonClick(ability);
    }
    #endregion

    #region Event Handlers
    private void HandleAbilityAcquired(Abilities ability)
    {
        DestroyButton();
    }
    #endregion

    #region Methods
    private void DestroyButton()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
    #endregion
}
