using UnityEngine;

/// <summary>
/// Se encarga de realizar el fade en los cambios de Scene.
/// Se suscribe a eventos de GameManager.
/// </summary>
public class SceneFade : MonoBehaviour
{
    private GameManager GM;     
    private Animator animator;   

    /// <summary>
    /// Recoge las instancias del GameManager y el animator.
    /// Suscribe el evento de cambio de GameState.
    /// </summary>
    private void Awake()
    {
        GM = GameManager.Instance;  
        animator = GetComponent<Animator>();
        GM.OnGameStateChanged += HandleGameStateChanged;
    }

    /// <summary>
    /// Se suscribe a los eventos
    /// </summary>
    private void OnEnable()
    {
        
    }

    /// <summary>
    /// Se desuscribe de los eventos
    /// </summary>
    /*private void OnDisable()
    {
        GM.OnGameStateChanged -= HandleGameStateChanged;
    }*/

    #region Event Handlers
    /// <summary>
    /// Activa el trigger que desencadena la animación 'fade-out' del scene.
    /// Notifica a GameManager para levantar el evento.
    /// </summary>
    private void HandleGameStateChanged()
    {
        if (animator != null)
        {
            animator.SetTrigger("FadeOut"); //Activa el trigger que realiza el fade.
            GM.AudioM.Raise_FadeOut();
        }
    }

    /// <summary>
    /// Llamado desde el objeto animator al terminar la animación.
    /// Notifica a GameManager para levantar el evento.
    /// </summary>
    public void OnFadeCompleted()
    {
        GM.Raise_FadeCompleted();
    }
    #endregion
}
