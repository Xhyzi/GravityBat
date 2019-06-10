using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Controla el Input proveniente de Android.
/// Actualmente solo registra el button back, pero puede utilizarse para dar soporte a gamepads y
/// registrar 'arrastres' en la pantalla tactil para desplazarse entre paginas (menu de seleccion de mundos)
/// </summary>
public class InputManager : MonoBehaviour
{
    private GameManager GM;

    private void Awake()
    {
        GM = GameManager.Instance;
    }
    
    /// <summary>
    /// Si se pulsa el boton back de android (tecla escape) lo notifica a GameManager
    /// para que eleve el evento correspondiente.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GM.Raise_AndroidBackPressed();
        }
    }

    /// <summary>
    /// Si la aplicacion pierde el foco y se encontraba en la pantalla de un nivel
    /// activara la opcion de pausa (para evitar una colision casi segura al
    /// recuperar el foco en medio del nivel).
    /// </summary>
    /// <param name="pause">indica si se ha pausado la app o no,
    /// Este mensaje es enviado al perder el foco, ya que que RunInBackground esta desactivado.</param>
    private void OnApplicationPause(bool pause)
    {
        if (pause && GM.GameState == GameState.LEVEL && 
            (LevelManager.Instance.State == LevelState.RUNNING || 
            LevelManager.Instance.State == LevelState.INIT))
        {
            LevelManager.Instance.Raise_ButtonBackClicled();
        } 
    }
}
