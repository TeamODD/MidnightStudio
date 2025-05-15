using System.Collections;
using UnityEngine;

public class FourthScene_1stCut : MonoBehaviour
{
    public float Time;
    public Obj_Production Obj_OP;
    public UI_Production Obj_UP;
    public SpriteRenderer Obj_SR;
    public Sprite[] Enemy = new Sprite[2];
    public bool IsTest = false;
    public FourthScene_Manager Scene_Manager;
    public MoviePart_Subtitle Subtitle;
    public AudioClip[] Clips;
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
        yield return new WaitForSeconds(0.3f);
        
        // * 액션.
        Obj_UP.Position("Instant", 0f, Vector3.zero, new Vector3(-1.4f, 0f, 0f));
        Obj_UP.Rotation("Instant", 0f, Vector3.zero, Vector3.zero);
        yield return null;

        Obj_UP.Position("Smooth", 0.25f, new Vector3(-1.4f, 0f, 0f), new Vector3(-0.8f, -0.25f, 0f));
        Obj_UP.Rotation("Smooth", 0.25f, Vector3.zero, new Vector3(0f, 0f, -6f));
        Scene_Manager.AudioPlayer.PlaySE(Clips[0]);
        yield return new WaitForSeconds(0.1f);

        Obj_SR.sprite = Enemy[1];
        Obj_OP.Coloring("Lerp", 0.15f, new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f));
        yield return new WaitForSeconds(0.15f);

        Obj_UP.Position("Lerp", 0.1f, new Vector3(-0.8f, -0.25f, 0f), new Vector3(-0.5f, -0.25f, 0f));
        Obj_UP.Rotation("Lerp", 0.1f, new Vector3(0f, 0f, -6f), new Vector3(0f, 0f, 3f));
        yield return new WaitForSeconds(0.1f);
        
        Obj_UP.Position("Smooth", 0.15f, new Vector3(-0.5f, -0.25f, 0f), new Vector3(-0.25f, 0f, 0f));
        Obj_UP.Rotation("Smooth", 0.15f, new Vector3(0f, 0f, 3f), new Vector3(0f, 0f, 0f));
        yield return new WaitForSeconds(0.35f);
        // * 컷.
    }
}
