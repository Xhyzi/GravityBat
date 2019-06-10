using GravityBat_Constants;
using UnityEngine;

/// <summary>
/// Desplaza un fondo (bg) de forma infinita en la dirección indicada.
/// Puede ser utilizado con varias capas a diferentes velocidades para 
/// obtener un efecto de profundidad.
/// </summary>
public class IdleCameraBgParallaxer : MonoBehaviour
{
    #region Serialized Fields
    /// <summary>
    /// Velocidad de scroll lateral
    /// </summary>
    [SerializeField]
    float scrollSpeed;  

    /// <summary>
    /// Tamanio del bg a desplazar
    /// </summary>
    [SerializeField]
    float bgSize;      
    
    /// <summary>
    /// Dirección en la que se desplaza el bg
    /// </summary>
    [SerializeField]
    Direction dir;
    #endregion

    /// <summary>
    /// Es llamado una vez por frameUpdate
    /// </summary>
    private void LateUpdate()
    {
        ParallaxBg();
    }

    #region Methods
    /// <summary>
    /// Lleva a cabo el desplazamiento del bg en la direccion y velocidad indicadas.
    /// </summary>
    private void ParallaxBg()
    {
        switch (dir)
        {
            case Direction.RIGHT:
                transform.position -= Vector3.right * scrollSpeed;
                break;

            case Direction.LEFT:
                transform.position += Vector3.right * scrollSpeed;
                break;
        }

        //Mueve el bg a la posicion inicial cuando completa el recorrido
        if (bgSize < -(transform.position.x))
        {
            transform.position = Vector3.right * 0;
        }
    }
    #endregion
}
