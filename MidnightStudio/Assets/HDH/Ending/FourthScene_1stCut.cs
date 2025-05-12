using System.Collections;
using UnityEngine;

public class FourthScene_1stCut : MonoBehaviour
{
    public float Time;
    public UI_Production[] Obj;
    public Sprite[] Protagonist = new Sprite[2];
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
        yield return new WaitForSeconds(0.35f);
        
        // * 액션.
        Obj[0].Position("Instant", 0f, Vector3.zero, new Vector3(-1.4f, 0f, 0f));
        Obj[0].Rotation("Instant", 0f, Vector3.zero, Vector3.zero);
        yield return null;

        Obj[0].Position("Smooth", 0.25f, new Vector3(-1.4f, 0f, 0f), new Vector3(-0.8f, -0.25f, 0f));
        Obj[0].Rotation("Smooth", 0.25f, Vector3.zero, new Vector3(0f, 0f, -6f));
        yield return new WaitForSeconds(0.25f);

        Obj[0].Position("Lerp", 0.1f, new Vector3(-0.8f, -0.25f, 0f), new Vector3(-0.5f, -0.25f, 0f));
        Obj[0].Rotation("Lerp", 0.1f, new Vector3(0f, 0f, -6f), new Vector3(0f, 0f, 3f));
        yield return new WaitForSeconds(0.1f);
        
        Obj[0].Position("Smooth", 0.15f, new Vector3(-0.5f, -0.25f, 0f), new Vector3(-0.25f, 0f, 0f));
        Obj[0].Rotation("Smooth", 0.15f, new Vector3(0f, 0f, 3f), new Vector3(0f, 0f, 0f));
        yield return new WaitForSeconds(0.15f);
        // * 컷.
    }
}
