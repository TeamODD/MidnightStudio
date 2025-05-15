using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MainESCControl : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject Slate;
    public RectTransform optionRect;
    public RectTransform optionImg;
    public GaugeControl gauge;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    public SceneFader sceneFader;

    public float slideDuration = 1f;

    public Option_Production Option_Production;
    private bool isOptionOpen = false;

    private AudioManager AudioPlayer;
    public AudioClip[] Clips;
    public QuestionManager QuestionManager;
    private bool FUCKINGWAIT = false;

    void Awake()
    {
        optionRect = optionPanel.GetComponent<RectTransform>();

        // EventSystem�� ����Ǿ� ���� ������ �ڵ� �Ҵ�
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                Debug.LogWarning("EventSystem�� ���� �������� �ʽ��ϴ�. �ڵ� �����մϴ�.");
                GameObject es = new GameObject("EventSystem");
                eventSystem = es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
            }
        }
    }
    void Start()
    {
        AudioPlayer = AudioManager.Instance;
        optionPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isOptionOpen && QuestionManager.IsProductioning == false) OpenOptionPanel();
            Debug.Log("작동함");
        }
    }

    void OpenOptionPanel()
    {
        Slate.SetActive(true);
        Option_Production.Production_Start();
        StartCoroutine(OnOptionClicked_ObjActive());
        Debug.Log("작동됨");
        AudioPlayer.PlaySE(Clips[0]);
    }

    private IEnumerator OnOptionClicked_ObjActive()
    {
        isOptionOpen = true;
        yield return new WaitForSeconds(0.5f);

        optionPanel.SetActive(true);
        yield return new WaitForSeconds(0.45f);

        Slate.SetActive(false);
    }

    public void CloseOptionPanel()
    {
        if (isOptionOpen && FUCKINGWAIT == false && QuestionManager.IsProductioning == false)
        {
            FUCKINGWAIT = true;
            Slate.SetActive(true);
            Option_Production.Production_Start();
            StartCoroutine(OnOptionClicked_ObjDisactive());
            Debug.Log("작동됨");
            AudioPlayer.PlaySE(Clips[0]);
        }
    }
    private IEnumerator OnOptionClicked_ObjDisactive()
    {
        yield return new WaitForSeconds(0.5f);
        optionPanel.SetActive(false);

        yield return new WaitForSeconds(0.45f);
        Slate.SetActive(false);

        optionPanel.SetActive(false);
        gauge.StartGauge();
        yield return null;

        isOptionOpen = false;
        FUCKINGWAIT = false;
    }

    private IEnumerator Slide(RectTransform rect, Vector2 target)
    {
        Vector2 start = rect.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            rect.anchoredPosition = Vector2.Lerp(start, target, elapsed / slideDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.anchoredPosition = target;
    }

    private IEnumerator SlideAndDeactivate(RectTransform rect, Vector2 target, System.Action onComplete)
    {
        yield return Slide(rect, target);
        onComplete?.Invoke();
    }

    private bool IsPointerOverUI(RectTransform targetRect)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.transform.IsChildOf(targetRect))
                return true;
        }

        return false;
    }
}
