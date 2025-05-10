using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq.Expressions;
using UnityEngine.UIElements;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.SceneManagement;

public class DialogueParser : MonoBehaviour
{
    private Dictionary<string, string[]> allAnswers = new Dictionary<string, string[]>();

    public QuestionManager QuestionManager;
    public GameObject panel_ink;
    public GameObject panel_client;
    public ScrollViewController scroll_controller;

    public UnityEngine.UI.Image name_box_ink; //이름 상자
    public UnityEngine.UI.Image name_box_client;
    public TMP_Text targetText_ink; 
    public TMP_Text targetText_client;

    public SpriteRenderer Client;
    public SpriteRenderer InkHead;

    public Panel_Ink_Production panel_ink_production;
    public Panel_Client_Production panel_client_production;
    public Line_Production line_production;
    public Character_ink_production character_ink_production;
    public Character_client_production character_client_production;
    public Character_ink_panel_production character_ink_panel_production;
    //public Clapper_production clapper_production;
    //public Clap_production clap_production;
    public Cut_production cut_production;

    public float delay = 0.06f; //글자가 움직이는 속도
    public bool Show_Ink_Panel_check = false;
    public bool Show_Client_Panel_check = false;
    public bool Line_Client_check = false;
    public bool Line_ink_check = false;

    public void AnswerToQuestion(string key) {
        LoadCSV(key);
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

    public void CutProduction()
    {
        StartCoroutine(PlayCutsceneThenLoadEnding());
    }

    private IEnumerator PlayCutsceneThenLoadEnding()
    {
        yield return StartCoroutine(GameCutCoroutine());  // 코루틴 종료까지 대기
        SceneManager.LoadScene("Ending");                     // 이후 씬 전환
    }

    void Start()
    {
        //LoadCSV();
        init();
        StartCoroutine(GameStartCoroutine()); //처음 잉크맨이 앉는 장면
        //StartCoroutine(GameEndCoroutine());
        //StartCoroutine(GameCutCoroutine());
        cut_production.Cut_AlpahSetZero();

        Show_Ink_Panel_check = false;
        Show_Client_Panel_check = false;
        Line_Client_check = false;
        Line_ink_check = false;
    // StartCoroutine(ShowDialogueByPrefix("story1_0_3_0")); // dialogIndex 값을 불러오는 장치(동시에 글씨 생성 장치) // 임시 값 설정
    }

    IEnumerator GameStartCoroutine() { 
        character_ink_production.GameStartInkMove(); //다가오는 거 
        character_ink_panel_production.GameStartInkMove_panel();

        yield return new WaitForSeconds(0.5f);
        //character_ink_panel_production.GameStartInkRotate_panel();

        //Sprite sprite_stretch = Resources.Load<Sprite>("Ink_Character/ink_stretch"); //사진 불러오기
        //InkHead.sprite = sprite_stretch;
        yield return new WaitForSeconds(0.3f);
        Sprite sprite_walk = Resources.Load<Sprite>("Ink_Character/ink_walk");
        InkHead.sprite = sprite_walk;
        character_ink_panel_production.GameStartInkMoveBack();

        
        
        yield return new WaitForSeconds(0.1f);
        character_ink_production.GameStartInkMoveBack();
        //character_ink_panel_production.GameStartInkRotate_panelBack();

        yield return new WaitForSeconds(0.3f);
        Sprite sprite_idle = Resources.Load<Sprite>("Ink_Character/ink_idle");
        InkHead.sprite = sprite_idle;

        yield return new WaitForSeconds(0.3f);
        character_client_production.Alpha_Client_on();

    }

    //IEnumerator GameEndCoroutine()
    //{
    //    clapper_production.ClapperMoveX();
    //    yield return new WaitForSeconds(0.85f);
    //    clapper_production.ClapperMoveY();
    //    yield return new WaitForSeconds(0.57f);
    //    clapper_production.ClapperRotate();
    //    yield return new WaitForSeconds(0.15f);
    //    //clap_production.RotateClap_up();
    //    yield return new WaitForSeconds(0.3f);
    //    yield return new WaitForSeconds(0.1f);
    //    //clap_production.RotateClap_down();


    //}

    IEnumerator GameCutCoroutine() {
        Debug.Log("샌즈");

        
        yield return new WaitForSeconds(0.01f);
        cut_production.Cut_AlpahUpdate();
        yield return new WaitForSeconds(0.1f);
        cut_production.Cut_AlpahFianl();
        yield return new WaitForSeconds(2.25f);
    }

    private void init()
    {
        panel_ink_production.Destroy_Ink_Panel();

        panel_client_production.Destroy_Client_Panel();

        line_production.Line_align();
  
        //character_client_production.Alpha_Client();
    }

    IEnumerator ShowDialogueByPrefix(string prefix)
    {
        List<string> keys = GetDialogueIndexesByPrefix(prefix);
        List<string> infoList = new List<string>(); // 정보 누적용

        foreach (string key in keys)
        {
            yield return StartCoroutine(ShowDialogueByIndex(key));
            yield return new WaitForSeconds(0.5f); // 시간 지연

            // info가 있다면 리스트에 추가
            if (allAnswers.ContainsKey(key) && allAnswers[key].Length > 3)
            {
                string info = allAnswers[key][3];
                if (!string.IsNullOrWhiteSpace(info))
                {
                    //infoList.Add(info);
                    scroll_controller.AddClueToScroll(info);
                    string currentSceneIndex = QuestionManager.GetCurrentSceneIndex();
                    QuestionManager.AddClueToImage(currentSceneIndex, info);
                }
            }
        }

        new WaitForSeconds(2f);
        Debug.Log("모든 대사 출력 완료."); 
        StopAllCoroutines(); //QuestionManager에게 끝났음을 알림
                                                         
        panel_client_production.Hide_Client_Panel_Instant(); //판넬이 즉시 사라지는 효과(SetActive 대체)
        //글자가 처음 사라질 때를 체크하는 변수 초기화
        Show_Client_Panel_check = false;

        targetText_client.text = "";

        //  정보 유무에 따라 알맞은 allOn() 호출
        if (infoList.Count > 0)
        {
            QuestionManager.allOn(infoList);
        }
        else
        {
            QuestionManager.allOn();
        }
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
        
        return result;
    }

    IEnumerator ShowDialogueByIndex(string index) //인덱스의 위치에 따라 화자 설정
    {
        if (!allAnswers.ContainsKey(index))
            yield break;

        string speaker = allAnswers[index][0];
        string line = allAnswers[index][1];
        string act = allAnswers[index][2];

        TMP_Text target = null;
        UnityEngine.UI.Image image = null;
        Debug.Log("실행");
        if (speaker.Contains("ink"))
        {
            if (Show_Ink_Panel_check == false) 
            {
                panel_ink_production.Move_Ink_Panel(); //move

                panel_ink_production.Show_Ink_Panel(); //글자가 처음 나타날 때 생기는 효과 

                line_production.Line_Ink(); // 가운데 라인 움직임

                character_ink_panel_production.MoveRight_Ink();

                character_client_production.MoveRight_Client();

                panel_ink_production.Move_Ink_Panel_Right();

                panel_client_production.Move_Client_Panel_Right();

                panel_ink_production.Move_Ink_Panel_Right();

                Line_ink_check = true;
                name_box_ink.SetNativeSize();
                target = targetText_ink;
                
                image = name_box_ink;
                ApplyActImage(speaker, act);
                Show_Ink_Panel_check = true;
            }
            else { 
                target = targetText_ink; 
                ApplyActImage(speaker, act);
                if (!Line_Client_check && Line_ink_check) //라인 체크 및 움직이기
                {
                    line_production.Line_Ink();

                    character_ink_panel_production.MoveRight_Ink();

                    character_client_production.MoveRight_Client();

                    panel_ink_production.Move_Ink_Panel_Right();

                    panel_client_production.Move_Client_Panel_Right();

                    panel_ink_production.Move_Ink_Panel_Right();

                    Line_ink_check = false;
                    Line_Client_check = true;
                }

            }
            
        }
        else if (speaker.Contains("client"))
        {
            if (Show_Client_Panel_check == false)
            {
                
                panel_client_production.Move_Client_Panel();  //move

                panel_client_production.Show_Client_Panel(); //글자가 처음 나타날 때 생기는 효과

                line_production.Line_Client();

                character_ink_panel_production.MoveLeft_Ink();

                character_client_production.MoveLeft_Client();

                panel_client_production.Move_Client_Panel_Left();

                panel_ink_production.Move_Ink_Panel_Left();

                name_box_client.SetNativeSize();
                target = targetText_client;
               
                image = name_box_client;
                ApplyActImage(speaker, act);
                Show_Client_Panel_check = true;

            }
            else
            {
                target = targetText_client;
                ApplyActImage(speaker, act);
                if (Line_Client_check && !Line_ink_check) { //라인 체크 및 움직이기
                    line_production.Line_Client();

                    character_ink_panel_production.MoveLeft_Ink();

                    character_client_production.MoveLeft_Client();

                    panel_client_production.Move_Client_Panel_Left();

                    panel_ink_production.Move_Ink_Panel_Left();

                    Line_Client_check = false;
                    Line_ink_check = true;
                }
                    
                
            }
            
        }
        /*if (target == null)
        {
            Debug.LogError("targetText is null! 대사를 출력할 TMP_Text가 연결되어 있지 않습니다.");
            yield break;
        }*/
        Debug.Log(index);
        yield return StartCoroutine(TextPrintToTarget(target, line));
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

    IEnumerator TextPrintToTarget(TMP_Text target, string text) //텍스트 타이핑
    {
        target.text = "";
        foreach (char c in text)
        {
            target.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    private void LoadCSV(string story) //로드 및 저장 
    {
        var csvData = CSVReader.Read(story); // Resources/dialogue.csv // 임시 수정-story1_0_3_0
        //story1_0_0_0
        foreach (var row in csvData)
        {
            if (!row.ContainsKey("keys") || !row.ContainsKey("speaker") || !row.ContainsKey("act") || !row.ContainsKey("dialogue") || !row.ContainsKey("information"))
                continue;

            string index = row["keys"].ToString().Trim();
            string speaker = row["speaker"].ToString().Trim();
            string animation = row["act"].ToString().Trim();
            string dialogue = row["dialogue"].ToString().Trim();
            string information = "";
            if (row.ContainsKey("information") && row["information"] != null && !string.IsNullOrWhiteSpace(row["information"].ToString()))
            {
                information = row["information"].ToString().Trim();
            }



            if (!allAnswers.ContainsKey(index))
                allAnswers.Add(index, new string[] { speaker, dialogue, animation, information });
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