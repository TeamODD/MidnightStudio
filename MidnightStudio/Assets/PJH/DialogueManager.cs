using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // UI 텍스트를 사용하기 위해 추가


// 대사 정보 저장 클래스
public class DialogueEntry
{
    public string id;
    public string speaker;
    public string animation;
    public string dialogue;
    public string hint;

    public DialogueEntry(string id, string speaker, string animation, string dialogue, string hint)
    {
        this.id = id;
        this.speaker = speaker;
        this.animation = animation;
        this.dialogue = dialogue;
        this.hint = hint;
    }
}

public class DialogueManager : MonoBehaviour
{
    private List<DialogueEntry> allDialogues = new List<DialogueEntry>();
    public TMP_Text dialogueText;  // 대사를 출력할 Text UI 요소 (Editor에서 연결)
    public float dialogueSpeed = 0.1f;  // 대사 출력 속도

    void Start()
    {
        LoadCSV();
    }

    // CSV 로드
    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("dialogue");  // dialogue.csv 파일 로드
        if (csvFile == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다!");
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 4) continue;

            string id = parts[0].Trim();
            string speaker = parts[1].Trim();
            string animation = parts[2].Trim();
            string dialogue = parts[3].Trim();
            string hint = parts.Length >= 5 ? parts[4].Trim() : "";

            DialogueEntry entry = new DialogueEntry(id, speaker, animation, dialogue, hint);
            allDialogues.Add(entry);
        }

        Debug.Log("CSV 로딩 완료: " + allDialogues.Count + "개의 대사");
    }

    // 대사를 ID로 출력하는 함수
    public void PlayDialogueByPrefix(string idPrefix)
    {
        StopAllCoroutines();  // 이전 코루틴 멈추기
        StartCoroutine(PlayDialogueCoroutine(idPrefix));  // 대사 출력 코루틴 시작
    }

    // 대사 출력
    public void OnClick_PlayDialogue(string questionId)
    {
        PlayDialogueByPrefix(questionId);  // questionId에 해당하는 대사 출력
    }

    IEnumerator PlayDialogueCoroutine(string idPrefix)
    {
        List<DialogueEntry> filtered = allDialogues.FindAll(entry => entry.id.StartsWith(idPrefix));  // ID로 필터링
        filtered.Sort((a, b) => a.id.CompareTo(b.id)); // ID 순 정렬

        foreach (var entry in filtered)
        {
            yield return StartCoroutine(ShowDialogue(entry.dialogue));  // 대사 출력 코루틴 호출
            yield return new WaitForSeconds(1f);  // 1초 간격으로 대사 출력
        }
    }

    // 대사 한 글자씩 출력하는 코루틴
    IEnumerator ShowDialogue(string dialogue)
    {
        dialogueText.text = "";  // 텍스트 UI 초기화
        foreach (char letter in dialogue)
        {
            dialogueText.text += letter;  // 한 글자씩 추가
            yield return new WaitForSeconds(dialogueSpeed);  // 출력 속도 조절
        }
    }
}
