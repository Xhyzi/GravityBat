using UnityEngine;
using System.IO;
using System.Collections;
using GravityBat_Constants;
using GravityBat_Localization;

public class ShareNative : MonoBehaviour
{
    private LevelManager LM;
    private TitleScreenManager TSM;

    private bool isProcessing = false;
    private bool isFocused = false;

    private void Awake()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameState.TITLE_SCREEN:
                TSM = TitleScreenManager.Instance;
                break;
            case GameState.LEVEL:
                LM = LevelManager.Instance;
                break;
        }
    }

    /// <summary>
    /// Se suscribe a los eventos
    /// </summary>
    private void OnEnable()
    {   
        if (TSM != null)
            TSM.OnButtonShareClicked += HandleAppShareButtonClick;
        if (LM != null)
            LM.OnButtonShareClicked += HandleScoreShareButtonClick;
        isProcessing = false;
    }

    /// <summary>
    /// Se desuscribe a los eventos
    /// </summary>
    private void OnDisable()
    {
        if (TSM != null)
            TSM.OnButtonShareClicked -= HandleAppShareButtonClick;
        if (LM != null)
            LM.OnButtonShareClicked -= HandleScoreShareButtonClick;
    }


    #region EventHandlers
    private void HandleAppShareButtonClick()
    {
        StartCoroutine(Share("screen_share.png",
            Strings.GetString(TEXT_TAG.TXT_SHARE_APP_MSG) + " " + Constants.APP_LINK,
            Strings.GetString(TEXT_TAG.TXT_SHARE_APP_INFO)));
    }

    private void HandleScoreShareButtonClick()
    {
        StartCoroutine(Share("score_share.png", Strings.GetString(TEXT_TAG.TXT_SHARE_SCORE_MSG), Strings.GetString(TEXT_TAG.TXT_SHARE_SCORE_INFO)));
    }
    #endregion

    #region Methods


    /// <summary>
    /// Corrutina que lleva a cabo el share
    /// </summary>
    /// <param name="imageName">nombre de la imagen que se comparte dentro del sistema de archivos</param>
    /// <param name="msg">mensaje que se comparte</param>
    /// <param name="shareInfo">Información que se muestra al usuario al desplegar las opciones para compartir.</param>
    /// <returns></returns>
    IEnumerator Share(string imageName, string msg, string shareInfo)
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot(imageName, 2);
        string destination = Path.Combine(Application.persistentDataPath, imageName);
        //yield return new WaitForSecondsRealtime(0.33f);

        if (!Application.isEditor && Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), msg);
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareInfo);
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocused);
        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocused = focus;
    }
    #endregion
}
