using UnityEngine;

public class WelcomeMessage : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "UI_2";
        canvas.sortingOrder = 999;
    }

    public void MessageReadOk()
    {
        GameManager.Instance.Data.IsWelcomeMessageDone = true;
        GameManager.Instance.Raise_RequestDataSave();

        Destroy(gameObject);
    }
}
