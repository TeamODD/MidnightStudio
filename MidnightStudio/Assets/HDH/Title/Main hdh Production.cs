using UnityEngine;
using System.Collections;

public class Main_hdh_Production : MonoBehaviour
{   
    public Option_Production Clapper_Production;

    private IEnumerator Clapper_Production_Sign;

    public void Clapper_Production_Start()
    {
        Clapper_Production.gameObject.SetActive(true);

        if (Clapper_Production_Sign != null) { StopCoroutine(Clapper_Production_Sign); }
        Clapper_Production_Sign = Clapper_Production_Start_Coroutine();
        StartCoroutine(Clapper_Production_Sign);
    }

    private IEnumerator Clapper_Production_Start_Coroutine()
    {
        Clapper_Production.Production_Start();
        yield return new WaitForSeconds(0.95f);

        Clapper_Production.gameObject.SetActive(false);
    }
}