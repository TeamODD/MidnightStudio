using UnityEngine;
using System.Collections;

public class FifthScene_4thCut : MonoBehaviour
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
        Obj_UP[0].Position("Lerp", 5f, new Vector3(0f, 0f, 0f), new Vector3(-0.65f, 0f, 0f));
        Obj_UP[0].Scale("Lerp", 5f, new Vector3(0.9275f, 0.9275f, 0f), new Vector3(1f, 1f, 0f));

        Obj_UP[1].Position("Lerp", 10f, new Vector3(-5f, 0.32f, 0f), new Vector3(-7f, 0.38f, 0f));
        Obj_UP[1].Scale("Lerp", 10f, new Vector3(0.9275f, 0.9275f, 0f), new Vector3(1.1f, 1.1f, 0f));

        Obj_UP[2].Position("Lerp", 10f, new Vector3(2.27f, -0.71f, 0f), new Vector3(1.62f, -0.85f, 0f));
        Obj_UP[2].Scale("Lerp", 10f, new Vector3(0.9275f, 0.9275f, 0f), new Vector3(1f, 1f, 0f));

        Subtitle.Engage("그렇게도 듣고 싶어 하는 말을 해주도록 하지.", "Down", 2f);
        yield return new WaitForSeconds(4f);
        // * 컷.
    }
}
