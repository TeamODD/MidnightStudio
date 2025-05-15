using UnityEngine;

public class Panel_Ink_Production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform PanelInkRect;
    public UI_Production Right_Bg;
    public UI_Production Left_Bg;

    public void Hide_Ink_Panel()
    {
        ui_production.Alpha("Smooth", 0.25f, 1, 0);
    }

    public void Show_Ink_Panel()
    {
        ui_production.Alpha("Smooth", 0.25f, 0, 1);
    }

    public void Move_Ink_Panel()
    {
        ui_production.Move("Smooth", 0.5f, "x", -180f, -80f);
    }

    public void Move_Ink_Panel_Right()
    {
        ui_production.Move("Smooth", 0.2f, "x", PanelInkRect.anchoredPosition.x, 220f);
    }

    public void Move_Ink_Panel_Left()
    {
        ui_production.Move("Smooth", 0.2f, "x", PanelInkRect.anchoredPosition.x, -465f);
    }

    public void Destroy_Ink_Panel() {
        ui_production.Alpha("Instant", 1f, 1, 0);
    }
}

