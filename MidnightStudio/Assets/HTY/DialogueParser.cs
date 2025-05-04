using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueParser : MonoBehaviour
{
    private Dictionary<string, string[]> allAnswers = new Dictionary<string, string[]>();
    public GameObject panel_ink;
    public GameObject panel_client;
    public TMP_Text targetText_ink; 
    public TMP_Text targetText_client;
    private float delay = 0.095f; //���ڰ� �����̴� �ӵ�
    public IEnumerator delayQuestion(TMP_Text target, string text)
    {
        target.text = "";
        foreach (char c in text)
        {
            target.text += c;
            yield return new WaitForSeconds(delay);
        }
    }


    private void HideAllPanels()
    {
        panel_ink.SetActive(false);
        panel_client.SetActive(false);
    }

    void Start()
    {
        LoadCSV();

      
        StartCoroutine(ShowDialogueByPrefix("story1_0_0_0")); // dialogIndex ���� �ҷ����� ��ġ(���ÿ� �۾� ���� ��ġ) // �ӽ� �� ����
    }

    IEnumerator ShowDialogueByPrefix(string prefix)
    {
        List<string> keys = GetDialogueIndexesByPrefix(prefix);
        foreach (string key in keys)
        {
            yield return StartCoroutine(ShowDialogueByIndex(key));
            yield return new WaitForSeconds(0.5f); // �ð� ����

            //�� ������ ����ٰ� �׸� �ٲ� �� �ֵ��� �ϸ� ���� ��??
        }

        new WaitForSeconds(2f);
        Debug.Log("��� ��� ��� �Ϸ�."); 
        targetText_ink.enabled = false;
    }

    private List<string> GetDialogueIndexesByPrefix(string prefix)
    {
        List<string> result = new List<string>();
        foreach (var key in allAnswers.Keys)
        {
            if (key.StartsWith(prefix + "_")) // "story1_0_0_0_"
            {
                result.Add(key);
            }
        }

        result.Sort(); // _1, _2, _3 ������� ���
        return result;
    }

    IEnumerator ShowDialogueByIndex(string index) //�ε����� ��ġ�� ���� ȭ�� ����
    {
        if (!allAnswers.ContainsKey(index))
            yield break;

        string speaker = allAnswers[index][0];
        string line = allAnswers[index][1];

        // ���� ��� ��� �г� �����
        HideAllPanels();
        TMP_Text target = null;

        if (speaker.Contains("��ũ ���"))
        {
            target = targetText_ink;
        }
        else if (speaker.Contains("�Ƿ���"))
        {
            target = targetText_client;
        }

        yield return StartCoroutine(TextPrintToTarget(target, line));
    }

    IEnumerator TextPrintToTarget(TMP_Text target, string text) //�ؽ�Ʈ Ÿ����
    {
        panel_ink.SetActive(true);
        panel_client.SetActive(true);
        target.text = "";
        foreach (char c in text)
        {
            target.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    private void LoadCSV() //�ε� �� ���� 
    {
        TextAsset csvFile = Resources.Load<TextAsset>("dialogue");
        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            List<string> parts = ParseCsvLine(line);
            if (parts.Count < 4) continue;

            string index = parts[0].Trim();
            string speaker = parts[1].Trim();
            string dialogue = parts[3].Trim();

            if (!allAnswers.ContainsKey(index))
                allAnswers.Add(index, new string[] { speaker, dialogue });
        }
    }

    private List<string> ParseCsvLine(string line) //����ǥ�� ��ǥ �˿�
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"') inQuotes = !inQuotes;
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.Trim());
                current = "";
            }
            else current += c;
        }

        result.Add(current.Trim());
        return result;
    }
}