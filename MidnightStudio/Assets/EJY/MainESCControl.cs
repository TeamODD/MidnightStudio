using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MainESCControl : MonoBehaviour
{
    public GameObject optionPanel;
    public RectTransform optionRect;
    public RectTransform optionImg;
    public GaugeControl gauge;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    public SceneFader sceneFader;

    public float slideDuration = 1f;
    private Vector2 onScreenPosition = Vector2.zero;
    private Vector2 offScreenPosition;

    private bool isOptionOpen = false;

    void Awake()
    {
        optionRect = optionPanel.GetComponent<RectTransform>();
        offScreenPosition = new Vector2(0f, -Screen.height); // 아래쪽에서 시작

        // EventSystem이 연결되어 있지 않으면 자동 할당
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                Debug.LogWarning("EventSystem이 씬에 존재하지 않습니다. 자동 생성합니다.");
                GameObject es = new GameObject("EventSystem");
                eventSystem = es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
            }
        }
    }
    void Start()
    {
        offScreenPosition = new Vector2(0f, -Screen.height);
        optionRect.anchoredPosition = offScreenPosition;
        optionPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isOptionOpen) OpenOptionPanel();
            else CloseOptionPanel();
        }

        if (isOptionOpen && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!IsPointerOverUI(optionImg))
                CloseOptionPanel();
        }
    }

    void OpenOptionPanel()
    {
        isOptionOpen = true;
        sceneFader.FadeToGray(0.4f);
        optionPanel.SetActive(true);
        StartCoroutine(Slide(optionRect, onScreenPosition));
        gauge.PauseGauge();

        if (sceneFader.fadeImage != null)
            sceneFader.fadeImage.raycastTarget = true;
    }

    void CloseOptionPanel()
    {
        isOptionOpen = false;
        sceneFader.FadeOutGray();
        StartCoroutine(SlideAndDeactivate(optionRect, offScreenPosition, () =>
        {
            optionPanel.SetActive(false);
            if (sceneFader.fadeImage != null)
                sceneFader.fadeImage.raycastTarget = false; // 다시 UI 클릭 허용
        }));
        gauge.StartGauge();
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
