using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueParser : MonoBehaviour
{
    private Dictionary<string, string[]> allAnswers = new Dictionary<string, string[]>();
    public QuestionManager QuestionManager;
    public GameObject panel_ink;
    public GameObject panel_client;
    public TMP_Text targetText_ink; 
    public TMP_Text targetText_client;
    private float delay = 0.095f; //글자가 움직이는 속도

    public void AnswerToQuestion(string key) {
        StartCoroutine(ShowDialogueByPrefix(key));
    }


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

      
        // StartCoroutine(ShowDialogueByPrefix("story1_0_0_0")); // dialogIndex 값을 불러오는 장치(동시에 글씨 생성 장치) // 임시 값 설정
    }

    IEnumerator ShowDialogueByPrefix(string prefix)
    {
        List<string> keys = GetDialogueIndexesByPrefix(prefix);
        foreach (string key in keys)
        {
            yield return StartCoroutine(ShowDialogueByIndex(key));
            yield return new WaitForSeconds(0.5f); // 시간 지연

            //내 생각엔 여기다가 그림 바꿀 수 있도록 하면 좋을 듯??
        }

        new WaitForSeconds(2f);
        Debug.Log("모든 대사 출력 완료."); 
        StopAllCoroutines(); //QuestionManager에게 끝났음을 알림
        panel_ink.SetActive(false);
        panel_client.SetActive(false);
        targetText_ink.text = "";
        targetText_client.text = "";
        QuestionManager.allOn();
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

        result.Sort(); // _1, _2, _3 순서대로 출력
        return result;
    }

    IEnumerator ShowDialogueByIndex(string index) //인덱스의 위치에 따라 화자 설정
    {
        if (!allAnswers.ContainsKey(index))
            yield break;

        string speaker = allAnswers[index][0];
        string line = allAnswers[index][1];

        // 먼저 모든 대사 패널 숨기기
        HideAllPanels();
        TMP_Text target = null;

        if (speaker.Contains("잉크 헤드"))
        {
            target = targetText_ink;
        }
        else if (speaker.Contains("의뢰인"))
        {
            target = targetText_client;
        }

        yield return StartCoroutine(TextPrintToTarget(target, line));
    }

    IEnumerator TextPrintToTarget(TMP_Text target, string text) //텍스트 타이핑
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

    private void LoadCSV() //로드 및 저장 
    {
        var csvData = CSVReader.Read("dialogue"); // Resources/dialogue.csv

        foreach (var row in csvData)
        {
            if (!row.ContainsKey("ID") || !row.ContainsKey("화자") || !row.ContainsKey("대사"))
                continue;

            string index = row["ID"].ToString().Trim();
            string speaker = row["화자"].ToString().Trim();
            string dialogue = row["대사"].ToString().Trim();

            if (!allAnswers.ContainsKey(index))
                allAnswers.Add(index, new string[] { speaker, dialogue });
        }
        /*TextAsset csvFile = Resources.Load<TextAsset>("dialogue");
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
        }*/
    }

    /*private List<string> ParseCsvLine(string line) //따옴표와 쉼표 검열
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
    }*/
}