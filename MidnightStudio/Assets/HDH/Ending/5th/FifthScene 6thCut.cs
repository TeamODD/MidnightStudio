using UnityEngine;
using System.Collections;

public class FifthScene_6thCut : MonoBehaviour
{
    public float Time;
    public UI_Production Obj_UP;
    public UI_Production Spark_UP;
    public bool IsTest = false;
    public FifthScene_Manager Scene_Manager;
    public MoviePart_Subtitle Subtitle;
    private IEnumerator Production_Start_Sign;
    public AudioClip[] Clips;

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
        Obj_UP.Position("Lerp", 5f, new Vector3(-1.21f, -0.63f, 0f), new Vector3(-1.47f, -0.58f, 0f));
        yield return new WaitForSeconds(1f);

        Scene_Manager.Fade_Panel.Coloring("Instant", 0f, Color.black, new Color(0f, 0f, 0f, 1f));
        Scene_Manager.Fade_Panel.Alpha("Instant", 0.1f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        Scene_Manager.Shake.Production_Start(15);

        Scene_Manager.Fade_Panel.Coloring("Instant", 0f, new Color(0f, 0f, 0f, 1f), new Color(0.98f, 0.46f, 0.14f, 1f));
        Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
        yield return new WaitForSeconds(0.05f);
        

        Scene_Manager.Fade_Panel.Coloring("Instant", 0f, new Color(0f, 0f, 0f, 1f), new Color(0f, 0f, 0f, 1f));
        Spark_UP.Alpha("Instant", 0f, 0f, 0f);
        yield return new WaitForSeconds(0.05f);
        // * 컷.
    }
}
