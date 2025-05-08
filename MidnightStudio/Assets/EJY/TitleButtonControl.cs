using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonControl : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionPanel;
    public RectTransform optionImg;
    public SceneFader sceneFader;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public float slideDuration = 1f;
    private RectTransform optionRect;
    public Vector2 offScreenPosition = new Vector2(0f, -Screen.height); // �Ʒ��� ȭ�� ��
    public Vector2 onScreenPosition = Vector2.zero; // ���� ��ġ (�߾� ����)


    private void Awake()
    {
        optionRect = optionPanel.GetComponent<RectTransform>();
        offScreenPosition = new Vector2(0f, -Screen.height); // �Ʒ��ʿ��� ����
    }
    private void Update()
    {
        // Option â�� �����ְ� ���콺 ���� Ŭ�� �� �˻�
        if (optionPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUI(optionImg))
            {
                OnExitClosed();
            }
        }
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
            // OptionImg �Ǵ� �� �ڽ��̸� ���� Ŭ������ ����
            if (result.gameObject.transform.IsChildOf(targetRect))
                return true;
        }

        return false; // �ܺ� Ŭ��
    }

    public void OnStartClicked()
    {
        sceneFader.FadeToScene("Synopsis");
    }

    public void OnOptionClicked()
    {
        sceneFader.FadeToGray(0.4f); // 40% �������� ȸ��
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
        optionRect.anchoredPosition = offScreenPosition; // ���� ��ġ
        StartCoroutine(Slide(optionRect, onScreenPosition));
    }

    public void OnExitClosed()
    {
        //optionPanel.SetActive(false);
        //mainPanel.SetActive(true);
        sceneFader.FadeOutGray();
        StartCoroutine(SlideAndDeactivate(optionRect, offScreenPosition, () => {
            optionPanel.SetActive(false);
            mainPanel.SetActive(true);
        }));
    }

    private IEnumerator Slide(RectTransform rect, Vector2 target)
    {
        Vector2 start = rect.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            rect.anchoredPosition = Vector2.Lerp(start, target, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = target;
    }
    private IEnumerator SlideAndDeactivate(RectTransform rect, Vector2 target, System.Action onComplete)
    {
        yield return Slide(rect, target);
        onComplete?.Invoke();
    }

    public void OnQuitClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
