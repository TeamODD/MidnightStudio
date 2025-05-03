using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowAppear : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Arrow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Arrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Arrow.SetActive(false);
    }
}

