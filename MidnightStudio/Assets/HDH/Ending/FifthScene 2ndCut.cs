using UnityEngine;
using System.Collections;

public class FifthScene_2ndCut : MonoBehaviour
{
    public float Time;
    public SpriteRenderer Obj_SR;
    public Obj_Production Obj_OP;
    public UI_Production Obj_UP;
    public Sprite[] Protagonist = new Sprite[2];
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
        Obj_SR.sprite = Protagonist[1];

        Obj_UP.Position("Lerp", 5f, new Vector3(1.12f, -1.4f, 0f), new Vector3(-0.32f, -1.15f, 0f));
        yield return new WaitForSeconds(2f);
        // * 컷.
    }
}
