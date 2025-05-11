using UnityEngine;
using System.Collections;

public class TextBox_Production : MonoBehaviour
{
    public bool IsTest = false;
    public Sprite[] SpeakerBox_Sprites;

    public UI_Production TextBox;
    public UI_Production SpeakerBox;

    private IEnumerator TextBox_Production_Sign;
    private IEnumerator SpeakerBox_Production_Sign;

    private void Start()
    {
        if (IsTest == true)
        {
            TextBox_Production_Start();
            SpeakerBox_Production_Start("ink");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsTest == true)
            {
                TextBox_Production_Start();
                SpeakerBox_Production_Start("client");
            }
        }
    }

    public void TextBox_Production_Start()
    {
        if (TextBox_Production_Sign != null) { StopCoroutine(TextBox_Production_Sign); }
        TextBox_Production_Sign = TextBox_Production_Start_Coroutine();
        StartCoroutine(TextBox_Production_Sign);
    }

    private IEnumerator TextBox_Production_Start_Coroutine()
    {
        TextBox.Move("Smooth", 0.2f, "y", -285f, -265f);
        TextBox.Alpha("Smooth", 0.2f, 0f, 1f);
        yield return new WaitForSeconds(0.2f);
    }

    public void SpeakerBox_Production_Start(string Peaker)
    {
        if (SpeakerBox_Production_Sign != null) { StopCoroutine(SpeakerBox_Production_Sign); }
        SpeakerBox_Production_Sign = SpeakerBox_Production_Start_Coroutine(Peaker);
        StartCoroutine(SpeakerBox_Production_Sign);
    }

    private IEnumerator SpeakerBox_Production_Start_Coroutine(string Peaker)
    {
        if (Peaker == "ink") { SpeakerBox.Image(SpeakerBox_Sprites[0]); }
        else if (Peaker == "client") { SpeakerBox.Image(SpeakerBox_Sprites[1]); }

        SpeakerBox.Move("Lerp", 0.1f, "x", -740f, -640f);
        SpeakerBox.Alpha("Lerp", 0.1f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        SpeakerBox.Move("Smooth", 0.2f, "x", -640f, -675f);
        SpeakerBox.Alpha("Smooth", 0.2f, 0f, 1f);
        yield return new WaitForSeconds(0.2f);
    }
}
