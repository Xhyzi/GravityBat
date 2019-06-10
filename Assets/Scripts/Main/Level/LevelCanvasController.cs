using System.Collections;
using System.Collections.Generic;
using GravityBat_Constants;
using UnityEngine;
using TMPro;


public class LevelCanvasController : MonoBehaviour
{
    #region Attributes
    private LevelManager LM;
    private Canvas canvas;
    private Queue<GameObject> scorePool;
    #endregion

    private void Awake()
    {
        LM = LevelManager.Instance;
        canvas = GetComponent<Canvas>();
        scorePool = new Queue<GameObject>();
    }

    /// <summary>
    /// Se suscribe a los eventos de LevelManager
    /// </summary>
    private void OnEnable()
    {
        LM.OnItemAnimationFinished += HandleItemAnimationFinished;
    }

    /// <summary>
    /// Se desuscribe a los eventos de LevelManager
    /// </summary>
    private void OnDisable()
    {
        LM.OnItemAnimationFinished -= HandleItemAnimationFinished;
    }

    #region Event Handlers
    private void HandleItemAnimationFinished(int points, Vector3 pos)
    {
        CreateScoreText(points, pos);
        StartCoroutine(ScoreFlowAnimation());
    }
    #endregion

    #region Methods
    private void CreateScoreText(int points, Vector3 pos)
    {
        GameObject go = Instantiate(Resources.Load("prefabs/empty_go") as GameObject, transform);
        go.name = "ScoreText";
        TextMeshProUGUI tMesh = go.AddComponent<TextMeshProUGUI>();

        tMesh.font = Resources.Load("fonts/tmpro/press_start/extended_ascii/ps2p_basic") as TMP_FontAsset;       //Asigna la fuente


        //tMesh.alignment = TextAlignmentOptions.Mid
        go.transform.position = pos;    //Asigna la posicion
        tMesh.text = points.ToString(); //Asigna el texto
        tMesh.fontSize = 25;
        tMesh.alignment = TextAlignmentOptions.CenterGeoAligned;
        go.GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 0.5f);
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 50);

        scorePool.Enqueue(go);
    }

    /// <summary>
    /// Lleva a cabo la animación del texto con la puntuación en una corrutina.
    /// Al finalizar la animación borra el GameObject.
    /// </summary>
    /// <returns>Devuelve hasta cuando se debe esperar para recuperar la ejecución de la corrutina.
    /// En este caso hasta el nuevo FixedUpdate</returns>
    private IEnumerator ScoreFlowAnimation()
    {
        GameObject go = scorePool.Dequeue();

        for (int i = 0; i < 60; i++)
        {
            go.transform.position = new Vector3(go.transform.position.x, 
                                    go.transform.position.y + Constants.SCORE_TEXT_ANIMATION_SPEED);

            yield return new WaitForFixedUpdate();
        }

        Destroy(go);
    }
    #endregion
}
