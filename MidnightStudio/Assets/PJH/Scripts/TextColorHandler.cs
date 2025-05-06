using TMPro;
using UnityEngine;
using UnityEngine.UI; // Image 타입을 사용하기 위해 이 줄을 추가합니다.
using UnityEngine.EventSystems;

public class TextColorHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextColorManager manager;
    public TMP_Text tmpText;
    public Image image;
    void Awake()
    {
        if (tmpText == null) tmpText = GetComponent<TMP_Text>();
        if (image == null) image = GetComponent<Image>();
        Debug.Log(tmpText);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log(tmpText);
        manager.OnTextHovered(tmpText, image);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manager.OnTextUnhovered();
    }
}
