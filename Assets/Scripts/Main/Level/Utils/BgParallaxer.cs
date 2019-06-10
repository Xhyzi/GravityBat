using UnityEngine;

/// <summary>
/// Desplaza un background a una velocidad diferente a la de la camara, generando
/// un efecto visual de profundidad.
/// El fondo es repetido de forma infinita.
/// </summary>
public class BgParallaxer : MonoBehaviour
{
    #region Attributes
    private Transform cameraTransform;  //posicion de la camara
    private Transform[] tiles;          //Posicion de cada tile
    private float viewZone = 10;    
    private int leftIndex;              //Indice del fondo izquierdo      
    private int rightIndex;             //Indice del fondo derecho
    private float lastCameraX;          //Ultima posicion en X de la camara
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Referencia al fondo de la izquierda.
    /// </summary>
    [SerializeField]
    GameObject BgLeft;

    /// <summary>
    /// Referencia al fondo del centro.
    /// </summary>
    [SerializeField]
    GameObject BgCenter;

    /// <summary>
    /// Referencia al fondo de la derecha.
    /// </summary>
    [SerializeField]
    GameObject BgRight;

    /// <summary>
    /// Velocidad de desplazamiento respecto a la camara
    /// </summary>
    [SerializeField]
    float parallaxSpeed;

    /// <summary>
    /// Tamanio del fondo
    /// </summary>
    [SerializeField]
    float bgSize;     
    #endregion

    /// <summary>
    /// Llamado antes del primer frame Update.
    /// Inicializa el parallax
    /// </summary>
    private void Start()
    {
        InitParallax();
    }

    /// <summary>
    /// Llamado una ver por frame Update.
    /// Actualiza la posicion de los fondos.
    /// </summary>
    //private void FixedUpdate()
    private void Update()
    {
        UpdateParallax();
    }

    #region Methods
    /// <summary>
    /// Inicializa el Parallax
    /// </summary>
    private void InitParallax()
    {
        cameraTransform = Camera.main.transform;    //Recoge una referencia del transform de la camara
        lastCameraX = cameraTransform.position.x;   //Obtiene la ultima posicion de la camara

        tiles = new Transform[3] { BgLeft.transform, BgCenter.transform, BgRight.transform };   //Genera un array con los 3 bg utilizados en el parallax

        leftIndex = 0;  //Inicializa los valores de los indices izquierdo y derecho
        rightIndex = tiles.Length - 1;  //2
    }

    /// <summary>
    /// Actualiza la posicion del fondo
    /// </summary>
    private void UpdateParallax()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;    //Incremento de la posicion X de la camara desde el ultimo frame
        transform.position += Vector3.right * (deltaX * parallaxSpeed); //aumenta la posicion del bg en la proporcion indicada por parallaxSpeed

        lastCameraX = cameraTransform.position.x; // Actualiza la posicion de la camara en X

        //Comprueba si se ha llegado al limite de alguno de los bg, para hacer el "Scroll" (Intercambio de los bg para crear el efecto infinito)
        if (cameraTransform.position.x < (tiles[leftIndex].transform.position.x + viewZone))
        {
            ScrollLeft();
        }
        else if (cameraTransform.position.x > (tiles[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }

    /// <summary>
    /// Mueve el fondo cuando te desplazas hacia la derecha, reordena el array de backgrounds y actualiza los indices.
    /// </summary>
    private void ScrollLeft()
    {
        int lastRight = rightIndex;

        tiles[rightIndex].position = Vector3.right * (tiles[leftIndex].position.x - bgSize);
        leftIndex = rightIndex;
        rightIndex--;

        if(rightIndex < 0)
        {
            rightIndex = tiles.Length - 1;
        }
    }

    /// <summary>
    /// Mueve el fondo cuando te desplazas hacia la derecha, reordena el array de backgrounds y actualiza los indices.
    /// </summary>
    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        //tiles[leftIndex].position = Vector3.right * (tiles[rightIndex].position.x + bgSize);
        tiles[leftIndex].position = new Vector3(1 * tiles[rightIndex].position.x + bgSize, tiles[rightIndex].position.y);
        rightIndex = leftIndex;
        leftIndex++;

        if(leftIndex == tiles.Length)
        {
            leftIndex = 0;
        }
    }
    #endregion
}
