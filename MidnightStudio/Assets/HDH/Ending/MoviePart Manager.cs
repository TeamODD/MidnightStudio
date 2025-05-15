using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoviePart_Manager : MonoBehaviour
{
    public List<string> Test_Sequence = new List<string> { "2", "3", "1", "4", "0" };
    public EndingManager EndingManager;
    public UI_Production Fade_Panel;
    public FirstScene_Manager FirstScene;
    public SecondScene_Manager SecondScene;
    public ThirdScene_Manager ThirdScene;
    public FourthScene_Manager FourthScene;
    public FifthScene_Manager FifthScene;
    public bool IsTest = false;
    private IEnumerator Total_Production_Start_Sign;

    private void Start()
    {
        // FirstScene.gameObject.SetActive(false);
        // SecondScene.gameObject.SetActive(false);
        // ThirdScene.gameObject.SetActive(false);
        // FourthScene.gameObject.SetActive(false);
        // FifthScene.gameObject.SetActive(false);

        if (IsTest == true) { Production_Start(Test_Sequence); }
    }

    // # 애니메이션을 시작하는 함수.
    public void Production_Start(List<string> Sequence)
    {
        if (Total_Production_Start_Sign != null) { StopCoroutine(Total_Production_Start_Sign); }
        Total_Production_Start_Sign = Production_Start_Coroutine(Sequence);
        StartCoroutine(Total_Production_Start_Sign);
    }

    // # 애니메이션 로직.
    private IEnumerator Production_Start_Coroutine(List<string> Sequence)
    {
        yield return new WaitForSeconds(1f);

        Fade_Panel.Alpha("Lerp", 0.5f, 1f, 0f);

        foreach (string Value in Sequence)
        {
            switch (Value)
            {
                case "1":
                    Debug.Log("1st Scene - 1st Cut Start.");
                    if (Value == Sequence[4]) { FirstScene.IsLast = true; }
                    FirstScene.gameObject.SetActive(true);
                    FirstScene.Production_Start();
                    yield return new WaitForSeconds(FirstScene.Cut_1st.Time + FirstScene.Cut_2nd.Time);

                    if (FirstScene.IsLast == false) { FirstScene.gameObject.SetActive(false); }
                    break;
                case "3":
                    Debug.Log("2nd Scene - 1st Cut Start.");
                    if (Value == Sequence[4]) { SecondScene.IsLast = true; }
                    SecondScene.gameObject.SetActive(true);
                    SecondScene.Production_Start();
                    yield return new WaitForSeconds(SecondScene.Cut_1st.Time);

                    if (SecondScene.IsLast == false) { SecondScene.gameObject.SetActive(false); }
                    break;
                case "2":
                    Debug.Log("3rd Scene - 1st Cut Start.");
                    if (Value == Sequence[4]) { ThirdScene.IsLast = true; }
                    ThirdScene.gameObject.SetActive(true);
                    ThirdScene.Production_Start();
                    yield return new WaitForSeconds(ThirdScene.Cut_1st.Time);

                    if (ThirdScene.IsLast == false) { ThirdScene.gameObject.SetActive(false); }
                    break;
                case "4":
                    Debug.Log("4th Scene - 2nd Cut Start.");
                    if (Value == Sequence[4]) { FourthScene.IsLast = true; }
                    FourthScene.gameObject.SetActive(true);
                    FourthScene.Production_Start();
                    yield return new WaitForSeconds(FourthScene.Cut_2nd.Time + FourthScene.Cut_3rd.Time + FourthScene.Cut_4th.Time);

                    if (FourthScene.IsLast == false) { FourthScene.gameObject.SetActive(false); }
                    break;
                case "0":
                    Debug.Log("5th Scene - 1st Cut Start.");
                    if (Value == Sequence[4]) { FifthScene.IsLast = true; }
                    FifthScene.gameObject.SetActive(true);
                    FifthScene.Production_Start();
                    yield return new WaitForSeconds(
                        FifthScene.Cut_1st.Time + FifthScene.Cut_2nd.Time + FifthScene.Cut_3rd.Time + FifthScene.Cut_4th.Time + FifthScene.Cut_5th.Time + FifthScene.Cut_6th.Time);

                    if (FifthScene.IsLast == false) { FifthScene.gameObject.SetActive(false); }
                    break;
            }

            if (Sequence[4] == Value)
            {
                Fade_Panel.Alpha("Lerp", 0.5f, 0f, 1f);
                yield return new WaitForSeconds(3f);

                EndingManager.MovieEnd();
            }
        }
    }
}
