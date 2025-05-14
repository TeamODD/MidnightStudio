using System.Collections;
using UnityEngine;

public class FourthScene_Manager : MonoBehaviour
{
    public FourthScene_2ndCut Cut_2nd;
    public FourthScene_3rdCut Cut_3rd;
    public FourthScene_4thCut Cut_4th;
    public bool IsLast = false;
    public bool IsTest = false;
    private IEnumerator Total_Production_Start_Sign;
    private void Start()
    {
        //Cut_2nd.gameObject.SetActive(true);

        if (IsTest == true) { Production_Start(); }
    }

    // # 애니메이션을 시작하는 함수.
    public void Production_Start()
    {
        if (Total_Production_Start_Sign != null) { StopCoroutine(Total_Production_Start_Sign); }
        Total_Production_Start_Sign = Production_Start_Coroutine();
        StartCoroutine(Total_Production_Start_Sign);
    }

    // # 애니메이션 로직.
    private IEnumerator Production_Start_Coroutine()
    {
        // # 2ndCut 시작.
        Debug.Log("2nd Cut Start.");
        yield return null;
        
        Cut_2nd.gameObject.SetActive(true);
        Cut_2nd.Production_Start();
        yield return new WaitForSeconds(Cut_2nd.Time - 0.01f);
        Cut_2nd.gameObject.SetActive(false);

        // # 3rdCut 시작.
        Debug.Log("3rd Cut Start.");
        yield return null;
        
        Cut_3rd.gameObject.SetActive(true);
        Cut_3rd.Production_Start();
        yield return new WaitForSeconds(Cut_3rd.Time - 0.01f);
        
        Cut_3rd.gameObject.SetActive(false);

        // # 4thCUt 시작.
        Debug.Log("4th Cut Start.");
        yield return null;
        
        Cut_4th.gameObject.SetActive(true);
        Cut_4th.Production_Start();
        yield return new WaitForSeconds(Cut_4th.Time - 0.01f);
        
        if (!IsLast) { Cut_4th.gameObject.SetActive(false); }
    }
}
