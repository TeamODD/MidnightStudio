using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIStart : MonoBehaviour
{
    [System.Serializable]
    public struct CountdownText
    {
        public string content;
        public Color color;
    }

    public GameObject dontTouch;
    public GameObject background; // 배경 색을 바꿀 오브젝트
    public SpriteRenderer inkHead; // 잉크 헤드
    public SpriteRenderer client;  // 의뢰인
    public TMP_Text tmpText;
    public UI_Production CountDown_Production;
    public CanvasGroup textCanvasGroup;
    public RectTransform textTransform;
    public List<GameObject> remain = new List<GameObject>();
    public GaugeControl gauge;

    public QuestionManager QuestionManager;
    public DialogueParser DialogueParser;
    public Main_hdh_Production Main_Production;

    public CountdownText[] countdownTexts = new CountdownText[3];

    private Color originalInkColor;
    private Color originalClientColor;

    void Start()
    {
        // 카운트다운 텍스트 기본 설정
        countdownTexts[0] = new CountdownText { content = "lights", color = Color.white };
        countdownTexts[1] = new CountdownText { content = "Camera!", color = Color.white };
        countdownTexts[2] = new CountdownText { content = "ACTION!!!", color = Color.white };

        // 원래 색상 저장
        originalInkColor = inkHead.color;
        originalClientColor = client.color;

        MakeTransparentExcept();
        dontTouch.SetActive(true);
        StartCoroutine(FullIntroSequence());
    }

    IEnumerator FullIntroSequence()
    {

        yield return StartCoroutine(FadeSilhouetteAndWhiteBackground());
        yield return StartCoroutine(PlayCountdownSequence());

        // ACTION 이후 화면 잠깐 암전 -> 밝아지면서 캐릭터 복원
        QuestionManager.IntroReset();
        yield return StartCoroutine(FlashBlackTransitionAndRestore());

        // UI 다시 활성화
        EnableUI();
        gauge.StartGauge();

        QuestionManager.IntroProduction();
    }

    IEnumerator PlayCountdownSequence()
    {
        for (int i = 0; i < countdownTexts.Length; i++)
        {
            tmpText.text = countdownTexts[i].content;
            tmpText.color = countdownTexts[i].color;

            yield return StartCoroutine(PlayTextEffect(countdownTexts[i].content));
        }

        tmpText.text = "";
    }

    IEnumerator PlayTextEffect(string Contents)
    {
        Vector3 TotalScale = new Vector3(4f, 4f, 1f);
        switch (Contents)
        {
            case "lights":
                TotalScale = new Vector3(4f, 4f, 1f);
                break;
            case "Camera!":
                TotalScale = new Vector3(5f, 5f, 1f);
                break;
            case "ACTION!!!":
                Main_Production.Clapper_Production_Start();
                TotalScale = new Vector3(6f, 6f, 1f);
                break;
        }
        CountDown_Production.Scale("Lerp", 0.1f, TotalScale, TotalScale - new Vector3(3.1f, 3.1f, 1f));
        CountDown_Production.Alpha("Lerp", 0.1f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);

        CountDown_Production.Scale("Lerp", 0.05f, TotalScale - new Vector3(3.1f, 3.1f, 1f), TotalScale - new Vector3(3f, 3f, 1f));
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.4f);

        if (Contents == "ACTION!!!")
        {
            CountDown_Production.Alpha("Lerp", 0.1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            
            tmpText.gameObject.SetActive(false);
        }

        // float duration = 0.35f;
        // float elapsed = 0f;

        // while (elapsed < duration)
        // {
        //     float t = elapsed / duration;
        //     textTransform.localScale = Vector3.one * (1f + t);
        //     textCanvasGroup.alpha = 1f - t;
        //     elapsed += Time.deltaTime;
        //     yield return null;
        // }

        // textCanvasGroup.alpha = 0f;
        // yield return new WaitForSeconds(0.2f);
    }

    IEnumerator FadeSilhouetteAndWhiteBackground()
    {
        float duration = 1f;
        float elapsed = 0f;
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        bgRenderer.color = Color.black;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            inkHead.color = Color.Lerp(Color.white, Color.black, t);
            client.color = Color.Lerp(Color.white, Color.black, t);
            bgRenderer.color = Color.Lerp(Color.black, Color.white, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bgRenderer.color = Color.white;
        inkHead.color = Color.black;
        client.color = Color.black;
    }

    IEnumerator FlashBlackTransitionAndRestore()
    {
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        float duration = 0.1f;

        // 흰색 → 불투명한 검정 (빠른 암전)
        float elapsed = 0f;
        Color startColor = Color.white;
        Color endColor = new Color(0f, 0f, 0f, 1f); // 검정 + 불투명

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            bgRenderer.color = Color.Lerp(startColor, endColor, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bgRenderer.color = endColor;
        yield return new WaitForSeconds(0.05f);

        // ✅ UI를 바로 켬
        EnableUI();

        // 검정(불투명) → 검정(투명) + 캐릭터 복원
        duration = 0.5f;
        elapsed = 0f;
        startColor = new Color(0f, 0f, 0f, 1f); // 불투명한 검정
        endColor = new Color(0f, 0f, 0f, 0f);   // 완전히 투명한 검정

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            bgRenderer.color = Color.Lerp(startColor, endColor, t);
            inkHead.color = Color.Lerp(Color.black, originalInkColor, t);
            client.color = Color.Lerp(Color.black, originalClientColor, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bgRenderer.color = endColor;
        inkHead.color = originalInkColor;
        client.color = originalClientColor;
    }

    public CanvasGroup[] allGroups1;

    void MakeTransparentExcept()
    {
        CanvasGroup[] allGroups = FindObjectsOfType<CanvasGroup>();

        foreach (CanvasGroup group in allGroups1)
        {
            bool isExcluded = false;

            foreach (GameObject go in remain)
            {
                if (group.gameObject == go || group.transform.IsChildOf(go.transform))
                {
                    isExcluded = true;
                    break;
                }
            }

            if (!isExcluded)
            {
                group.alpha = 0f;
                group.interactable = true;
                group.blocksRaycasts = false;
            }
        }
    }

    void EnableUI()
    {
        CanvasGroup[] allGroups = FindObjectsOfType<CanvasGroup>();
        dontTouch.SetActive(false);
        // 배경을 완전히 투명하게 만듦 (SpriteRenderer 기준)
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        Color bgColor = bgRenderer.color;
        bgColor.a = 0f;
        bgRenderer.color = bgColor;

        foreach (CanvasGroup group in allGroups1)
        {
            bool isExcluded = false;

            foreach (GameObject go in remain)
            {
                if (group.gameObject == go || group.transform.IsChildOf(go.transform))
                {
                    isExcluded = true;
                    break;
                }
            }

            if (!isExcluded)
            {
                group.alpha = 1f;
                group.interactable = true;
                group.blocksRaycasts = true;
            }
        }
    }
}
