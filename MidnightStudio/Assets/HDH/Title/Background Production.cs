using System.Collections;
using UnityEngine;

public class Background_Production : MonoBehaviour
{
    public UI_Production[] Obj = new UI_Production[3];
    
    public bool IsTest = false;
    private IEnumerator Client_Production_Sign;
    private IEnumerator Ink_Production_Sign;
    private IEnumerator Film_Production_Sign;

    private void Start()
    {
        if (IsTest == true) { Production_Start(); }
    }

    public void Production_Start()
    {
        Client_Production();
        Ink_Production();
        Film_Production();
    }

    public void Production_End()
    {
        if (Client_Production_Sign != null) { StopCoroutine(Client_Production_Sign); }
        if (Ink_Production_Sign != null) { StopCoroutine(Ink_Production_Sign); }
        if (Film_Production_Sign != null) { StopCoroutine(Film_Production_Sign); }
    }

    private void Client_Production()
    {
        if (Client_Production_Sign != null) { StopCoroutine(Client_Production_Sign); }
        Client_Production_Sign = Client_Production_Coroutine();
        StartCoroutine(Client_Production_Sign);
    }

    private IEnumerator Client_Production_Coroutine()
    {
        while (true)
        {
            Obj[0].Scale("Smooth", 2f, new Vector3(0.95f, 0.9f, 1f), new Vector3(0.9f, 0.95f, 1f));
            yield return new WaitForSeconds(2f);

            Obj[0].Scale("Smooth", 2f, new Vector3(0.9f, 0.95f, 1f), new Vector3(0.95f, 0.9f, 1f));
            yield return new WaitForSeconds(2f);
        }
    }

    private void Ink_Production()
    {
        if (Ink_Production_Sign != null) { StopCoroutine(Ink_Production_Sign); }
        Ink_Production_Sign = Ink_Production_Coroutine();
        StartCoroutine(Ink_Production_Sign);
    }

    private IEnumerator Ink_Production_Coroutine()
    {
        while (true)
        {
            Obj[1].Scale("Smooth", 1f, new Vector3(0.95f, 0.94f, 1f), new Vector3(0.94f, 0.95f, 1f));
            yield return new WaitForSeconds(1f);

            Obj[1].Scale("Smooth", 1f, new Vector3(0.94f, 0.95f, 1f), new Vector3(0.95f, 0.94f, 1f));
            yield return new WaitForSeconds(1f);
        }
    }

    private void Film_Production()
    {
        if (Film_Production_Sign != null) { StopCoroutine(Film_Production_Sign); }
        Film_Production_Sign = Film_Production_Coroutine();
        StartCoroutine(Film_Production_Sign);
    }

    private IEnumerator Film_Production_Coroutine()
    {
        while (true)
        {
            Obj[2].Scale("Smooth", 1.25f, new Vector3(0.95f, 0.926f, 1f), new Vector3(0.926f, 0.95f, 1f));
            yield return new WaitForSeconds(1.25f);

            Obj[2].Scale("Smooth", 1.25f, new Vector3(0.926f, 0.95f, 1f), new Vector3(0.95f, 0.926f, 1f));
            yield return new WaitForSeconds(1.25f);
        }
    }
}
