// =====================================================================
// 문장을 가운데에 띄우고 싶다? -> ShowText("띄우고 싶은 문장");
// 문장을 점차 사라지게 싶다? -> ShowHide();
// 문장을 없애고 싶다? -> ShowDestory();
// =====================================================================

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueControl : MonoBehaviour
{
    public void ShowText(string Sentence)
    {
        DestoryText();

        AllExported = false;
        TextDivide(Sentence);
        TextAlign(PreTexts);

        if (TextShow_Sign != null) { StopCoroutine(TextShow_Sign); }
        TextShow_Sign = ShowText_Coroutine();
        StartCoroutine(TextShow_Sign);

        if (TextEnter_Sign != null) { StopCoroutine(TextEnter_Sign); }
        TextEnter_Sign = TextEnter_Coroutine();
        StartCoroutine(TextEnter_Sign);
    }

    public void HideText()
    {
        foreach (Transform c in PreTexts)
        {
            GameObject TMP_Obj = c.gameObject;

            UI_Production TMP_Production = TMP_Obj.GetComponent<UI_Production>();
            TMP_Production.Alpha("Lerp", Text_Speed, 1f, 0f);
        }
    }

    public void DestoryText()
    {
        if (TextShow_Sign != null) { StopCoroutine(TextShow_Sign); }
        if (TextEnter_Sign != null) { StopCoroutine(TextEnter_Sign); }

        PreTexts = new List<Transform>();

        foreach (Transform child in TextsContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    [Header("플레이어 상호작용 요소")]
    [SerializeField] private int Skip_Click = 0;

    [Header("텍스트 관련")]
    [SerializeField] private TMP_FontAsset Text_Font;
    [SerializeField] private float Text_Speed = 0.5f;
    [SerializeField] private float Text_Wait = 0.5f;
    [SerializeField] private GameObject TextPrefab;

    [Header("클래스 로컬 요소")]
    [SerializeField] private List<Transform> PreTexts = new List<Transform>();
    [SerializeField] private Transform TextsContainer;
    private IEnumerator TextShow_Sign;
    private IEnumerator TextEnter_Sign;

    private bool AllExported = true;

    public void TextDivide(string Sentence)
    {
        foreach (char c in Sentence)
        {
            GameObject TMP_Obj = Instantiate(TextPrefab, Vector2.zero, Quaternion.identity, TextsContainer.gameObject.transform);
            TMP_Obj.name = c.ToString();

            Transform TMP_Transform = TMP_Obj.transform;
            PreTexts.Add(TMP_Transform);

            TMP_Text TMP_Text = TMP_Obj.GetComponent<TMP_Text>();
            TMP_Text.text = c.ToString();
            TMP_Text.font = Text_Font;
        }
        Debug.Log("TextsMaker : 텍스트 분리 완료.");
    }

    private void TextAlign(List<Transform> PreTexts)
    {
        Vector2 TotalSizeDelta = new Vector2(0f, 100f);

        foreach (Transform c in PreTexts)
        {
            GameObject TMP_Obj = c.gameObject;

            RectTransform TMP_Rect = TMP_Obj.GetComponent<RectTransform>();
            TMP_Text TMP_Text = TMP_Obj.GetComponent<TMP_Text>();

            if (char.IsLetterOrDigit(TMP_Text.text[0]) == false || TMP_Text.text.Any(char.IsWhiteSpace)) { TMP_Rect.sizeDelta = new Vector2(50f, 100f); }
            else { TMP_Rect.sizeDelta = new Vector2(75f, 100f); }

            TMP_Rect.anchoredPosition = new Vector2(TotalSizeDelta.x + TMP_Rect.sizeDelta.x, 0f);

            TotalSizeDelta += new Vector2(TMP_Rect.sizeDelta.x, 0f);
        }

        RectTransform TextsContainer_Rect = TextsContainer.GetComponent<RectTransform>();
        TextsContainer_Rect.sizeDelta = TotalSizeDelta;
        Debug.Log(TotalSizeDelta);


        float Rect_X = 0f;
        foreach (Transform c in PreTexts)
        {
            GameObject TMP_Obj = c.gameObject;

            RectTransform TMP_Rect = TMP_Obj.GetComponent<RectTransform>();
            TMP_Rect.anchoredPosition = new Vector2(Rect_X, 0f);
            Rect_X += TMP_Rect.sizeDelta.x;
        }
    }

    private IEnumerator ShowText_Coroutine()
    {
        foreach (Transform c in TextsContainer.gameObject.transform)
        {
            GameObject TMP_Obj = c.gameObject;

            RectTransform TMP_Rect = TMP_Obj.GetComponent<RectTransform>();
            TMP_Text TMP_Text = TMP_Obj.GetComponent<TMP_Text>();
            UI_Production TMP_Production = TMP_Obj.GetComponent<UI_Production>();

            if (char.IsLetterOrDigit(TMP_Text.text[0]) == false || TMP_Text.text.Any(char.IsWhiteSpace))
            {
                TMP_Production.Alpha("Lerp", Text_Speed / 2, 0f, 1f);
                yield return new WaitForSeconds(Text_Wait / 2);
            }
            else
            {
                TMP_Production.Alpha("Lerp", Text_Speed, 0f, 1f);
                yield return new WaitForSeconds(Text_Wait);
            }
        }

        AllExported = true;
    }

    private IEnumerator TextEnter_Coroutine()
    {
        bool EnterCount = true;
        while (EnterCount)
        {
            while (Input.GetMouseButtonDown(0))
            {
                if (AllExported == false)
                {
                    if (TextShow_Sign != null) { StopCoroutine(TextShow_Sign); }
                    foreach (Transform c in PreTexts)
                    {
                        GameObject TMP_Obj = c.gameObject;

                        UI_Production TMP_Production = TMP_Obj.GetComponent<UI_Production>();
                        TMP_Production.Alpha("Instant", 0f, 0f, 1f);
                    }
                    AllExported = true;
                }
                else
                {
                    AllExported = false;
                    EnterCount = false;
                    HideText();
                    break;
                }
                yield return null;
            }
            yield return null;
        }
    }
}
