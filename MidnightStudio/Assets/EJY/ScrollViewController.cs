using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    public GameObject clueItemPrefab;      // 텍스트 항목 프리팹 (Text or TMP_Text 포함)
    public Transform contentParent;        // Scroll View의 Content 오브젝트

    private HashSet<string> displayedClues = new HashSet<string>(); // 중복 방지

    public void UpdateClueList(List<string> clueList)
    {
        foreach (string clue in clueList)
        {
            if (!displayedClues.Contains(clue))
            {
                AddClueToScroll(clue);
                displayedClues.Add(clue);
            }
        }
    }

    private void AddClueToScroll(string clueText)
    {
        if (displayedClues.Contains(clueText)) return;

        GameObject newItem = Instantiate(clueItemPrefab, contentParent);
        TMP_Text clueLabel = newItem.GetComponent<TMP_Text>();
        if (clueLabel != null)
        {
            StartCoroutine(TypeText(clueLabel, clueText));
            displayedClues.Add(clueText); // 중복 방지 등록
        }
        else
        {
            Debug.LogWarning("Clue item prefab에 TMP_Text 컴포넌트가 없습니다.");
        }
    }

    private IEnumerator TypeText(TMP_Text label, string fullText)
    {
        label.text = fullText;
        label.maxVisibleCharacters = 0;

        for (int i = 0; i <= fullText.Length; i++)
        {
            label.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.03f); // 속도 조절 (0.03초마다 한 글자)
        }
    }
}
