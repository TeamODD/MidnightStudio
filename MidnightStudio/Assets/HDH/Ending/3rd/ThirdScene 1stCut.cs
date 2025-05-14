using System.Collections;
using UnityEngine;

public class ThirdScene_1stCut : MonoBehaviour
{
    public float Time;
    public UI_Production Chara_Obj;
    public Obj_Production[] Effect_Obj;
    public SpriteRenderer Protagonist_SR;
    public Sprite[] Protagonist = new Sprite[3];
    public bool IsTest = false;
    public ThirdScene_Manager Scene_Manager;
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
        Protagonist_SR.sprite = Protagonist[0];
        Chara_Obj.Position("Smooth", 0.3f, new Vector3(2f, -0.95f, -1f), new Vector3(2.6f, -0.95f, -1f));
        Chara_Obj.Rotation("Lerp", 0.1f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 3f));
        yield return new WaitForSeconds(0.1f);

        Chara_Obj.Rotation("Smooth", 0.1f, new Vector3(0f, 0f, 3f), new Vector3(0f, 0f, 0f));
        yield return new WaitForSeconds(0.5f);

        Protagonist_SR.sprite = Protagonist[1];
        Chara_Obj.Rotation("Smooth", 0.1f, new Vector3(0f, 0f, 3f), new Vector3(0f, 0f, 0f));
        Effect_Obj[0].gameObject.SetActive(true);
        yield return null;

        Effect_Obj[0].Coloring("Lerp", 0.25f, new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f));
        yield return new WaitForSeconds(0.45f);

        Effect_Obj[0].gameObject.SetActive(false);

        Scene_Manager.Fade_Panel.Coloring("Instant", 0f, Color.black, new Color(0f, 0f, 0f, 1f));
        Scene_Manager.Fade_Panel.Alpha("Instant", 0.1f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        Scene_Manager.Shake.Production_Start(15);

        Effect_Obj[2].Move("Lerp", true, 0.5f, "y", 4.18f, 4.36f);
        Effect_Obj[2].Coloring("Lerp", 1f, new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f));

        Chara_Obj.Rotation("Smooth", 0.15f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, -9f));
        Scene_Manager.Fade_Panel.Coloring("Instant", 0f, new Color(0f, 0f, 0f, 1f), new Color(0.98f, 0.46f, 0.14f, 1f));
        Scene_Manager.Fade_Panel.Alpha("Lerp", 0.1f, 1f, 0f);
        yield return new WaitForSeconds(0.3f);

        Protagonist_SR.sprite = Protagonist[2];
        Scene_Manager.Shake.Production_Start(30);

        Effect_Obj[1].gameObject.SetActive(true);
        Effect_Obj[1].Position("Smooth", true, 1.1f, new Vector3(8.252f, 0.435f, -2f), new Vector3(8.95f, 0.435f, -2f));
        Effect_Obj[1].Scale("Smooth", 0.75f, new Vector3(1f, 1f, 1f), new Vector3(1.25f, 1.25f, 1f));
        Effect_Obj[1].Coloring("Lerp", 1.1f, new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f));

        Chara_Obj.Position("Smooth", 0.15f, new Vector3(2.6f, -0.95f, -1f), new Vector3(3.2f, -0.95f, -1f));
        Chara_Obj.Rotation("Smooth", 0.5f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 6f));
        yield return new WaitForSeconds(0.15f);

        Chara_Obj.Position("Smooth", 0.4f, new Vector3(3.2f, -0.95f, -1f), new Vector3(3.4f, -0.95f, -1f));
        yield return new WaitForSeconds(0.4f);

        Chara_Obj.Position("Smooth", 0.45f, new Vector3(3.4f, -0.95f, -1f), new Vector3(3.8f, -0.95f, -1f));
        Chara_Obj.Rotation("Smooth", 0.45f, new Vector3(0f, 0f, 6f), new Vector3(-2f, 0f, 0f));
        yield return new WaitForSeconds(0.45f);

        // * 디졸브

        // * 컷.
    }
}
