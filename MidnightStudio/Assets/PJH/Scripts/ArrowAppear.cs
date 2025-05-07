using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowAppear : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UI_Production Arrow;
    public CanvasGroup ownAlpha;
    public QuestionManager questionManager;

    public void Start()
    {
        Arrow.Alpha("Instant", 0f, 0f, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(questionManager.canClick){
            Arrow.Alpha("Lerp", 0.1f, 0f, 1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(questionManager.canClick){
            Arrow.Alpha("Lerp", 0.1f, 1f, 0f);
        }
    }
    public void alphaOnOff()
    {
        if(ownAlpha.alpha != 0)
        {
            Arrow.Alpha("Lerp", 0.1f, ownAlpha.alpha, 0f);
        }
        
    } 
    
}

