using System.Collections;
using UnityEngine;

public class MoviePart_Manager : MonoBehaviour
{
    public int[] Sequence = new int[] {0, 1, 2, 3, 4};
    public FirstScene_Manager FirstScene;
    public SecondSceneManager SecondScene;
    public bool IsTest = false;
    private IEnumerator Total_Production_Start_Sign;

    private void Start()
    {
        FirstScene.gameObject.SetActive(false);
        SecondScene.gameObject.SetActive(false);

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
        foreach(int Value in Sequence)
        {
            switch(Value)
            {
                case 0:
                    Debug.Log("1st Scene - 1st Cut Start.");
                    FirstScene.gameObject.SetActive(true);
                    FirstScene.Production_Start();
                    yield return new WaitForSeconds(FirstScene.Cut_1st.Time + FirstScene.Cut_2nd.Time);

                    FirstScene.gameObject.SetActive(false);
                    break;
                case 1:
                    Debug.Log("2nd Scene - 2nd Cut Start.");
                    SecondScene.gameObject.SetActive(true);
                    SecondScene.Production_Start();
                    yield return new WaitForSeconds(SecondScene.Cut_1st.Time);

                    SecondScene.gameObject.SetActive(false);
                    break;
                case 2:
                    Debug.Log("3rd Scene - 3rd Cut Start.");
                    break;
            }
        }
    }
}
