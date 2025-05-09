using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Coroutine hoverCoroutine;
    private static GameObject currentPreview;

    public GameObject previewPrefab; // 확대 표시할 프리팹 (Image 포함된 오브젝트)

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
        RemovePreview(); // 기존 프리뷰 제거

        // 복제 생성
        currentPreview = Instantiate(previewPrefab, FindObjectOfType<Canvas>().transform);
        RectTransform rt = currentPreview.GetComponent<RectTransform>();

        //  캔버스 중앙 정렬
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f); // 중심 기준
        rt.anchoredPosition = Vector2.zero;

        //  원하는 크기 (5배 확대)
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
