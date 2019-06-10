using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Clase base utilizada para los triggers del nivel 
/// basados en un boxCollider2D de tipo trigger.
/// </summary>
public class _BaseLevelTrigger : MonoBehaviour
{
    /// <summary>
    /// Collider utilizado por el trigger.
    /// </summary>
    protected BoxCollider2D boxCol;

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge una referencia al boxCollider.
    /// </summary>
    private void Awake()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Ejecutado al producirse la colision de un objeto con el collider de tipo trigger.
    /// Si se produce una colision con el jugador lleva a cabo la accion asociada al trigger.
    /// </summary>
    /// <param name="col">collider del objeto con el que se produce la colision</param>
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == Constants.PLAYER_GO_TAG)
        {
            boxCol.enabled = false;
            TriggerAction();
        }
    }

    /// <summary>
    /// Metodo utilizado para herencia.
    /// Lleva a cabo la accion asociada al trigger.
    /// </summary>
    protected virtual void TriggerAction() { }
}
