using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para la camara principald el nivel.
/// Permite a la camara seguir la posicion de un GameObject, manteniendo el offset de diferencia.
/// </summary>
public class CameraFollowPlayer : MonoBehaviour
{
    /// <summary>
    /// Objeto del jugador.
    /// </summary>
    [SerializeField]
    GameObject player;   
    private Vector3 offset;     

    /// <summary>
    /// Llamado antes del primer frame Update.
    /// Calcula el desfase entre el avatar y la camara.
    /// </summary>
    private void Start()
    {
        offset = transform.position - player.transform.position;   
    }

    /// <summary>
    /// Ejecutado una vez por frame Update.
    /// Modifica la posicion de la camara para seguir al jugador, manteniendo el offset.
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
