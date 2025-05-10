using System.Collections;
using UnityEngine;

public class FourthScene_1stCut : MonoBehaviour
{
    public float Time;
    public UI_Production[] Obj = new UI_Production[5];
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
        // * 액션.
        Obj[0].Position("Instant", 0f, Vector3.zero, new Vector3(-12.5f, 12f, 0f));
        Obj[0].Rotation("Instant", 0f, Vector3.zero, new Vector3(0f, 0f, 8f));
        Obj[0].Scale("Instant", 0f, Vector3.zero, new Vector3(4f, 4f, 1f));

        Obj[3].Position("Instant", 0f, Vector3.zero, new Vector3(2.2f, -2.65f, -1f));
        Obj[4].Position("Smooth", 0.1f, new Vector3(3f, -2.5f, -1f), new Vector3(3.25f, -2.95f, -1f));
        yield return new WaitForSeconds(0.1f);

        Obj[4].Position("Lerp", 0.1f, new Vector3(3.25f, -2.95f, -1f), new Vector3(3.325f, -3.05f, -1f));
        Obj[4].Rotation("Lerp", 0.1f, Vector3.zero, new Vector3(0f, 0f, 3f));
        yield return new WaitForSeconds(0.1f);

        Obj[4].Position("Smooth", 0.1f, new Vector3(3.325f, -3.05f, -1f), new Vector3(3.25f, -2.95f, -1f));
        Obj[4].Rotation("Smooth", 0.1f, new Vector3(0f, 0f, 3f), new Vector3(0f, 0f, 8f));
        yield return new WaitForSeconds(0.2f);

        Obj[3].gameObject.SetActive(false);
        Obj[4].gameObject.SetActive(false);
        Obj[2].gameObject.SetActive(true);
        
        Obj[0].Position("Smooth", 0.2f, new Vector3(-12.5f, 12f, 0f), new Vector3(0f, 0f, 0f));
        Obj[0].Rotation("Smooth", 0.2f, new Vector3(0f, 0f, 8f), new Vector3(0f, 0f, 6f));
        Obj[0].Scale("Smooth", 0.2f, new Vector3(4f, 4f, 1f), new Vector3(1.1f, 1.1f, 1f));
        yield return new WaitForSeconds(0.2f);

        Obj[0].Rotation("Smooth", 5f, new Vector3(0f, 0f, 6f), new Vector3(0f, 0f, 0f));
        Obj[0].Scale("Smooth", 5f, new Vector3(1.1f, 1.1f, 1f), new Vector3(0.95f, 0.95f, 1f));
        yield return new WaitForSeconds(5f);
        // * 컷.
    }
}
