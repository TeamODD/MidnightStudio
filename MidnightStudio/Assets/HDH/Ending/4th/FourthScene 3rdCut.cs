using UnityEngine;
using System.Collections;

public class FourthScene_3rdCut : MonoBehaviour
{
    public float Time;
    public Obj_Production Obj_OP;
    public UI_Production Obj_UP;
    public bool IsTest = false;
    public FourthScene_Manager Scene_Manager;
    public MoviePart_Subtitle Subtitle;
    private IEnumerator Production_Start_Sign;

    private void Start()
    {
        if (IsTest == true) { Production_Start(); }
    }

    // # 애니메이션을 시작하는 함수.
    public void Production_Start()
    {
        if (Production_Start_Sign != null) { StopCoroutine(Production_Start_Sign); }
        Production_Start_Sign = Production_Start_Coroutine();
        StartCoroutine(Production_Start_Sign);
    }

    // # 애니메이션 로직.
    private IEnumerator Production_Start_Coroutine()
    {
        Obj_OP.Coloring("Instant", 0f, Color.black, new Color(1f, 1f, 1f, 0f));
        // * 액션.
        Obj_UP.Move("Smooth", 0.15f, "x", -1.25f, -0.7f);
        Obj_OP.Coloring("Lerp", 0.07f, new Color(1f, 1f, 1f, 0.5f), new Color(1f, 1f, 1f, 1f));
        yield return new WaitForSeconds(0.15f);

        Subtitle.Engage("세레니티움도 별거 아니군.", "Down", 2f);
        yield return new WaitForSeconds(2.1f);

        Subtitle.Engage("당신과도 여기서 작별인사야.", "Down", 3f);
        yield return new WaitForSeconds(2.75f);
        // * 컷.
    }
}
