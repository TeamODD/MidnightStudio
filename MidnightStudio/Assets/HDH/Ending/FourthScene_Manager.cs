using System.Collections;
using UnityEngine;

public class FourthScene_Manager : MonoBehaviour
{
    public FourthScene_1stCut Cut_1st;
    public bool IsTest = false;
    private IEnumerator Total_Production_Start_Sign;
    private void Start()
    {
        Cut_1st.gameObject.SetActive(false);

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
        // # 1stCut 시작.
        Debug.Log("1st Cut Start.");
        yield return null;
        
        Cut_1st.gameObject.SetActive(true);
        Cut_1st.Production_Start();
        yield return new WaitForSeconds(Cut_1st.Time);
        Cut_1st.gameObject.SetActive(false);
    }
}
