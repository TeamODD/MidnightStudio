using UnityEngine;

public class Cut_production : MonoBehaviour
{
    public UI_Production ui_production;

    public void Cut_AlpahSetZero() { //처음 0으로 초기화
        ui_production.Alpha("Instant", 1f, 1, 0);
    }

    public void Cut_AlpahUpdate() { //시작 연출
        ui_production.Scale("Lerp",0.1f, new Vector3(3f,3f,1f), new Vector3(0.8f, 0.8f, 1f));
        ui_production.Alpha("Instant", 0.35f, 0, 1);
    }

    public void Cut_AlpahFianl() //끝 연출
    {
        ui_production.Scale("Smooth", 0.25f, new Vector3(0.8f, 0.8f, 1f), new Vector3(1f,1f,1f));
    }
}
