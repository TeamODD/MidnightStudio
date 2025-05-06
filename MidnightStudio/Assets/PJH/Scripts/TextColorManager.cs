using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextColorManager : MonoBehaviour
{
    // 이 리스트에 TextColorHandler가 적용된 모든 UI 요소(텍스트-이미지 쌍)를 할당해주세요.
    public List<TextColorHandler> textHandlers;

    void Start()
    {
        if (textHandlers == null)
        {
            textHandlers = new List<TextColorHandler>();
        }

        // 모든 관리 대상 UI 요소들의 초기 상태 설정
        foreach (var textHandler in textHandlers)
        {
            if (textHandler == null) continue;

            textHandler.manager = this;
            if (textHandler.tmpText != null)
            {
                textHandler.tmpText.color = Color.white; // 텍스트 초기 색상
            }
            if (textHandler.image != null)
            {
                textHandler.image.color = Color.white; // 연결된 이미지 초기 색상
            }
        }
    }

    // 텍스트 요소 위에 마우스가 올라왔을 때 호출될 메서드
    // hoveredTextComponent: 마우스가 올라간 TMP_Text 컴포넌트
    // associatedImageComponent: hoveredTextComponent와 쌍을 이루는 Image 컴포넌트 (TextColorHandler에서 전달됨)
    public void OnTextHovered(TMP_Text hoveredTextComponent, Image associatedImageComponent)
    {
        foreach (var textHandler in textHandlers)
        {
            if (textHandler == null) continue;

            bool isHoveredHandler = (textHandler.tmpText == hoveredTextComponent);

            // 텍스트 색상 변경
            if (textHandler.tmpText != null)
            {
                // Debug.Log(textHandler);
                // Debug.Log(isHoveredHandler);
                textHandler.tmpText.color = isHoveredHandler ? Color.white : Color.grey;
            }

            // 연결된 이미지 색상 변경
            if (textHandler.image != null)
            {
                textHandler.image.color = isHoveredHandler ? Color.white : Color.grey;
            }
        }
    }

    // 텍스트 요소에서 마우스가 벗어났을 때 호출될 메서드
    public void OnTextUnhovered()
    {
        foreach (var textHandler in textHandlers)
        {
            if (textHandler == null) continue;

            if (textHandler.tmpText != null)
            {
                textHandler.tmpText.color = Color.white;
            }
            if (textHandler.image != null)
            {
                textHandler.image.color = Color.white;
            }
        }
    }
}
