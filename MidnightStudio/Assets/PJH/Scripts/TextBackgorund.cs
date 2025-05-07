using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextBackground : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UI_Production background;
    public QuestionManager questionManager;
    public RectTransform size;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(questionManager.canClick)
        {
            // background.SetActive(true)
            background.Area("Smooth", 0.2f, size.sizeDelta ,new Vector2(800f,100f));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // background.SetActive(false);
        if(questionManager.canClick){
            background.Area("Smooth", 0.2f, size.sizeDelta ,new Vector2(600f,100f));
        }
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

