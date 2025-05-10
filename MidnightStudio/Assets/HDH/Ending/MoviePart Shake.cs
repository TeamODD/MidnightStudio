using System.Collections;
using UnityEngine;

public class MoviePart_Shake : MonoBehaviour
{
    public Obj_Production Camera_Production;
    private IEnumerator Total_Production_Start_Sign;

    // # 애니메이션을 시작하는 함수.
    public void Production_Start(int Repeat)
    {
        if (Total_Production_Start_Sign != null) { StopCoroutine(Total_Production_Start_Sign); }
        Total_Production_Start_Sign = Production_Start_Coroutine(Repeat);
        StartCoroutine(Total_Production_Start_Sign);
    }

    // # 애니메이션 로직.
    private IEnumerator Production_Start_Coroutine(int Repeat)
    {
        Vector3 Target_Pos = Vector3.zero;

        for(int i = 0; i < Repeat; i++)
        {
            Target_Pos = new Vector3(Random.Range(-0.615f, 0.615f), Random.Range(-0.35f, 0.35f), -10f);
            Camera_Production.Position("Lerp", true, 0.01f, Camera_Production.transform.position, Target_Pos);
            yield return new WaitForSeconds(0.01f);
        }

        Camera_Production.Position("Instant", true, 0f, Vector3.zero, new Vector3(0f, 0f, -10f));
    }
}
