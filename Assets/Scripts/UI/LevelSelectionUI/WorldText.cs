using UnityEngine;
using TMPro;

/// <summary>
/// Script relacionado con el texto que muestra el nombre del mundo seleccionado
/// </summary>
public class WorldText : MonoBehaviour
{
    private void Start()
    {
        TextMeshProUGUI worldText = GetComponent<TextMeshProUGUI>();
        worldText.text = LevelSelectionManager.Instance.GetWorldString();
    }
}