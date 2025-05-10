using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FirstScene_Manager : MonoBehaviour
{
    public FirstScene_1stCut Cut_1st;
    public FirstScene_2ndCut Cut_2nd;
    public UI_Production[] Production_Ojbects;
    public bool IsTest = false;
    private IEnumerator Total_Production_Start_Sign;

    private void Start()
    {
        Cut_1st.gameObject.SetActive(false);
        Cut_2nd.gameObject.SetActive(false);

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

        // # 2ndCut 시작.
        Debug.Log("2nd Cut Start.");
        yield return null;
        
        Cut_2nd.gameObject.SetActive(true);
        Cut_2nd.Production_Start();
        yield return new WaitForSeconds(Cut_2nd.Time);
        Cut_2nd.gameObject.SetActive(false);
    }
}