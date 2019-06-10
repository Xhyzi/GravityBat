using UnityEngine;
using UnityEngine.EventSystems;

namespace GravityBat_Tutorial
{
    /// <summary>
    /// Script asociado al boton OK de los tutoriales.
    /// </summary>
    public class TutorialBtnOk : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            LevelManager.Instance.Raise_ButtonOKClick(); ;
        }
    }
}

