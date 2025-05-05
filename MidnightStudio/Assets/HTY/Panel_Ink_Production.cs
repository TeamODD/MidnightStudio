using UnityEngine;

public class Panel_Ink_Production : MonoBehaviour
{
    public UI_Production ui_production;

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

    public void Destroy_Ink_Panel() {
        ui_production.Alpha("Instant", 1f, 1, 0);
    }
}

