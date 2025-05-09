using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// * 첫 번째 장면의 연출을 담당합니다.
public class FirstScene_Anime : MonoBehaviour
{
    //===================================================
    // * 최종적인 연출을 관리하는 파트.
    //===================================================
    public UI_Production[] First_Objects = new UI_Production[3];

    private IEnumerator Total_Production_Start_Sign;
    
    // * 애니메이션 테스트를 위해서만 Start()를 사용합니다.
    private void Start()
    {
        Production_Start();
    }

    // * 연출 코루틴을 실행하기 위한 함수.
    public void Production_Start()
    {
        if(Total_Production_Start_Sign != null) { StopCoroutine(Total_Production_Start_Sign); }
        Total_Production_Start_Sign = Production_Start_Coroutine();
        StartCoroutine(Total_Production_Start_Sign);
    }

    // * 실질적으로 씬의 연출을 시작하는 코루틴.
    private IEnumerator Production_Start_Coroutine()
    {
        First_Objects[0].Move("Lerp", 15f, "x", 17f, -17f);
        Protagonist_1st_Scene_Production();
        yield return null;
    }

    //===================================================
    // * 주인공 캐릭터의 연출을 담당하는 파트.
    //===================================================
    public float Protagonist_1st_Scene_MoveSpeed;

    private IEnumerator Protagonist_1st_Scene_Production_Sign;
    private IEnumerator Protagonist_2nd_Scene_Production_Sign;
    public void Protagonist_1st_Scene_Production()
    {
        if(Protagonist_1st_Scene_Production_Sign != null) { StopCoroutine(Protagonist_1st_Scene_Production_Sign); }
        Protagonist_1st_Scene_Production_Sign = Protagonist_1st_Scene_Production_Coroutine();
        StartCoroutine(Protagonist_1st_Scene_Production_Sign);
    }
    private IEnumerator Protagonist_1st_Scene_Production_Coroutine()
    {
        while(true)
        {
            First_Objects[1].Move("Lerp", Protagonist_1st_Scene_MoveSpeed / 4, "y", -1f, -1.3f);
            yield return new WaitForSeconds(Protagonist_1st_Scene_MoveSpeed / 4);
            First_Objects[1].Move("Smooth", Protagonist_1st_Scene_MoveSpeed / 4 * 3, "y", -1.3f, -1f);
            yield return new WaitForSeconds(Protagonist_1st_Scene_MoveSpeed / 4 * 3);
        }
    }
    
}
