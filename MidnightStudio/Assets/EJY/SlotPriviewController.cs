using UnityEngine;
using UnityEngine.UI;

public class SlotPriviewController : MonoBehaviour
{
    public GameObject[] slotObjects; // ���� ������Ʈ�� (1~5)
    public Canvas targetCanvas;
    public QuestionManager QuestionManager;

    private GameObject currentPreview;
    private int currentPreviewIndex = -1;
    private bool isPreviewLocked = false;

    void Update()
    {
        for (int i = 0; i < slotObjects.Length; i++)
        {
            KeyCode key = KeyCode.Alpha1 + i;

            if (Input.GetKeyDown(key) && !isPreviewLocked && QuestionManager.canClick)
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
            Debug.LogWarning("Image ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        sourceImage.raycastTarget = false;
        GameObject imageObj = sourceImage.gameObject;

        currentPreview = Instantiate(imageObj);
        currentPreview.transform.SetParent(targetCanvas.transform, false);

        Button[] buttons = currentPreview.GetComponentsInChildren<Button>(true);
        foreach (Button b in buttons)
        {
            Destroy(b); //  ��ư Ŭ�� ��� ���� ����
        }

        RectTransform rt = currentPreview.GetComponent<RectTransform>();

        // ��ġ ���� �� ����
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;
        rt.localPosition = Vector3.zero; //  ����
        rt.localScale = Vector3.one * 10f;
        rt.sizeDelta = sourceImage.rectTransform.sizeDelta; //  ũ�� ����

        // Ŭ�� ���� ����
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
