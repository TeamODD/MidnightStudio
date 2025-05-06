using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextBackground : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UI_Production background;
    public RectTransform size;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // background.SetActive(true);
        background.Area("Smooth", 0.2f, size.sizeDelta ,new Vector2(800f,100f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // background.SetActive(false);
        background.Area("Smooth", 0.2f, size.sizeDelta ,new Vector2(600f,100f));
    }

    public void hide()
    {
        background.Area("Smooth", 0.2f, size.sizeDelta ,new Vector2(0f,100f));
    }
    public void show()
    {
        background.Area("Smooth", 0.2f, size.sizeDelta ,new Vector2(600f,100f));
    }
}

