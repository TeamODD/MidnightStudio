using UnityEngine;

public class Character_ink_panel_production : MonoBehaviour
{
    public UI_Production ui_production;
    public Obj_Production Obj;
    public RectTransform ink_panel_Rect;

    public void MoveRight_Ink()
    {
        ui_production.Move("Smooth", 0.2f, "x", ink_panel_Rect.anchoredPosition.x, -3.36f);
        Obj.Move("Smooth", true, 0.2f, "x", Obj.transform.position.x, 1.5f);
    }
    public void MoveLeft_Ink()
    {
        ui_production.Move("Smooth", 0.2f, "x", ink_panel_Rect.anchoredPosition.x, -7.83f);
        Obj.Move("Smooth", true, 0.2f, "x", Obj.transform.position.x, -1.5f);
    }
    public void GameStartInkMove_panel() {
        ui_production.Move("Smooth", 1f, "x", ink_panel_Rect.anchoredPosition.x, -6.22f);
    }

    //public void GameStartInkRotate_panel()
    //{
    //    ui_production.Rotation("Smooth", 0.3f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 12.5f));
    //}

    //public void GameStartInkRotate_panelBack()
    //{
    //    ui_production.Rotation("Smooth", 0.3f, new Vector3(0f, 0f, 12.5f), new Vector3(0f, 0f, 0f));
    //}

    public void GameStartInkMoveBack() {
        ui_production.Move("Smooth", 1f, "x", ink_panel_Rect.anchoredPosition.x, -4.76f);
    }
}
