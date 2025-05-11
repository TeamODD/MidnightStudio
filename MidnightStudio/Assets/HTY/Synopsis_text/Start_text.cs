using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Start_text : MonoBehaviour
{

    public SynopsisManager manager;
    public float StartTextDelay = 0.05f;
    public TMP_Text startText;
    public SpriteRenderer Client;
    public SpriteRenderer InkHead;
    public SynopsisColorChange synopsisColorManager;


    private Dictionary<string, string[]> dialogues = new Dictionary<string, string[]>();
    private Coroutine typingCoroutine;

    // 1 대사 불러오기 -> 텍스트 타이핑 띄우고 -> 대기 상태, 엔터로만 넘어가게 -> 엔터 누르면 다음 대사 <반복 끝날 때까지> -> 마지막 디버그 로그(manager.EnableProceed())

    public void Start()
    {
        SynopsisLoadCSV("story1_intro");
        StartCoroutine(StartTextIndex("story1_intro"));

    }

    IEnumerator StartTextIndex(string prefix)
    {
        List<string> keys = StartTextList(prefix);
        bool isFirst = true;

        foreach (string key in keys)
        {
            if (!isFirst)
            {
                // 첫 번째 이후는 엔터 입력 대기
                yield return StartCoroutine(WaitForEnterKey());
            }

            // 텍스트 출력
            yield return StartCoroutine(StartTextTyping(key));

            isFirst = false;
        }
        manager.EnableProceed();
    }

    IEnumerator WaitForEnterKey()
    {
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
    }

    private List<string> StartTextList(string prefix) 
    {
        List<string> dialogList = new List<string>();
        foreach (var key in dialogues.Keys)
        {
            dialogList.Add(key);
        }
        return dialogList;
    }

    IEnumerator StartTextTyping(string index) //인덱스의 위치에 따라 화자 설정
    {
        if (!dialogues.ContainsKey(index))
            yield break;

        string speaker = dialogues[index][0];
        string line = dialogues[index][1];
        string act = dialogues[index][2];

        TMP_Text target = null;
        Debug.Log("실행");
        if (speaker.Contains("ink"))
        {
            target = startText;
            ApplyActImage(speaker, act);
            synopsisColorManager.SetCharacterColorWhite();
        }

        else if (speaker.Contains("client"))
        {
            target = startText;
            ApplyActImage(speaker, act);
        }
        Debug.Log(index);
        typingCoroutine = StartCoroutine(TextTyping(target, line));

        // 타이핑 도중에 엔터키가 눌리면 강제로 코루틴 종료하고 다음으로 넘어감
        while (typingCoroutine != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
                break;
            }
            yield return null;
        }
    }

    public void ApplyActImage(string speaker, string act)
    {

        Sprite actSprite = LoadActSprite(speaker, act);
        if (actSprite == null)
        {
            Debug.LogError($"[에러] '{act}'에 해당하는 이미지를 찾을 수 없습니다. 경로를 다시 확인하세요.");
            return;
        }

        if (speaker.Contains("ink"))
        {
            InkHead.sprite = actSprite;

        }
        else if (speaker.Contains("client"))
        {
            Client.sprite = actSprite;

        }
    }

    private Sprite LoadActSprite(string speaker, string act)
    {
        string basePath = "";
        if (speaker.Contains("ink"))
        {
            basePath = "Ink_Character/";
        }
        else if (speaker.Contains("client"))
        {
            basePath = "Client_Character/";
        }
        else
        {
            Debug.LogWarning("알 수 없는 화자입니다.");
            return null;
        }

        string fullPath = basePath + act;

        Sprite sprite = Resources.Load<Sprite>(fullPath);

        if (sprite == null)
        {
            Debug.LogError($"[로드 실패] Sprite 경로: Resources/{fullPath}.png 또는 .jpg 가 존재하지 않음.");
        }

        return sprite;
    }

    IEnumerator TextTyping(TMP_Text target, string text) {
        target.text = "";
        foreach (char c in text)
        {
            target.text += c;
            yield return new WaitForSeconds(StartTextDelay);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private void SynopsisLoadCSV(string story) //로드 및 저장 
    {
        var csvData = CSVReader.Read(story); // Resources/dialogue.csv // 임시 수정-story1_0_3_0
        //story1_0_0_0
        foreach (var row in csvData)
        {
            if (!row.ContainsKey("keys") || !row.ContainsKey("speaker") || !row.ContainsKey("act") || !row.ContainsKey("dialogue"))
                continue;

            string index = row["keys"].ToString().Trim();
            string speaker = row["speaker"].ToString().Trim();
            string animation = row["act"].ToString().Trim();
            string dialogue = row["dialogue"].ToString().Trim();
           
            if (!dialogues.ContainsKey(index))
                dialogues.Add(index, new string[] { speaker, dialogue, animation});
        }
    }
}
