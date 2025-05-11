using UnityEngine;
using System.Collections;

public class Option_Production : MonoBehaviour
{
    public bool IsTest = false;

    public UI_Production ClapperBoard;
    public UI_Production ClapperBar;

    private IEnumerator ClapperBorad_Production_Sign;

    private void Start()
    {
        // if (IsTest == true) { Production_Start(); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (IsTest == true) { Production_Start(); }
        }
    }

    public void Production_Start()
    {
        if (ClapperBorad_Production_Sign != null) { StopCoroutine(ClapperBorad_Production_Sign); }
        ClapperBorad_Production_Sign = ClapperBorad_Production_Coroutine();
        StartCoroutine(ClapperBorad_Production_Sign);
    }

    private IEnumerator ClapperBorad_Production_Coroutine()
    {
        ClapperBar.Rotation("Instant", 0f, Vector3.zero, new Vector3(0f, 0f, 7.5f));

        ClapperBoard.Position("Lerp", 0.2f, new Vector3(-2260f, -530f, 0f), new Vector3(0f, -242f, 0f));
        ClapperBoard.Rotation("Lerp", 0.2f, new Vector3(0f, 0f, -15f), new Vector3(0f, 0f, 0f));
        yield return new WaitForSeconds(0.2f);

        ClapperBoard.Position("Smooth", 0.25f, new Vector3(0f, -242f, 0f), new Vector3(-100f, 105f, 0f));
        ClapperBoard.Rotation("Smooth", 0.25f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 5f));
        ClapperBar.Rotation("Smooth", 0.25f, new Vector3(0f, 0f, 7.5f), new Vector3(0f, 0f, 25f));
        yield return new WaitForSeconds(0.3f);

        ClapperBoard.Position("Lerp", 0.05f, new Vector3(-100f, 105f, 0f), new Vector3(-100f, -375f, 0f));
        ClapperBoard.Rotation("Lerp", 0.05f, new Vector3(0f, 0f, 5f), new Vector3(0f, 0f, 0f));
        ClapperBar.Rotation("Lerp", 0.05f, new Vector3(0f, 0f, 10f), new Vector3(0f, 0f, 0f));
        yield return new WaitForSeconds(0.05f);

        ClapperBoard.Position("Smooth", 0.15f, new Vector3(-100f, -375f, 0f), new Vector3(-100f, 105f, 0f));
        ClapperBoard.Rotation("Smooth", 0.15f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 5f));
        yield return new WaitForSeconds(0.2f);

        ClapperBoard.Position("Smooth", 0.2f, new Vector3(-100f, 105f, 0f), new Vector3(-2260f, -530f, 0f));
        ClapperBoard.Rotation("Smooth", 0.2f, new Vector3(0f, 0f, 5f), new Vector3(0f, 0f, -15f));
        yield return new WaitForSeconds(0.2f);
    }
}
