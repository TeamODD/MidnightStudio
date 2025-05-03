using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using TreeEditor;

public class QuestionEntry {
    public string index;
    public string question;
    public QuestionEntry(string index, string question)
    {
        this.index = index;
        this.question = question;
    }
}
public class QuestionManager : MonoBehaviour
{
    private List<QuestionEntry> allQuestions = new List<QuestionEntry>();
    public List<TMP_Text> dialogueText = new List<TMP_Text>();
    void Start()
    {
        LoadCSV();
    }

    private void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("story1_question");
        if (csvFile == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다!");
            return;
        }
        string[] lines = csvFile.text.Split('\n');
        foreach (string line in lines) {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] parts = line.Split(',');
            string index = parts[0].Trim();
            string question = parts[1].Trim();
            QuestionEntry entry = new QuestionEntry(index, question);
            allQuestions.Add(entry);
        }
        int questionCount = allQuestions.Count;
        for (int i = 0; i < 3; i++) {
            int randomIndex = UnityEngine.Random.Range(0, questionCount);
            dialogueText[i].text = allQuestions[randomIndex].question;
        }

    }
    
}
