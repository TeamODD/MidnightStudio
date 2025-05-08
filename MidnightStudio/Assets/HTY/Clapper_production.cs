using UnityEngine;

public class Clapper_production : MonoBehaviour
{
    public UI_Production ui_production;
    public RectTransform RectClapper;

    public void ClapperMoveX()
    {
        ui_production.Move("Smooth", 0.85f, "x", RectClapper.anchoredPosition.x, 0f);
    }

    public void ClapperMoveY()
    {
        ui_production.Move("Smooth", 0.57f, "y", RectClapper.anchoredPosition.y, -120f);
    }

    public void ClapperRotate()
    {
        ui_production.Rotation("Smooth", 0.2f, new Vector3(0f,0f,0f), new Vector3(0f,0f,20f));
    }



}
