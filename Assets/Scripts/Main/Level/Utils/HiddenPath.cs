using GravityBat_Constants;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

/// <summary>
/// Script relacionado con los caminos ocultos.
/// Cuando el jugador colisione contra el camino oculto este se vuelve transparente, mostrandose.
/// </summary>
public class HiddenPath : MonoBehaviour
{
    #region Attributes
    private Tilemap tilemap;
    private BoxCollider2D boxCol;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Velocidad de desvanecimiento
    /// </summary>
    [SerializeField]
    float fadeOutSpeed = Constants.HIDDEN_PATH_FADE_OUT_SPEED;
    #endregion

    /// <summary>
    /// Llamado al instanciar el MonoBehaviour.
    /// Recoge referencias al Tilemap y el BoxCollider2D
    /// </summary>
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    #region Methods
    /// <summary>
    /// Si el jugador atraviesa el camino oculto, este se vuelve transparente
    /// </summary>
    /// <param name="collision">objeto con el que se produce la colission</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.PLAYER_GO_TAG)
        {
            StartCoroutine(CoroutineFadeHiddenPath());
            boxCol.enabled = false;
        }
    }

    /// <summary>
    /// Corrutina que lleva a cabo el fade del camino oculto,
    /// aumentando la transparencia en cada paso y cediendo la ejecucion
    /// </summary>
    /// <returns>yield que cede la ejecucion durante el tiempo marcado</returns>
    private IEnumerator CoroutineFadeHiddenPath()
    {
        while (tilemap.color.a > 0)
        {
            tilemap.color = new Color(
                tilemap.color.r,
                tilemap.color.g,
                tilemap.color.b,
                tilemap.color.a - fadeOutSpeed);

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
