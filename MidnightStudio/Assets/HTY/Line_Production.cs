using UnityEngine;

public class Line_Production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform lineRect;

    public void Line_align()
    {
        ui_production.Move("Lerp", 0.1f, "x", 0f, 0f);
    }

    public void Line_Ink() {
        ui_production.Move("Smooth", 0.2f, "x", lineRect.anchoredPosition.x, 300f);
    }

    public void Line_Client()
    {
        ui_production.Move("Smooth", 0.2f, "x", lineRect.anchoredPosition.x, -300f);
    }
}
