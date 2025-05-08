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
        /*foreach (string clue in clueList)
        {
            if (!displayedClues.Contains(clue))
            {
                AddClueToScroll(clue);
                displayedClues.Add(clue);
            }
        }*/
        foreach (string clue in clueList)
        {
            if (!displayedClues.Contains(clue))
            {
                GameObject newItem = Instantiate(clueItemPrefab, contentParent);
                TMP_Text clueLabel = newItem.GetComponent<TMP_Text>();
                if (clueLabel != null)
                {
                    clueLabel.text = clue;
                    clueLabel.maxVisibleCharacters = clue.Length; // �� ���� ���� ���̵���
                    displayedClues.Add(clue);
                }
                else
                {
                    Debug.LogWarning("Clue item prefab�� TMP_Text ������Ʈ�� �����ϴ�.");
                }
            }
        }
    }

    public void AddClueToScroll(string clueText)
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
    public void ClearClueList()
    {
        displayedClues.Clear();
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator TypeText(TMP_Text label, string fullText)
    {
        label.text = fullText;
        label.maxVisibleCharacters = 0;

        for (int i = 0; i <= fullText.Length; i++)
        {
            label.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.09f); // �ӵ� ���� (0.03�ʸ��� �� ����)
        }
    }
}
