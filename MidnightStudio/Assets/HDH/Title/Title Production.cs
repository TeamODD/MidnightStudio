using UnityEngine;
using System.Collections;

public class Title_Production : MonoBehaviour
{   
    public UI_Production Panel;
    public GameObject[] Background_Objects;

    public Background_Production Background_Production;
    public SceneFader ScenFader;
    public Option_Production Option_Production;

    private IEnumerator Option_Production_Sign;

    private void Start()
    {
        Panel.Move("Smooth", 1f, "x", 0f, -1920f);
        Panel.Alpha("Instant", 0f, 0f, 1f);
    }

    public void Option_Production_Start()
    {
        Option_Production.gameObject.SetActive(true);

        if (Option_Production_Sign != null) { StopCoroutine(Option_Production_Sign); }
        Option_Production_Sign = Option_Production_Start_Coroutine();
        StartCoroutine(Option_Production_Sign);
    }

    private IEnumerator Option_Production_Start_Coroutine()
    {
        Option_Production.Production_Start();
        yield return new WaitForSeconds(0.5f);
        
        Background_Production.Production_End();

        Background_Objects[0].SetActive(false);
        Background_Objects[1].SetActive(false);
        Background_Objects[2].SetActive(false);

        ScenFader.SetBlack(0.5f);
        yield return new WaitForSeconds(0.45f);
    }

    public void Option_Production_End()
    {
        Option_Production.gameObject.SetActive(true);

        if (Option_Production_Sign != null) { StopCoroutine(Option_Production_Sign); }
        Option_Production_Sign = Option_Production_End_Coroutine();
        StartCoroutine(Option_Production_Sign);
    }

    private IEnumerator Option_Production_End_Coroutine()
    {
        Option_Production.Production_Start();
        yield return new WaitForSeconds(0.5f);

        Background_Objects[0].SetActive(true);
        Background_Objects[1].SetActive(true);
        Background_Objects[2].SetActive(true);
        
        Background_Production.Production_Start();

        ScenFader.SetBlack(0f);
        yield return new WaitForSeconds(0.45f);
    }
}