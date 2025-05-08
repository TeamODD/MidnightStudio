using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    public GameObject clueItemPrefab;      // �ؽ�Ʈ �׸� ������ (Text or TMP_Text ����)
    public Transform contentParent;        // Scroll View�� Content ������Ʈ

    private HashSet<string> displayedClues = new HashSet<string>(); // �ߺ� ����

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
            displayedClues.Add(clueText); // �ߺ� ���� ���
        }
        else
        {
            Debug.LogWarning("Clue item prefab�� TMP_Text ������Ʈ�� �����ϴ�.");
        }
    }

    private IEnumerator TypeText(TMP_Text label, string fullText)
    {
        label.text = fullText;
        label.maxVisibleCharacters = 0;

        for (int i = 0; i <= fullText.Length; i++)
        {
            label.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.03f); // �ӵ� ���� (0.03�ʸ��� �� ����)
        }
    }
}
