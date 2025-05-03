using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void Start()
    {
        GetComponent<TMP_Text>().color = Color.black;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TMP_Text>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TMP_Text>().color = Color.black;
    }
}
