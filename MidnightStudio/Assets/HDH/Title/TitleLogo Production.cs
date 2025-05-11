using System.Collections;
using UnityEngine;

public class TitleLogo_Production : MonoBehaviour
{
    public float BPM;
    public bool IsTest = false;
    public UI_Production Title_Logo;
    private IEnumerator Title_Production_Sign;

    private void Start()
    {
        if (IsTest == true) { Production_Start(); }
    }

    public void Production_Start()
    {
        if (Title_Production_Sign != null) { StopCoroutine(Title_Production_Sign); }
        Title_Production_Sign = Title_Production_Coroutine();
        StartCoroutine(Title_Production_Sign);
    }

    private IEnumerator Title_Production_Coroutine()
    {
        while (true)
        {
            float WaitingTime = 60 / BPM;

            Title_Logo.Scale("Smooth", WaitingTime / 8 * 7, new Vector3(1.1f, 1.1f, 1f), new Vector3(1f, 1f, 1f));
            yield return new WaitForSeconds(WaitingTime / 8 * 7);

            Title_Logo.Scale("Smooth", WaitingTime / 8, new Vector3(1f, 1f, 1f), new Vector3(1.1f, 1.1f, 1f));
            yield return new WaitForSeconds(WaitingTime / 8);
        }
    }
}
