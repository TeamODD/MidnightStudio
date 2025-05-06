using UnityEngine;

public class Panel_Client_Production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform PanelClientRect;

    public void Hide_Client_Panel()
    {
        ui_production.Alpha("Smooth", 0.25f, 1, 0);
    }

    public void Show_Client_Panel()
    {
        ui_production.Alpha("Smooth", 0.25f, 0, 1);
    }

    public void Hide_Client_Panel_Instant()
    {
        ui_production.Alpha("Smooth", 0.6f, 1, 0);
    }
    public void Move_Client_Panel()
    {
        ui_production.Move("Smooth", 0.5f, "x", 359f, 259f);
    }
    public void Move_Client_Panel_Right()
    {
        ui_production.Move("Smooth", 0.2f, "x", PanelClientRect.anchoredPosition.x, 560f);
    }

    public void Move_Client_Panel_Left()
    {
        ui_production.Move("Smooth", 0.2f, "x", PanelClientRect.anchoredPosition.x, -10f);
    }
    public void Destroy_Client_Panel()
    {
        ui_production.Alpha("Instant", 1f, 1, 0);
    }
}
