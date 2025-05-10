using System.Collections;
using UnityEngine;
using TMPro;

public class MoviePart_Subtitle : MonoBehaviour
{
    public TMP_Text TextBox;
    public UI_Production Self_Production;

    private IEnumerator TextShow_Sign;
    private void Start()
    {
        Self_Production.Alpha("Instant", 0f, 0f, 0f);
    }
    public void Engage(string Contents, string Height, float Time)
    {
        if (TextShow_Sign != null) { StopCoroutine(TextShow_Sign); }
        TextShow_Sign = TextShow_Coroutine(Contents, Height, Time);
        StartCoroutine(TextShow_Sign);
    }

    private IEnumerator TextShow_Coroutine(string Contents, string Height, float Time)
    {   
        switch (Height)
        {
            case "Up":
                Self_Production.Move("Instant", 0f, "y", 0f, 335f);
                break;
            case "Down":
                Self_Production.Move("Instant", 0f, "y", 0f, -335f);
                break;
        }

        Self_Production.Alpha("Instant", 0f, 0f, 1f);

        TextBox.text = Contents;
        yield return new WaitForSeconds(Time);

        Self_Production.Alpha("Instant", 0f, 0f, 0f);
    }
}
