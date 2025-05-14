using UnityEngine;
using System.Collections;

public class FifthScene_5thCut : MonoBehaviour
{
    public float Time;
    public UI_Production[] Obj_UP;
    public bool IsTest = false;
    public FifthScene_Manager Scene_Manager;
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
        // * 액션.
        //Obj_UP[0].Position("Lerp", 7f, new Vector3(0f, 0f, 0f), new Vector3(-0.65f, 0f, 0f));
        Obj_UP[0].Scale("Lerp", 7.5f, new Vector3(1.1f, 1.1f, 0f), new Vector3(0.9275f, 0.9275f, 0f));

        Obj_UP[1].Move("Lerp", 7.5f, "x", -2.5f, -2.25f);
        Obj_UP[1].Scale("Lerp", 7.5f, new Vector3(1f, 1f, 0f), new Vector3(0.9275f, 0.9275f, 0f));

        Obj_UP[2].Position("Lerp", 7.5f, new Vector3(5.5f, -3.35f, 0f), new Vector3(3.6f, -2.85f, 0f));
        Obj_UP[2].Scale("Lerp", 7.5f, new Vector3(1.1f, 1.1f, 0f), new Vector3(0.9275f, 0.9275f, 0f));
        yield return new WaitForSeconds(1f);

        Subtitle.Engage("달의 그림자에게 안식을.", "Down", 2f);
        yield return new WaitForSeconds(4f);
        // * 컷.
    }
}
