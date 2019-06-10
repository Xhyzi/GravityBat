using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado a los GameObject de las cajas pequenias.
/// pueden ser convertidas en cerezas mediante el uso de una habilidad
/// del personaje.
/// </summary>
public class BoxIntoCherry : MonoBehaviour
{
    private LevelManager LM;

    private void Awake()
    {
        LM = LevelManager.Instance;
    }

    /// <summary>
    /// Se suscribe a los eventos
    /// </summary>
    private void OnEnable()
    {
        LM.OnBlockChangerClick += HandleBlockChangeClick;
    }

    /// <summary>
    /// Se desuscribe de los eventos
    /// </summary>
    private void OnDisable()
    {
        LM.OnBlockChangerClick -= HandleBlockChangeClick;
    }

    #region Event Handler
    private void HandleBlockChangeClick()
    {
        Instantiate(Resources.Load("prefabs/items/cherry_coin") as GameObject,
            transform.position, transform.rotation, transform.parent.transform);
        Destroy(gameObject);
    }
    #endregion
}
