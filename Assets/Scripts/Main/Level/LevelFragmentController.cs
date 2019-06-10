using UnityEngine;
using System.Collections;
using GravityBat_Constants;

/// <summary>
/// Controla el spawn/despawn de los fragmentos de nivel.
/// Se encuentra asociado a un GameObject con una configuracion diferente
/// para cada mundo del juego.
/// 
/// Los fragmentos de nivel se cargan y borran de forma dinamica a medida que 
/// se recorre el nivel. De esta forma solo hay 3 fragmentos de nivel cargados
/// al mismo tiempo, evitando un uso abusivo de memoria al cargar el nivel completo.
/// 
/// La carga y borrado de los fragmentos se lleva a cabo con corrutinas, evitando
/// tambien bloquear un frame update momentaneamente hasta que se lleve a cabo la accion,
/// lo cual restaria fluidez al juego.
/// </summary>
public class LevelFragmentController : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    
    /// <summary>
    /// Indice del fragmento de nivel actual.
    /// </summary>
    private int levelFragmentIndex;

    /// <summary>
    /// Indica si la corrutina esta activa, evitando una doble activacion
    /// </summary>
    private bool isFragmentCoroutineActive;

    /// <summary>
    /// Indica si es el primer trigger
    /// </summary>
    private bool firstFragmentTriggered;

    /// <summary>
    /// Indica si el nivel es infinito.
    /// </summary>
    private bool isInfiniteLevel;

    /// <summary>
    /// Array con los 3 fragmentos de nivel activos.
    /// </summary>
    private GameObject[] activeLevelFragments = new GameObject[3];

    /// <summary>
    /// Array con los fragmentos de nivel disponibles para el nivel.
    /// </summary>
    private GameObject[] PoolLevelFragments;
    #endregion

    #region Serialized Fields
    /// <summary>
    /// Fragmentos del nivel 1.
    /// </summary>
    [SerializeField]
    GameObject[] Level_1_Fragments;

    /// <summary>
    /// Fragmentos del nivel 2.
    /// </summary>
    [SerializeField]
    GameObject[] Level_2_Fragments;

    /// <summary>
    /// Fragmentos del nivel 3.
    /// </summary>
    [SerializeField]
    GameObject[] Level_3_Fragments;

    /// <summary>
    /// Fragmentos del nivel 4.
    /// </summary>
    [SerializeField]
    GameObject[] Level_4_Fragments;

    /// <summary>
    /// Fragmentos del nivel 5.
    /// </summary>
    [SerializeField]
    GameObject[] Level_5_Fragments;

    /// <summary>
    /// Prefab del grid que sirve como padre para los fragmentos
    /// </summary>
    [SerializeField]
    GameObject GridParentPrefab;

    /// <summary>
    /// Fragmento final que puede repetirse de forma infinita al finalizar el nivel.
    /// </summary>
    //[SerializeField]
    //GameObject LastInfiniteFragment;
    #endregion

    /// <summary>
    /// Llamado al instanciar el MonoBehaviour.
    /// Recoge una referencia a LevelManager.
    /// </summary>
    private void Awake()
    {
        LM = LevelManager.Instance;
    }

    /// <summary>
    /// Inicializa el nivel.
    /// Inicializa los fragmentos del nivel.
    /// Suscribe los eventos de LevelManager.
    /// </summary>
    private void OnEnable()
    {
        LM.InitLevel();
        InitLevelFragments();
        firstFragmentTriggered = false;
        LM.OnLevelFragmentTrigger += HandleLevelFragmentTrigger;
    }

    /// <summary>
    /// Desuscribe los eventos de LevelManager
    /// </summary>
    private void OnDisable()
    {
        LM.OnLevelFragmentTrigger -= HandleLevelFragmentTrigger;
    }

    #region Event Handlers
    /// <summary>
    /// Lleva a cabo la accion correspondiente al LevelFragmentTrigger.
    /// Si no es el primer fragmento da inicio a la corrutina CoroutineUpdateLevelFragments.
    /// </summary>
    private void HandleLevelFragmentTrigger()
    {
        if (firstFragmentTriggered)
        {
            if (!isInfiniteLevel)
                StartCoroutine(CoroutineUpdateLevelFragments());
            else
                StartCoroutine(CoroutineInfiniteRandomLevelFragments());
        }
        else
            firstFragmentTriggered = true;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Inicializa los levelFragments, instanciandolos
    /// </summary>
    private void InitLevelFragments()
    {
        isInfiniteLevel = false;

        switch (LM.LevelIndex)
        {
            case 0:
                PoolLevelFragments = Level_1_Fragments;
                break;

            case 1:
                PoolLevelFragments = Level_2_Fragments;
                break;

            case 2:
                PoolLevelFragments = Level_3_Fragments;
                break;

            case 3:
                isInfiniteLevel = true;
                PoolLevelFragments = Level_4_Fragments;
                break;

            case 4:
                PoolLevelFragments = Level_5_Fragments;
                break;
        }

        if (!isInfiniteLevel)
            for (levelFragmentIndex = 0; levelFragmentIndex < activeLevelFragments.Length; levelFragmentIndex++)
            {
                activeLevelFragments[levelFragmentIndex] = Instantiate(PoolLevelFragments[levelFragmentIndex],
                    new Vector3(Constants.LEVEL_FRAGMENT_OFFSET * levelFragmentIndex, 0, 0),
                    Quaternion.identity);
            }
        else
        {
            for (levelFragmentIndex = 0; levelFragmentIndex < activeLevelFragments.Length; levelFragmentIndex++)
            {
                activeLevelFragments[levelFragmentIndex] = Instantiate(PoolLevelFragments[levelFragmentIndex == 0 ? 0 : GetRandomFragmentIndex()],
                    new Vector3(Constants.LEVEL_FRAGMENT_OFFSET * levelFragmentIndex, 0, 0),
                    Quaternion.identity);
            }
        }

        Instantiate(Resources.Load (
            Constants.PREFAB_LEVEL_RESOURCES_PATH + Constants.START_TRIGGER
            ) as GameObject).transform.SetParent(activeLevelFragments[0].transform);
    }

    /// <summary>
    /// Lleva a cabo la destruccion y la carga de los fragmentos de nivel en varios frame Update.
    /// Esto permite liberar el hilo que ejecuta el frame Update, evitando que la 
    /// pantalla pueda congelarse hasta finalizar la carga de los nuevos fragmentos
    /// y la destruccion de los viejos.
    /// </summary>
    /// <returns>yield</returns>
    private IEnumerator CoroutineUpdateLevelFragments()
    {
        if (!isFragmentCoroutineActive)    //Comprueba que no se haya lanzado ya la corutina
        {
            isFragmentCoroutineActive = true;  //Marca la corutina como activa

            #region Destroy
            Transform[] t;
            GameObject[] prefabChildren;
            GameObject prefab;

            t = activeLevelFragments[0].GetComponentsInChildren<Transform>();   //Obtiene un array con los Transform hijos del gameObject a eliminar
            prefabChildren = new GameObject[t.Length];

            for (int i = 0; i < t.Length; i++)
            {
                prefabChildren[i] = t[i].gameObject;    //Llena el array con los gameObject hijos del gameObject a eliminar
            }

            //Ha de hacerse en bucles separado para evitar borrar un Transform padre de otro, dejando un null en el array
            for (int i = 0; i < prefabChildren.Length; i++)
            {
                Destroy(prefabChildren[i]);             //Destroye cada uno de los gameObjects hijos
                yield return new WaitForFixedUpdate();  //Cesa la ejecucion hasta el proximo frame update
            }

            activeLevelFragments[0] = activeLevelFragments[1];      //Coloca el fragmento 1 en la posicion 0 (vacia)
            activeLevelFragments[1] = activeLevelFragments[2];      
            activeLevelFragments[2] = Instantiate(GridParentPrefab, //Carga en el fragmento 1 un Grid que se utilizara de padre para los componentes a cargar
                        new Vector3(Constants.LEVEL_FRAGMENT_OFFSET * levelFragmentIndex, 0, 0),
                        Quaternion.identity);

            yield return new WaitForFixedUpdate();  //Cesa la ejecucion hasta el proximo frame update
            #endregion

            #region Instantiate

            if (levelFragmentIndex < PoolLevelFragments.Length - 1)
            {
                //Recorre los transform de los hijos DIRECTOS del prefab a instanciar
                foreach (Transform trans in PoolLevelFragments[levelFragmentIndex].transform)
                {
                    //Se instancia uno de los componentes hijos del prefab
                    prefab = Instantiate(trans.gameObject,
                            trans.gameObject.transform.position + activeLevelFragments[2].transform.position,
                            trans.gameObject.transform.rotation);

                    //Se establece el grid como padre
                    prefab.transform.parent = activeLevelFragments[2].transform;

                    if (levelFragmentIndex == PoolLevelFragments.Length - 2)
                        Instantiate(Resources.Load(
                            Constants.PREFAB_LEVEL_RESOURCES_PATH + Constants.END_TRIGGER
                            ) as GameObject).transform.SetParent(activeLevelFragments[2].transform, false);

                    yield return new WaitForFixedUpdate();  //Cesa la ejecucion hasta el proximo frame update
                }
            }
            else
            {
                foreach (Transform trans in PoolLevelFragments[PoolLevelFragments.Length - 1].transform)
                {
                    //Se instancia uno de los componentes hijos del prefab
                    prefab = Instantiate(trans.gameObject,
                            trans.gameObject.transform.position + activeLevelFragments[2].transform.position,
                            trans.gameObject.transform.rotation);

                    //Se establece el grid como padre
                    prefab.transform.parent = activeLevelFragments[2].transform;

                    yield return new WaitForFixedUpdate(); //Cesa la ejecucion hasta el proximo frame update
                }
            }

            levelFragmentIndex++;   //Aumenta el levelFragmentIndex

            isFragmentCoroutineActive = false;  //Marca la corutina como Inactiva
            #endregion
        }
    }

    /// <summary>
    /// Lleva a cabo la destruccion y la carga de los fragmentos de nivel en varios frame Update.
    /// Esto permite liberar el hilo que ejecuta el frame Update, evitando que la 
    /// pantalla pueda congelarse hasta finalizar la carga de los nuevos fragmentos.
    /// La carga de los fragmentos se hace de forma aleatoria de entre los fragmentos presentes en el Pool
    /// y la destruccion de los viejos.
    /// </summary>
    /// <returns>yield</returns>
    private IEnumerator CoroutineInfiniteRandomLevelFragments()
    {
        Debug.LogWarning("Random Level");
        if (!isFragmentCoroutineActive)
        {
            isFragmentCoroutineActive = true;

            Transform[] t;
            GameObject[] prefabChildren;
            GameObject prefab;

            t = activeLevelFragments[0].GetComponentsInChildren<Transform>();   //Obtiene un array con los Transform hijos del gameObject a eliminar
            prefabChildren = new GameObject[t.Length];

            for (int i = 0; i < t.Length; i++)
            {
                prefabChildren[i] = t[i].gameObject;    //Llena el array con los gameObject hijos del gameObject a eliminar
            }

            //Ha de hacerse en bucles separado para evitar borrar un Transform padre de otro, dejando un null en el array
            for (int i = 0; i < prefabChildren.Length; i++)
            {
                Destroy(prefabChildren[i]);     //Destroye cada uno de los gameObjects hijos
                yield return new WaitForFixedUpdate(); //Cesa la ejecucion hasta el proximo frame update
            }

            activeLevelFragments[0] = activeLevelFragments[1];      //Coloca el fragmento 1 en la posicion 0 (vacia)
            activeLevelFragments[1] = activeLevelFragments[2];
            activeLevelFragments[2] = Instantiate(GridParentPrefab, //Carga en el fragmento 1 un Grid que se utilizara de padre para los componentes a cargar
                        new Vector3(Constants.LEVEL_FRAGMENT_OFFSET * levelFragmentIndex, 0, 0),
                        Quaternion.identity);
            Debug.Log("Offset -> " + Constants.LEVEL_FRAGMENT_OFFSET * levelFragmentIndex);

            yield return new WaitForFixedUpdate();  //Cesa la ejecucion hasta el proximo frame update


            foreach (Transform trans in PoolLevelFragments[GetRandomFragmentIndex()].transform)
            {
                prefab = Instantiate(trans.gameObject,
                    trans.gameObject.transform.position + activeLevelFragments[2].transform.position,
                    trans.gameObject.transform.rotation);

                prefab.transform.parent = activeLevelFragments[2].transform;

                yield return new WaitForFixedUpdate();
            }

            levelFragmentIndex++;   //Aumenta el levelFragmentIndex


            isFragmentCoroutineActive = false;
        }
    }

    /// <summary>
    /// Obtiene el indice de un fragmento aleatorio dentro del Pool 
    /// </summary>
    /// <returns>index de un fragmento aleatorio</returns>
    private int GetRandomFragmentIndex()
    {
        return Random.Range(0, PoolLevelFragments.Length);
    }
    #endregion
}
