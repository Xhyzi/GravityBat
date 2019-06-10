using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script utilizado para controlar el comportamiento del avatar del jugador en la pantalla de titulo.
/// </summary>
public class DummyAvatarController : MonoBehaviour
{
    
    private AvatarState state;
    private Rigidbody2D rbody2D;

    private Quaternion DOWN_ROTATION = Quaternion.Euler(0, 0, -35);         //Angulo de rotacion hacia abajo
    private Quaternion FORWARD_ROTATION = Quaternion.Euler(0, 0, 35);       //Angulo de rotacion hacia arriba

    private int tapCount;
    private bool bounceForward;

    [SerializeField]
    float speed;
    [SerializeField]
    float tiltSmooth;
    [SerializeField]
    float upSpeed;

    private void Awake()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        state = AvatarState.MOVING;

        tapCount = 0;
        bounceForward = false;
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case AvatarState.IDLE_FLYING:
                AvatarBounce();
                break;

            case AvatarState.MOVING:
                MoveAvatar();
                break;
        }

        AvatarFlying();
    }

    /// <summary>
    /// Mueve el avatar en el eje x.
    /// </summary>
    private void MoveAvatar()
    {
        transform.position += Vector3.right * speed;

        if (transform.position.x >= -7)
        {
            state = AvatarState.IDLE_FLYING;
        }
    }

    /// <summary>
    /// Mueve el avatar sin desplazamiento en eje x
    /// </summary>
    private void IdleAvatar()
    {
        AvatarFlying();
    }

    /// <summary>
    /// Controla el vuelo del avatar.
    /// </summary>
    private void AvatarFlying()
    {
        if (transform.position.y <= Constants.AVATAR_AUTO_FLAP_LOWER_LIMIT)
        {
            MoveAvatarUp();
        }
        else if(transform.position.y < Constants.AVATAR_AUTO_FLAP_UPPER_LIMIT && 
                Random.Range(0, 100) < Constants.AVATAR_AUTO_FLAP_PERCENTAGE)
        {
            MoveAvatarUp();
        }

        UpdateAvatarTilt();
    }

    /// <summary>
    /// Actualiza el angulo de inclinacion del avatar
    /// </summary>
    private void UpdateAvatarTilt()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,  DOWN_ROTATION, tiltSmooth * Time.deltaTime);
    }

    /// <summary>
    /// Mueve ligeramente el avatar de izquierda a derecha
    /// </summary>
    private void AvatarBounce()
    {
        if (bounceForward)
        {
            transform.position += Vector3.right * speed * Constants.DUMMY_BOUNCE_SPEED;

            if (transform.position.x >= Constants.DUMMY_BOUNCE_RIGHT_LIMIT)
            {
                bounceForward = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Constants.DUMMY_BOUNCE_SPEED;

            if (transform.position.x <= Constants.DUMMY_BOUNCE_LEFT_LIMIT)
            {
                bounceForward = true;
            }
        }
    }

    /// <summary>
    /// Mueve el avatar hacia arriba.
    /// </summary>
    public void MoveAvatarUp()
    {
        transform.rotation = FORWARD_ROTATION;
        rbody2D.velocity = Vector3.up * upSpeed;
        tapCount = 0;
    }

    /// <summary>
    /// Mensaje disparado al presionar sobre el avatar.
    /// </summary>
    private void OnMouseDown()
    {
        tapCount++;

        if (tapCount < 3)
        {
            rbody2D.velocity = Vector3.up * upSpeed * Constants.DUMMY_TAP_IMPULSE;
            GameManager.Instance.AudioM.Raise_PlayerFlap();
        }
        else
        {
            rbody2D.velocity = Vector3.up * upSpeed * Constants.DUMMY_TAP_GREAT_IMPULSE;
            GameManager.Instance.AudioM.Raise_PlayerGravitySwap(); //TODO: cambiar sonido 
        }

        transform.rotation = FORWARD_ROTATION;
    }
}