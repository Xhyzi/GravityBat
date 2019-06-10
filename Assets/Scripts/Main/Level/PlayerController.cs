using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado al GameObject del avatar.
/// Se encarga de controlar el personaje.
/// Se suscribe a eventos de LevelManager.
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;                        //Referencia al LevelManager
    private Animator animator;                      //Referencia al animator del player
    private SpriteRenderer spriteRenderer;          //Referencia al spriteRenderer del player
    private Rigidbody2D rBody2D;                    //Referencia al rigidbody del player
    private CapsuleCollider2D capsuleCollider2D;    //Referencia al collider del player
    private bool btnTapPresed;                      //Indica que el boton ha sido presionado
    private float deltaTime;
    private float lastMovedTime;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Velocidad a la que se desplaza el player.
    /// </summary>
    [SerializeField]
    float speed = 0.1f;         //Velocidad a la que se desplaza el player. Nota: [0.1 - 0.2]

    /// <summary>
    /// Impulso que se da al jugador al pulsar el boton de volar.
    /// </summary>
    [SerializeField]
    float tapSpeed = 5;      //Velocidad aportada al pulsar el boton "Tap". Nota: [5]

    /// <summary>
    /// Suavidaz con la que el se inclina el sprite del avatar.
    /// </summary>
    [SerializeField]
    float tiltSmooth = 1;    //Velocidad a la que se produce la rotacion del sprite. Nota: [1]
    #endregion

    #region Constants 
    /// <summary>
    /// Contiene el offset del collider cuando la gravedad se dirige hacia arriba.
    /// </summary>
    private Vector2 COL_OFFSET_UP = new Vector2(Constants.PLAYER_COLLIDER_OFFSET_X, Constants.PLAYER_COLLIDER_OFFSET_Y);       

    /// <summary>
    /// Contiene el offset del collider cuando la gravedad se dirige hacia abajo.
    /// </summary>
    private Vector2 COL_OFFSET_DOWN = new Vector2(Constants.PLAYER_COLLIDER_OFFSET_X, - Constants.PLAYER_COLLIDER_OFFSET_Y);  

    /// <summary>
    /// Angulo de rotacion del avatar hacia abajo.
    /// </summary>
    private Quaternion DOWN_ROTATION = Quaternion.Euler(0, 0, - Constants.PLAYER_FLYING_TILT_ANGLE);        

    /// <summary>
    /// Angulo de rotacion del avatar hacia arriba.
    /// </summary>
    private Quaternion FORWARD_ROTATION = Quaternion.Euler(0, 0, Constants.PLAYER_FLYING_TILT_ANGLE);       

    /// <summary>
    /// Angulo de rotacion del avatar hacia abajo en la animacion de muerte.
    /// </summary>
    private Quaternion DOWN_ROTATION_DEATH = Quaternion.Euler(0, 0, - Constants.PLAYER_DEAD_TILT_ANGLE);   

    /// <summary>
    /// Angulo de rotacion del avatar hacia arriba en la animacion de muerte.
    /// </summary>
    private Quaternion FORWARD_ROTATION_DEATH = Quaternion.Euler(0, 0, Constants.PLAYER_DEAD_TILT_ANGLE);

    /// <summary>
    /// Angulo de rotacion por defecto (0)
    /// </summary>
    private Quaternion RESET_ROTATION = Quaternion.Euler(0, 0, 0);
    #endregion

    /// <summary>
    /// Llamado al instanciar el MonoBehaviour.
    /// Recoge referencias a LevelManager, Animator, SpriteRenderer, RigidBody2D y CapsuleCollider.
    /// </summary>
    private void Awake()
    {
        //Se recoge la instancia del singleton
        LM = LevelManager.Instance;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rBody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// Suscribe los eventos de LevelManager.
    /// </summary>
    private void OnEnable()
    {
        LM.OnDeath += HandleOnDeath;
        LM.OnGameOver += HandleOnGameOver;
        LM.OnTapButton += HandleOnTapButton;
        LM.OnGravityButton += HandleOnGravityButton;
        LM.OnLevelFragmentTrigger += HandleOnLevelFragmentTrigger;
        LM.OnTutorialEnded += HandleTutorialCompleted;

        btnTapPresed = false;
        rBody2D.gravityScale = Constants.DEFAULT_GRAVITY_SCALE;
        lastMovedTime = 0;
    }

    /// <summary>
    /// Desuscribe los eventos de LevelManager.
    /// </summary>
    private void OnDisable()
    {
        LM.OnDeath -= HandleOnDeath;
        LM.OnGameOver -= HandleOnGameOver;
        LM.OnTapButton -= HandleOnTapButton;
        LM.OnGravityButton -= HandleOnGravityButton;
        LM.OnLevelFragmentTrigger -= HandleOnLevelFragmentTrigger;
        LM.OnTutorialEnded -= HandleTutorialCompleted;
    }

    /// <summary>
    /// Es llamado una vez por frame.
    /// Ejecuta el frameUpdate correspondiente al estado del nivel.
    /// </summary>
    private void FixedUpdate()
    {
        switch (LM.State)
        {
            case LevelState.INIT:
                InitFrameUpdate();
                break;

            case LevelState.RUNNING:
                RunningFrameUpdate();
                ParseSecond();
                break;

            case LevelState.DEAD:
                DeathFrameUpdate();
                break;

            case LevelState.COMPLETED:
                InitFrameUpdate();
                break;
        }
    }

    #region Event Handlers
    private void HandleOnGameOver()
    {
        Invoke("FreezePlayer", Constants.PLAYER_FREEZE_AFTER_DEAD_WAIT_TIME);
    }

    private void HandleOnDeath()
    {
        DoDeathAnimation();
    }

    private void HandleOnTapButton()
    {
        btnTapPresed = true;    //Recomendable realizar la acción en el LateUpdate
    }

    private void HandleOnGravityButton(bool gravityEnabled)
    {
        PlayerGravitySwap(gravityEnabled);
    }

    private void HandleOnLevelFragmentTrigger()
    {
        if (LM.IsInfinite)
        {
            LM.Data.SpeedModifier += Constants.SPEED_MODIFIER_INCREMENT;
            Time.timeScale += 0.004f;
        }
    }

    private void HandleTutorialCompleted(TutorialTags tutorial)
    {
        switch (tutorial)
        {
            case TutorialTags.POINTS:
            case TutorialTags.MULTIPLIER:
            case TutorialTags.SWAP_CONSECUENCES:
                if (transform.position.y < Constants.AVATAR_AUTO_FLAP_UPPER_LIMIT && LM.GravityEnabled)
                {
                    transform.rotation = FORWARD_ROTATION;
                    rBody2D.velocity = new Vector2(rBody2D.velocity.x, (LM.GravityEnabled ? 1 : -1) * tapSpeed);
                }
                else if (transform.position.y > Constants.AVATAR_AUTO_FLAP_LOWER_LIMIT && !LM.GravityEnabled)
                {
                    transform.rotation = DOWN_ROTATION;
                    rBody2D.velocity = new Vector2(rBody2D.velocity.x, (LM.GravityEnabled ? 1 : -1) * tapSpeed);
                }
                break;
        }
    }
    #endregion

    //TODO: Propositos unicamente de debug.
    private void OnTriggerEnter2D(Collider2D col)
    {
        LM.Raise_PlayerCollision("Player collides with -> " + col.gameObject.tag);
    }

    /// <summary>
    /// Notifica al LevelManager la colision y muerte del jugador.
    /// </summary>
    /// <param name="collider">collider del objeto con el que se ha colisionado</param>
    void OnCollisionEnter2D(Collision2D collider)
    {
        LM.Raise_PlayerDeath();
    }

    #region Methods
    /// <summary>
    /// Lleva a cabo el frameUpdate antes de iniciar el nivel (INIT).
    /// El avatar volara de forma aleatoria siguiendo unos limites
    /// </summary>
    private void InitFrameUpdate()
    {
        MovePlayerX();
        AvatarAutoFlap();
        UpdatePlayerAngleByFrame(Constants.AVATAR_ALIVE_TILT_SPEED);
    }

    /// <summary>
    /// Lleva a cabo el frameUpdate mientras se esta jugando el nivel
    /// </summary>
    void RunningFrameUpdate()
    {
        MovePlayerX();
        PlayerAvatarFlap();
        UpdatePlayerAngleByFrame(Constants.AVATAR_ALIVE_TILT_SPEED);
    }

    /// <summary>
    /// Lleva a cabo el frameUpdate cuando el jugador muere
    /// </summary>
    void DeathFrameUpdate()
    {
        UpdatePlayerAngleByFrame(Constants.AVATAR_DEAD_TILT_SPEED);
    }

    /// <summary>
    /// Cuenta el paso del tiempo, notificando el paso de cada segundo al LevelManager
    /// </summary>
    private void ParseSecond()
    {
        deltaTime += Time.deltaTime;    //Suma el incremento del tiempo

        if (deltaTime >= 1f)    //Si el incremento del tiempo es mayor a 1s
        {
            deltaTime -= 1f;    //Resta 1s al incremento y dispara el evento

            LM.Raise_SecondParsed();
        }
    }

    /// <summary>
    /// Lleva a cabo la animacion de muerte del jugador
    /// </summary>
    private void DoDeathAnimation()
    {
        capsuleCollider2D.enabled = false;  //Desactiva el collider
        rBody2D.freezeRotation = false;     //Activa la rotacion
        rBody2D.gravityScale = Constants.DEATH_ANIMATION_GRAVITY_SCALE;           //Modifica la escala de la gravedad
        rBody2D.velocity = (LM.GravityEnabled ? Vector3.up : Vector3.down) * Constants.DEATH_ANIMATION_IMPULSE;    //Impulsa el jugador hacia arriba
        transform.rotation = LM.GravityEnabled ? FORWARD_ROTATION_DEATH : DOWN_ROTATION_DEATH;
        spriteRenderer.sortingLayerName = "UI_1";   //Cambia la capa del avatar

        animator.SetTrigger("death");   //Activa la animacion de muerte

        Invoke("FinishDeathAnimation", Constants.DEATH_ANIMATION_LENGTH);
    }

    /// <summary>
    /// Lleva a cambio el cambio de la gravedad del jugador
    /// </summary>
    private void PlayerGravitySwap(bool gravityEnabled)
    {
        if (gravityEnabled)
        {
            spriteRenderer.flipY = true;
            capsuleCollider2D.offset = COL_OFFSET_DOWN;
        }
        else
        {
            spriteRenderer.flipY = false;
            capsuleCollider2D.offset = COL_OFFSET_UP;
        }

        rBody2D.velocity = Vector3.zero;
        transform.rotation = RESET_ROTATION;
        animator.SetTrigger("gravitySwap");
    }

    /// <summary>
    /// Desplaza al jugador en el eje x en proporcion a la velocidad.
    /// </summary>
    private void MovePlayerX()
    {
        rBody2D.velocity = new Vector2(speed, rBody2D.velocity.y);
    }

    /// <summary>
    /// Actualiza el angulo de inclinacion del jugador frame a frame
    /// </summary>
    private void UpdatePlayerAngleByFrame(float tiltSpeed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, LM.GravityEnabled ? DOWN_ROTATION : FORWARD_ROTATION, tiltSmooth * tiltSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Hace volar al avatar de forma aleatoria dentro de unos limites
    /// </summary>
    private void AvatarAutoFlap()
    {
        if (transform.position.y < Constants.AVATAR_AUTO_FLAP_LOWER_LIMIT) //Debe hacer flap
        {
            transform.rotation = FORWARD_ROTATION;
            rBody2D.velocity = new Vector2(rBody2D.velocity.x, (LM.GravityEnabled ? 1 : -1) * tapSpeed);

        }
        else if (transform.position.y < Constants.AVATAR_AUTO_FLAP_UPPER_LIMIT &&
                    Random.Range(0, 100) < Constants.AVATAR_AUTO_FLAP_PERCENTAGE)  // Puede hacer flap
        {
            transform.rotation = FORWARD_ROTATION;
            rBody2D.velocity = new Vector2(rBody2D.velocity.x, (LM.GravityEnabled ? 1 : -1) * tapSpeed);
        }
    }

    /// <summary>
    /// Hace volvar al avatar por accion del jugador
    /// </summary>
    private void PlayerAvatarFlap()
    {
        if (btnTapPresed)   //Es recomendable hacerlo en el LateUpdate para evitar errores visuales. 
        {
            transform.rotation = LM.GravityEnabled ? FORWARD_ROTATION : DOWN_ROTATION;   //Rota el sprite
            rBody2D.velocity = new Vector2(rBody2D.velocity.x, (LM.GravityEnabled ? 1 : -1) * tapSpeed);
            btnTapPresed = false;   //Desactiva el flag
        }
    }

    /// <summary>
    /// Comunica al LevelManager que puede abrir el menu de GameOver
    /// </summary>
    private void FinishDeathAnimation()
    {
        if (!LM.IsInfinite)
            LM.Raise_GameOver();
        else
            LM.Raise_InfiniteGameOver();
    }

    /// <summary>
    /// Congela al jugador
    /// </summary>
    private void FreezePlayer()
    {
        rBody2D.gravityScale = 0;
        rBody2D.velocity = new Vector3(0, 0, 0);
    }
    #endregion
}
