using UnityEngine;

public class Character_ink_production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform inkRect;

    public void GameStartInkMove()
    {
        ui_production.Move("Smooth", 1f, "y", inkRect.anchoredPosition.y, inkRect.anchoredPosition.y + 0.5f);
    }

    public void GameStartInkMoveBack()
    {
        ui_production.Move("Smooth", 1f, "y", inkRect.anchoredPosition.y, inkRect.anchoredPosition.y - 0.5f);
    }

    public void Alpha_Ink()
    {
        //ui_production.Alpha("Instant", 0.1f,1f,0f);

    }
}
