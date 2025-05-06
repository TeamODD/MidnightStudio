using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public TextBackground[] background = new TextBackground[3];
    private bool canClick = true;
    public DialogueParser parser;
    public GameObject reroll;
    public List<GameObject> questionBox = new List<GameObject>();
    private string sceneIndex;
    public List<Image> scene = new List<Image>();
    public static List<TMP_Text> questionText = new List<TMP_Text>();
    public static string dialogIndex;
    private List<Dictionary<string, object>> QuestionDictionary;
    // 씬마다 선택된 질문들을 저장
    private Dictionary<string, List<string>> sceneToQuestions = new Dictionary<string, List<string>>();
    private List<Coroutine> activeCoroutines = new List<Coroutine>();

    
    // 현재 세팅된 질문들을 추적하는 리스트
    private List<string> selectedKeys = new List<string>();

    void Awake()
    {
        QuestionDictionary = CSVReader.Read("story1_question");
        for (var i = 0; i < QuestionDictionary.Count; i++)
        {
            Debug.Log("key:" + QuestionDictionary[i]["key"] + " " +
                   "question:" + QuestionDictionary[i]["question"] + " " +
                   "dialogTrue:" + QuestionDictionary[i]["dialogTrue"] + " " +
                   "dialogFalse:" + QuestionDictionary[i]["dialogFalse"]
                   );

        }
    }

    void questionMaker()
    {
        // 기존 질문이 저장되어 있으면 그걸 사용
        if (sceneToQuestions.ContainsKey(sceneIndex))
        {
            selectedKeys = sceneToQuestions[sceneIndex];
        }
        else
        {
            List<string> validKeys = new List<string>();

            // sceneIndex에 맞는 key들을 찾는다.
            for (int i = 0; i < QuestionDictionary.Count; i++)
            {
                string key = QuestionDictionary[i]["key"].ToString();
                if (key.StartsWith($"story1_question_{sceneIndex}_"))
                {
                    validKeys.Add(key);
                }
            }

            selectedKeys = new List<string>();

            // 3개의 랜덤 질문을 뽑기
            while (selectedKeys.Count < 3 && validKeys.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validKeys.Count);
                string randomKey = validKeys[randomIndex];

                if (!selectedKeys.Contains(randomKey))
                {
                    selectedKeys.Add(randomKey);
                }
            }

            // 이 씬에 대해 저장해둠
            sceneToQuestions[sceneIndex] = new List<string>(selectedKeys);
        }

        // UI에 표시
        for (int i = 0; i < selectedKeys.Count; i++)
        {
            Dictionary<string, object> questionData = QuestionDictionary.Find(entry => entry["key"].ToString() == selectedKeys[i]);
            if (questionData != null)
            {
                questionText[i].text = questionData["question"].ToString();
            }
        }
        questionShow();
    }


    void Start()
    {
        sceneIndex = "0";
        questionMaker();
    }

    public void reload()
    {
        // 현재 sceneIndex에 해당하는 저장된 질문 삭제
        if (sceneToQuestions.ContainsKey(sceneIndex))
        {
            sceneToQuestions.Remove(sceneIndex);
        }

        questionMaker();
    }


    public void sceneIndexChange(string sceneIndex)
    {
        if (this.sceneIndex == sceneIndex) return;
        this.sceneIndex = sceneIndex;
        questionMaker();
    }
    public void OnQuestionClicked(int index)
    {
        if(canClick)
        {
            nextIndex(index);  // dialogTrue 설정
            textOff();         // 모든 질문 비활성화
            sceneOff(int.Parse(sceneIndex));
            questionBoxOff();
            rerollOff();
            Debug.Log("대화시스템");
            Debug.Log(dialogIndex);
            if (parser == null)
            {
                Debug.LogError("DialogueParser가 할당되지 않았습니다! Inspector에서 연결해 주세요.");
                return;
            }
            canClick = false;
            questionHide();
            parser.AnswerToQuestion(dialogIndex);

        }

    }
    public void nextIndex(int index)
    {
        if (index < 0 || index >= selectedKeys.Count)
        {
            Debug.LogError("Invalid question index clicked.");
            return;
        }

        string clickedKey = selectedKeys[index];
        Dictionary<string, object> questionData = QuestionDictionary.Find(entry => entry["key"].ToString() == clickedKey);

        if (questionData != null)
        {
            dialogIndex = questionData["dialogTrue"].ToString();

            Debug.Log("Clicked question's dialogTrue: " + dialogIndex);

            // 👉 DialogManager 같은 곳에 전달
            // 예: DialogManager.Instance.Show(dialogTrue);
        }
    }

    public void sceneOff(int selectedIndex)
    {
        for (int i = 0; i < scene.Count; i++)
        {
            // selectedIndex에 해당하는 씬만 제외하고 비활성화
            if (i != selectedIndex && scene[i] != null)
            {
                scene[i].gameObject.SetActive(false);
            }
        }
    }

    public void rerollOff() {
        reroll.SetActive(false);
    }

    public void questionBoxOff() {
        foreach (GameObject obj in questionBox)
        {
            obj.SetActive(false);
        }
    }
    public void textOff()
    {
        foreach (TMP_Text text in questionText)
        {
            text.color = Color.black; // 텍스트 색상을 검은색으로 변경
            text.gameObject.SetActive(false);
            
        }
    }
    
    public void allOn() {
        foreach (TMP_Text text in questionText)
        {
            text.gameObject.SetActive(true);
        }
        reroll.SetActive(true);
        for (int i = 0; i < scene.Count; i++)
        {
            scene[i].gameObject.SetActive(true);
        }
        foreach (GameObject obj in questionBox)
        {
            obj.SetActive(true);
        }
        canClick = true;
    }
    public void questionHide() 
    {
        for(int i = 0; i < 3; i++)
        {
            background[i].hide();
        }
    }
    public void questionShow() 
    {
        for(int i = 0; i < 3; i++)
        {
            background[i].show();
        }
    }
}
