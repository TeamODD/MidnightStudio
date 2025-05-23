using UnityEngine;
using System.Collections;

public class FifthScene_Manager : MonoBehaviour
{
    public UI_Production Fade_Panel;
    public FifthScene_1stCut Cut_1st;
    public FifthScene_2ndCut Cut_2nd;
    public FifthScene_3rdCut Cut_3rd;
    public FifthScene_4thCut Cut_4th;
    public FifthScene_5thCut Cut_5th;
    public FifthScene_6thCut Cut_6th;
    public bool IsLast = false;
    public bool IsTest = false;
    public MoviePart_Shake Shake;
    private IEnumerator Total_Production_Start_Sign;

    public AudioManager AudioPlayer;

    private void Start()
    {
        AudioPlayer = AudioManager.Instance;
        
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
        // # 1stCut 시작.
        Debug.Log("1st Cut Start.");
        yield return null;
        
        Cut_1st.gameObject.SetActive(true);
        Cut_1st.Production_Start();
        yield return new WaitForSeconds(Cut_1st.Time - 0.01f);
        Cut_1st.gameObject.SetActive(false);

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

        // # 4thCut 시작.
        Debug.Log("4th Cut Start.");
        yield return null;
        
        Cut_4th.gameObject.SetActive(true);
        Cut_4th.Production_Start();
        yield return new WaitForSeconds(Cut_4th.Time - 0.01f);
        Cut_4th.gameObject.SetActive(false);

        // # 5thCut 시작.
        Debug.Log("5th Cut Start.");
        yield return null;
        
        Cut_5th.gameObject.SetActive(true);
        Cut_5th.Production_Start();
        yield return new WaitForSeconds(Cut_5th.Time - 0.01f);
        Cut_5th.gameObject.SetActive(false);

        // # 6thCut 시작.
        Debug.Log("6th Cut Start.");
        yield return null;
        
        Cut_6th.gameObject.SetActive(true);
        Cut_6th.Production_Start();
        yield return new WaitForSeconds(Cut_6th.Time - 0.01f);
        Cut_6th.gameObject.SetActive(false);
    }
}
