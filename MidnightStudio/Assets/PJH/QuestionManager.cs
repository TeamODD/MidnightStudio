using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuestionManager : MonoBehaviour
{
    public string sceneIndex = "0";
    public List<TMP_Text> questionText = new List<TMP_Text>();
    private List<Dictionary<string,object>> QuestionDictionary;
    void Awake()
    {
        QuestionDictionary = CSVReader.Read ("story1_question");
        for(var i=0; i < QuestionDictionary.Count; i++) {
            Debug.Log("key:" + QuestionDictionary[i]["key"] + " " +
                   "question:" + QuestionDictionary[i]["question"] + " ");
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

    // 3개의 랜덤 질문을 뽑기
    for (int i = 0; i < 3; i++)
    {
        int randomIndex = UnityEngine.Random.Range(0, validKeys.Count);
        string randomKey = validKeys[randomIndex];

        // randomKey에 해당하는 질문 찾기
        Dictionary<string, object> questionData = QuestionDictionary.Find(entry => entry["key"].ToString() == randomKey);

        if (questionData != null)
        {
            // 해당 질문을 UI에 적용
            questionText[i].text = questionData["question"].ToString();
        }
    }
}

    void Start() {
        questionMaker();
    }
    public void sceneIndexChange0() {
        sceneIndex = "0";
        questionMaker();
    }
    public void sceneIndexChange1() {
        sceneIndex = "1";
        questionMaker();
    }
    public void sceneIndexChange2() {
        sceneIndex = "2";
        questionMaker();
    }
    public void sceneIndexChange3() {
        sceneIndex = "3";
        questionMaker();
    }
    public void sceneIndexChange4() {
        sceneIndex = "4";
        questionMaker();
    }

}
