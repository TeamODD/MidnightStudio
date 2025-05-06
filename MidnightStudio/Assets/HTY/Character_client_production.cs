using UnityEngine;

public class Character_client_production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform clientRect;

    public void Character_align()
    {
        ui_production.Move("Lerp", 0.1f, "x", 0f, 0f);
    }

    public void MoveRight_Client()
    {
        ui_production.Move("Smooth", 0.2f, "x", clientRect.anchoredPosition.x, 7.34f);
    }

    public void MoveLeft_Client()
    {
        ui_production.Move("Smooth", 0.2f, "x", clientRect.anchoredPosition.x, 4.3f);
    }
}
