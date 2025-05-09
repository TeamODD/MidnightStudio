using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Coroutine hoverCoroutine;
    private static GameObject currentPreview;

    public GameObject previewPrefab; // Ȯ�� ǥ���� ������ (Image ���Ե� ������Ʈ)

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverCoroutine = StartCoroutine(DelayedShowPreview());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);

        RemovePreview();
    }

    private IEnumerator DelayedShowPreview()
    {
        yield return new WaitForSeconds(2f);

        ShowPreview();
    }

    private void ShowPreview()
    {
        RemovePreview(); // ���� ������ ����

        // ���� ����
        currentPreview = Instantiate(previewPrefab, FindObjectOfType<Canvas>().transform);
        RectTransform rt = currentPreview.GetComponent<RectTransform>();

        //  ĵ���� �߾� ����
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f); // �߽� ����
        rt.anchoredPosition = Vector2.zero;

        //  ���ϴ� ũ�� (5�� Ȯ��)
        rt.localScale = Vector3.one * 5f;
    }

    private void RemovePreview()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }
}
