using UnityEngine;
using GravityBat_Constants;

/// <summary>
/// Script asociado al canvas que controla el flujo entre
/// las paginas de los diferentes mundos
/// </summary>
public class WorldPagesCanvas : MonoBehaviour
{
    #region Attributes
    /// <summary>
    /// Indica si se esta llevando acabo la transicion entre distintos worldPages.
    /// </summary>
    private bool pageTransition;    
    private WorldPageAnimator anim; 
    private LevelSelectionManager LSM;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Array que contiene todas las WorldPages
    /// </summary>
    [SerializeField]
    GameObject[] WorldPages;

    /// <summary>
    /// Pagina de mundo bloqueado
    /// </summary>
    [SerializeField]
    GameObject BlockedWorldPage;
    #endregion    

    /// <summary>
    /// Ejecutado al instanciar el MonoBehaviour.
    /// Recoge una referencia a LSM.
    /// </summary>
    private void Awake()
    {
        LSM = LevelSelectionManager.Instance;
    }

    /// <summary>
    /// Suscribe los eventos e inicializa la WorldPage.
    /// </summary>
    private void OnEnable()
    {
        LSM.OnArrowButtonClicked += HandleArrowButtonClicked;
        InitWorldPage();
    }

    /// <summary>
    /// Desuscribe los eventos
    /// </summary>
    private void OnDisable()
    {
        LSM.OnArrowButtonClicked -= HandleArrowButtonClicked;
    }

    /// <summary>
    /// Ejecutado en cada frame despues de 'Update'.
    /// Lleva a cabo la transicion entre worldPages cuando sea necesario.
    /// </summary>
    private void LateUpdate()
    {
        if (pageTransition)
        {
            pageTransition = anim.AnimateWorldPageChange();
        }
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo las acciones correspondientes al evento de pulsar el boton de la fleha.
    /// Realiza la animacion de cambio de pagina en la dirección establecida.
    /// </summary>
    /// <param name="dir">Direccion de la flecha pulsada. Marca la direccion en la que se realizara la animacion</param>
    private void HandleArrowButtonClicked(Direction dir)
    {
        if (!pageTransition)
        {
            ChangeWorldPage(dir);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Inicializa la WorldPage
    /// </summary>
    private void InitWorldPage()
    {
        if (LSM.WorldIndex < GameManager.Instance.Data.WorldsUnlockedCount())
        {
            WorldPages[LSM.WorldIndex].SetActive(true);
        }
        else
        {
            BlockedWorldPage.SetActive(true);
        }
    }
    #endregion

    #region WorldPage Animation
    /// <summary>
    /// Habilita una worldPage.
    /// </summary>
    /// <param name="index">indice de la worldpage a habilitar</param>
    private void EnableWorldPage(int index)
    {
        for (int i = 0; i < WorldPages.Length; i++)
        {
            if (i == index)
                WorldPages[index].SetActive(true);
            else
                WorldPages[index].SetActive(false);
        }
    }

    /// <summary>
    /// Lleva a cabo el cambio de worldPage
    /// </summary>
    /// <param name="dir">Direccion en la que se lleva a cabo el cambio</param>
    private void ChangeWorldPage(Direction dir)
    {
        int worldIndex = LSM.WorldIndex;
        int maxIndex = GameManager.Instance.Data.WorldsUnlockedCount();     //Obtiene el numero de mundos desbloqueados
        GameObject[] UnlockedWorldpages = new GameObject[maxIndex + 1];     //Crea un array con los mundos desbloqueados

        GameObject inPage;      //pagina saliente
        GameObject outPage;     //pagina entrante

        //Inicializa el array
        for (int i = 0; i < UnlockedWorldpages.Length - 1; i++)
        {
            UnlockedWorldpages[i] = WorldPages[i];
        }

        UnlockedWorldpages[UnlockedWorldpages.Length - 1] = BlockedWorldPage;  //Ultimo elemento del array

        outPage = UnlockedWorldpages[worldIndex];   //establece la page seleccionada como salida

        switch (dir)
        {
            //Si la transicion es hacia la derecha
            case Direction.RIGHT:
                worldIndex = worldIndex < maxIndex ? (worldIndex + 1) : 0;
                inPage = UnlockedWorldpages[worldIndex];
                break;

            //Si la transicion es hacia la izquierda
            case Direction.LEFT:
                worldIndex = worldIndex > 0 ? (worldIndex - 1) : maxIndex;
                inPage = UnlockedWorldpages[worldIndex];
                break;

            default:
                worldIndex = maxIndex;
                inPage = UnlockedWorldpages[maxIndex];
                break;
        }

        //Inicializa el objeto que lleva a cabo la transicion y la activa
        anim = new WorldPageAnimator(outPage, inPage, dir);
        pageTransition = true;
        LSM.WorldIndex = worldIndex;
    }

    /// <summary>
    /// Clase utilizada para crear objetos que llevan a cabo la transicion entre World Pages
    /// </summary>
    class WorldPageAnimator
    {
        GameObject outPage;         //Pagina que sale
        GameObject inPage;          //Pagina que entra
        RectTransform outTransform; //Transform de la pagina que sale
        RectTransform inTransform;  //Transform de la pagina qu entra

        int SIGN;

        float inOffset;
        float outOffset;

        const float RANGE = 20f;
        const float ANIM_STEP = 1f;

        /// <summary>
        /// Constructor con parametros.
        /// </summary>
        /// <param name="outPage">Pagina que sale</param>
        /// <param name="inPage">Pagina que entra</param>
        /// <param name="dir">Direccion en la que se produce el cambio</param>
        public WorldPageAnimator(GameObject outPage, GameObject inPage, Direction dir)
        {
            this.outPage = outPage;
            this.inPage = inPage;
            outTransform = outPage.GetComponent<RectTransform>();
            inTransform = inPage.GetComponent<RectTransform>();

            SIGN = dir == Direction.RIGHT ? 1 : -1; //signo en funcion de la direccion

            Init();
        }

        /// <summary>
        /// Metodo que lleva a cabo la animacion
        /// </summary>
        /// <returns>bool que indica si se esta llevando a cabo la animacion o si ha finalizado</returns>
        public bool AnimateWorldPageChange()
        {
            if (inOffset != 0f)
            {
                inOffset -= ANIM_STEP * SIGN;
                outOffset -= ANIM_STEP * SIGN;

                inTransform.transform.position = Vector3.right * inOffset;
                outTransform.transform.position = Vector3.right * outOffset;

                return true;
            }
            else
            {
                outPage.SetActive(false);

                return false;
            }
        }

        /// <summary>
        /// Inicializa el cambio de pagina.
        /// </summary>
        private void Init()
        {
            inPage.SetActive(true);
            inTransform.transform.position = Vector3.right * RANGE * SIGN;
            inOffset = RANGE * SIGN;
            outOffset = 0;
        }
    }
    #endregion
}
