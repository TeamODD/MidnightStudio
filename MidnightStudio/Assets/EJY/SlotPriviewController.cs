using UnityEngine;
using UnityEngine.UI;

public class SlotPriviewController : MonoBehaviour
{
    public GameObject[] slotObjects; // 슬롯 오브젝트들 (1~5)
    public Canvas targetCanvas;

    private GameObject currentPreview;
    private int currentPreviewIndex = -1;
    private bool isPreviewLocked = false;

    void Update()
    {
        for (int i = 0; i < slotObjects.Length; i++)
        {
            KeyCode key = KeyCode.Alpha1 + i;

            if (Input.GetKeyDown(key) && !isPreviewLocked)
            {
                ShowPreview(slotObjects[i]);
                currentPreviewIndex = i;
                isPreviewLocked = true;
            }

            if (isPreviewLocked && currentPreviewIndex == i && Input.GetKeyUp(key))
            {
                RemovePreview();
                currentPreviewIndex = -1;
                isPreviewLocked = false;
            }
        }
    }

    void ShowPreview(GameObject slot)
    {
        RemovePreview();

        Image sourceImage = slot.GetComponentInChildren<Image>();
        if (sourceImage == null)
        {
            Debug.LogWarning("Image 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        sourceImage.raycastTarget = false;
        GameObject imageObj = sourceImage.gameObject;

        currentPreview = Instantiate(imageObj);
        currentPreview.transform.SetParent(targetCanvas.transform, false);

        Button[] buttons = currentPreview.GetComponentsInChildren<Button>(true);
        foreach (Button b in buttons)
        {
            Destroy(b); //  버튼 클릭 기능 완전 제거
        }

        RectTransform rt = currentPreview.GetComponent<RectTransform>();

        // 위치 정렬 및 보정
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.localPosition = Vector3.zero; //  보정
        rt.localScale = Vector3.one * 5f;
        rt.sizeDelta = sourceImage.rectTransform.sizeDelta; //  크기 보정

        // 클릭 차단 방지
        Image previewImage = currentPreview.GetComponent<Image>();
        if (previewImage != null)
            previewImage.raycastTarget = false;
    }

    void RemovePreview()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }
}
