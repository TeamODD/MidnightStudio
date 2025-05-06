using UnityEngine;

public class Character_ink_production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform inkRect;

    public void MoveRight_Ink()
    {
        ui_production.Move("Smooth", 0.2f, "x", inkRect.anchoredPosition.x, -2.76f);
    }
    public void MoveLeft_Ink()
    {
        ui_production.Move("Smooth", 0.2f, "x", inkRect.anchoredPosition.x, -7.12f);
    }
}
