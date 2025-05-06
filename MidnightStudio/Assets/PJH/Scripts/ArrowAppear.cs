using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowAppear : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UI_Production Arrow;

    public void Start()
    {
        Arrow.Alpha("Instant", 0f, 0f, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Arrow.SetActive(true);
        Arrow.Alpha("Lerp", 0.1f, 0f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Arrow.SetActive(false);
        Arrow.Alpha("Lerp", 0.1f, 1f, 0f);
    }
}

