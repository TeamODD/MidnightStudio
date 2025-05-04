using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject reroll;
    public List<GameObject> arrow = new List<GameObject>();
    private string sceneIndex;
    public List<Image> scene = new List<Image>();
    public List<TMP_Text> questionText = new List<TMP_Text>();
    public string dialogIndex;
    private List<Dictionary<string, object>> QuestionDictionary;
    
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
        List<string> validKeys = new List<string>();

        // sceneIndex에 맞는 key들을 찾는다.
        for (int i = 0; i < QuestionDictionary.Count; i++)
        {
            string key = QuestionDictionary[i]["key"].ToString();
            if (key.StartsWith($"story1_question_{sceneIndex}_"))
            {
                validKeys.Add(key);  // 유효한 키를 리스트에 추가
            }
        }

        // 이미 선택된 질문들을 추적하기 위한 리스트 (현재 화면에 표시된 3개만 추적)
        selectedKeys.Clear();  // 이전 질문들을 초기화

        // 3개의 랜덤 질문을 뽑기
        for (int i = 0; i < 3; i++)
        {
            string randomKey;
            // 유효한 질문이 나올 때까지 반복
            do
            {
                int randomIndex = UnityEngine.Random.Range(0, validKeys.Count);
                randomKey = validKeys[randomIndex];
            }
            while (selectedKeys.Contains(randomKey));  // 이미 선택된 질문이면 다시 뽑음

            // randomKey에 해당하는 질문 찾기
            Dictionary<string, object> questionData = QuestionDictionary.Find(entry => entry["key"].ToString() == randomKey);

            if (questionData != null)
            {
                // 해당 질문을 UI에 적용
                questionText[i].text = questionData["question"].ToString();
                selectedKeys.Add(randomKey);  // 선택된 질문을 리스트에 추가
            }
        }
    }

    void Start()
    {
        sceneIndex = "0";
        questionMaker();
    }

    public void reload()
    {
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
        nextIndex(index);  // dialogTrue 설정
        textOff();         // 모든 질문 비활성화
        sceneOff(int.Parse(sceneIndex));
        arrowOff();
        rerollOff();
        Debug.Log("대화시스템");
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
    public void arrowOff()
    {
        foreach (GameObject obj in arrow)
        {
            obj.SetActive(false);
        }
    }

    public void textOff()
    {
        foreach (TMP_Text text in questionText)
        {
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
    }

}


