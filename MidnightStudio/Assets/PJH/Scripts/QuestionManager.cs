using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public SceneFader sceneFader;
    public SlotManager slotManager; // SlotManager 참조 (Inspector에서 할당)
    public ScrollViewController scrollViewController;
    public List<GameObject> arrowList = new List<GameObject>();
    public List<GameObject> scenearrowList = new List<GameObject>();
    public QuestionProduction questionProduction;
    private string lastScene = "0";
    public TextBackground[] background = new TextBackground[3];
    public bool canClick = true;
    public DialogueParser parser;
    public GameObject reroll;
    public List<GameObject> questionBox = new List<GameObject>();
    private string sceneIndex;
    public List<Image> scene = new List<Image>();
    public List<TMP_Text> questionText = new List<TMP_Text>();
    public static string dialogIndex;
    private List<Dictionary<string, object>> QuestionDictionary;
    // 씬마다 선택된 질문들을 저장
    private Dictionary<string, List<string>> sceneToQuestions = new Dictionary<string, List<string>>();
    private List<Coroutine> activeCoroutines = new List<Coroutine>();
    //단서들 저장 리스트
    private Dictionary<string, List<string>> imageInfoMap = new Dictionary<string, List<string>>();
    private Dictionary<int, Slot> cachedSlots = new Dictionary<int, Slot>();
    private List<string> clueList = new List<string>();

    // 현재 세팅된 질문들을 추적하는 리스트
    private List<string> selectedKeys = new List<string>();

    private IEnumerator rerollQuestionSign;


    void Awake()
    {
        // 자동으로 중복 EventSystem 제거
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1)
        {
            // 자신이 중복일 경우 제거
            if (systems[0] != this.GetComponent<EventSystem>())
            {
                Destroy(this.gameObject);
            }
        }

        // SlotManager 인스턴스 확인 (Inspector에서 할당하는 것을 권장)
        if (slotManager == null)
        {
            slotManager = FindObjectOfType<SlotManager>(); // 최후의 수단으로 찾기
            if (slotManager == null) Debug.LogError("SlotManager를 찾을 수 없습니다! Inspector에서 QuestionManager에 할당해주세요.");
        }

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

    void CacheSlots()
    {
        if (cachedSlots.Count > 0 && FindObjectsOfType<Slot>().Length == cachedSlots.Count) return; // 이미 캐시되었고 슬롯 수가 같다면 스킵

        cachedSlots.Clear();
        Slot[] allSlotsInScene = FindObjectsOfType<Slot>();
        foreach (Slot slot in allSlotsInScene)
        {
            if (!cachedSlots.ContainsKey(slot.slotIndex))
            {
                cachedSlots.Add(slot.slotIndex, slot);
            }
            else
            {
                Debug.LogWarning($"Duplicate slotIndex {slot.slotIndex} found. Slot caching might be incorrect for {slot.gameObject.name} and {cachedSlots[slot.slotIndex].gameObject.name}.", slot.gameObject);
            }
        }
        // Debug.Log($"Slots cached: {cachedSlots.Count}");
    }

    public void questionMaker()
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
        // 시작하자마자 페이드 인
        if (sceneFader != null)
        {
            sceneFader.FadeInNow();
        }

        sceneIndex = "0";
        CacheSlots(); // 게임 시작 시 슬롯 정보 캐시
        // questionMaker();
    }

    public void rerollQuestion()
    {
        if (canClick)
        {
            canClick = false;
            if (rerollQuestionSign != null)
            {
                StopCoroutine(rerollQuestionSign);
            }
            rerollQuestionSign = rerollQuestionCoroutine();
            StartCoroutine(rerollQuestionSign);
        }
    }


    private IEnumerator rerollQuestionCoroutine()
    {
        questionProduction.objectDisappear();
        questionArrowOff();
        questionHide();
        yield return new WaitForSeconds(0.2f);
        reload();
        questionProduction.objectAppear();
        questionShow();
        yield return new WaitForSeconds(0.2f);
        canClick = true;
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

    public void changeSceneQuestion(string sceneIndex)
    {
        if (canClick && sceneIndex != lastScene)
        {
            canClick = false;
            lastScene = sceneIndex;

            if (rerollQuestionSign != null)
            {
                StopCoroutine(rerollQuestionSign);
            }

            rerollQuestionSign = changeSceneQuestionCoroutine(sceneIndex);
            StartCoroutine(rerollQuestionSign);

            // sceneIndex 기반으로 arrow 활성화
            ActivateArrowForScene(sceneIndex);

            if (imageInfoMap.ContainsKey(sceneIndex))
            {
                scrollViewController.ClearClueList();
                scrollViewController.UpdateClueList(imageInfoMap[sceneIndex]);
            }
            else
            {
                scrollViewController.ClearClueList(); // 단서가 없으면 초기화만
            }
        }
    }
    void ActivateArrowForScene(string sceneIdentifier)
    {
        // 슬롯 총 수만큼 반복
        for (int i = 0; i < scenearrowList.Count; i++)
        {
            var scene = slotManager.GetSceneInSlot(i);

            // 해당 슬롯에 scene이 있고, sceneIdentifier가 일치하면 그 위치에 화살표 표시
            if (scene != null && scene.sceneIdentifier == sceneIdentifier)
            {
                scenearrowList[i].SetActive(true);
            }
            else
            {
                scenearrowList[i].SetActive(false);
            }
        }
        /*for (int i = 0; i < scenearrowList.Count; i++)
        {
            if (i == int.Parse(index))
                scenearrowList[i].SetActive(true);
            else
                scenearrowList[i].SetActive(false);
        }*/
    }

    public void ActivateArrowBySlotIndex(int slotIndex)
    {
        for (int i = 0; i < scenearrowList.Count; i++)
        {
            scenearrowList[i].SetActive(i == slotIndex);
        }
    }

    private IEnumerator changeSceneQuestionCoroutine(string sceneIndex)
    {
        questionProduction.objectDisappear();
        questionArrowOff();
        questionHide();
        yield return new WaitForSeconds(0.2f);
        sceneIndexChange(sceneIndex);
        questionProduction.objectAppear();
        questionShow();
        yield return new WaitForSeconds(0.2f);
        canClick = true;
    }

    public void sceneIndexChange(string sceneIndex)
    {
        if (this.sceneIndex == sceneIndex) return;
        this.sceneIndex = sceneIndex;
        questionMaker();
    }

    public void OnQuestionClicked(int index)
    {
        if (canClick)
        {
            nextIndex(index);  // dialogTrue 설정
            questionProduction.objectDisappear();
            // 모든 질문 비활성화
            sceneOff(sceneIndex); // 현재 sceneIndex (string)를 전달
            questionArrowOff();
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

    public void IntroReset()
    {
        questionArrowOff();
        rerollOff();
        questionHide();
        questionProduction.objectInstantDisappear();
        canClick = false;
    }

    public void IntroProduction()
    {
        parser.AnswerToQuestion("story1_intro");
    }

    public void nextIndex(int index)
    {
        if (index < 0 || index >= selectedKeys.Count)
        {
            Debug.LogError("Invalid question index clicked.");
            return;
        }

        string clickedKey = selectedKeys[index];
        Debug.Log(index);
        Dictionary<string, object> questionData = QuestionDictionary.Find(entry => entry["key"].ToString() == clickedKey);

        if (questionData != null)
        {
            dialogIndex = questionData["dialogTrue"].ToString();

            Debug.Log("Clicked question's dialogTrue: " + dialogIndex);

            // 👉 DialogManager 같은 곳에 전달
            // 예: DialogManager.Instance.Show(dialogTrue);
        }
    }

    public void sceneOff(string currentSceneId)
    {
        if (slotManager == null) { Debug.LogError("SlotManager가 QuestionManager에 할당되지 않았습니다."); return; }
        if (cachedSlots.Count == 0) CacheSlots(); // 슬롯 캐시가 비어있으면 다시 시도

        SceneDrag currentSceneDragObject = null;
        int currentSceneSlotIndex = -1;

        // 현재 sceneId에 해당하는 SceneDrag 객체와 그것이 담긴 슬롯 인덱스를 찾습니다.
        for (int i = 0; i < slotManager.totalSlots; i++)
        {
            SceneDrag sd = slotManager.GetSceneInSlot(i);
            if (sd != null && sd.sceneIdentifier == currentSceneId)
            {
                currentSceneDragObject = sd;
                currentSceneSlotIndex = i;
                break;
            }
        }

        // 현재 씬으로 식별된 SceneDrag 객체가 있다면 활성화 상태를 보장합니다.
        if (currentSceneDragObject != null)
        {
            currentSceneDragObject.gameObject.SetActive(true);
        }

        // 모든 슬롯을 순회하며 상태를 설정합니다.
        for (int i = 0; i < slotManager.totalSlots; i++)
        {
            Slot slotComponent = cachedSlots.ContainsKey(i) ? cachedSlots[i] : null;
            if (slotComponent == null) continue; // 캐시된 슬롯이 없으면 건너뜁니다.

            GameObject slotGO = slotComponent.gameObject;
            CanvasGroup cg = slotGO.GetComponent<CanvasGroup>();
            if (cg == null) cg = slotGO.AddComponent<CanvasGroup>();

            SceneDrag sceneDragInThisSlot = slotManager.GetSceneInSlot(i);

            if (i == currentSceneSlotIndex) // 현재 씬이 담긴 슬롯인 경우
            {
                cg.alpha = 1f; // 불투명
                cg.interactable = true;
                cg.blocksRaycasts = true;
                // currentSceneDragObject가 이미 위에서 활성화되었으므로, sceneDragInThisSlot도 활성 상태일 것입니다.
                if (sceneDragInThisSlot != null) sceneDragInThisSlot.gameObject.SetActive(true);
            }
            else // 다른 슬롯인 경우
            {
                cg.alpha = 0f; // 투명 (완전히 안 보이게)
                cg.interactable = false;
                cg.blocksRaycasts = false;
                // 이 슬롯에 다른 씬이 있다면 비활성화합니다.
                if (sceneDragInThisSlot != null)
                {
                    sceneDragInThisSlot.gameObject.SetActive(false);
                }
            }
        }
    }
    public string GetCurrentSceneIndex()
    {
        return sceneIndex;
    }

    public void rerollOff()
    {
        reroll.SetActive(false);
    }

    public void questionArrowOff()
    {
        foreach (TMP_Text obj in questionText)
        {
            obj.GetComponent<ArrowAppear>().alphaOnOff();
        }
    }

    public void allOn()
    { //정보가 없음
        reroll.SetActive(true);

        if (slotManager == null) { Debug.LogError("SlotManager가 QuestionManager에 할당되지 않아 allOn을 실행할 수 없습니다."); return; }
        if (cachedSlots.Count == 0) CacheSlots();

        // 모든 슬롯과 그 안의 SceneDrag 아이템을 활성화하고 불투명하게 만듭니다.
        for (int i = 0; i < slotManager.totalSlots; i++)
        {
            SceneDrag sceneDragInThisSlot = slotManager.GetSceneInSlot(i);
            if (sceneDragInThisSlot != null)
            {
                sceneDragInThisSlot.gameObject.SetActive(true);
            }

            Slot slotComponent = cachedSlots.ContainsKey(i) ? cachedSlots[i] : null;
            if (slotComponent != null)
            {
                GameObject slotGO = slotComponent.gameObject;
                CanvasGroup cg = slotGO.GetComponent<CanvasGroup>();
                if (cg == null) cg = slotGO.AddComponent<CanvasGroup>();
                cg.alpha = 1f;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }
        }

        foreach (GameObject obj in questionBox)
        {
            obj.SetActive(true);
        }
        reload();
        questionShow();
        questionProduction.objectAppear();
        canClick = true;
    }

    public void allOn(List<string> infoList) //정보가 있음
    {
        foreach (string info in infoList)
        {
            if (!clueList.Contains(info))
            {
                clueList.Add(info); // 중복 제거하고 추가
            }
        }
        scrollViewController.UpdateClueList(clueList);
        allOn();
    }

    public void AddClueToImage(string sceneIndex, string clue)
    {
        if (!imageInfoMap.ContainsKey(sceneIndex))
        {
            imageInfoMap[sceneIndex] = new List<string>();
        }

        if (!imageInfoMap[sceneIndex].Contains(clue))
        {
            imageInfoMap[sceneIndex].Add(clue);
        }
    }


    public void questionHide()
    {
        for (int i = 0; i < 3; i++)
        {
            background[i].hide();
        }
    }
    public void questionShow()
    {
        for (int i = 0; i < 3; i++)
        {
            background[i].show();
        }
    }
    public List<string> GetClueList()
    {
        return new List<string>(clueList); // 복사본 반환
    }
}
