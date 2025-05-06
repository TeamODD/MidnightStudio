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
        GameObject newItem = Instantiate(clueItemPrefab, contentParent);
        TMP_Text clueLabel = newItem.GetComponent<TMP_Text>();
        if (clueLabel != null)
        {
            clueLabel.text = clueText;
        }
        else
        {
            Debug.LogWarning("Clue item prefab�� TMP_Text ������Ʈ�� �����ϴ�.");
        }
    }
}
