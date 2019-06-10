using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    private TextMeshProUGUI tMesh;

    private void Awake()
    {
        tMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        tMesh.text = LevelManager.Instance.GetLevelString();
    }
}
