using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Start_text : MonoBehaviour
{
    public TextBox_Production TextBox;

    public string LastSpeaker;
    public SynopsisManager manager;
    public float StartTextDelay = 0.05f;

    public TMP_Text startText;
    public TMP_Text speaker_panel;

    public SpriteRenderer Client;
    public SpriteRenderer InkHead;

    public SynopsisCharacterColoring synopsisColorManager;
    public Speaker_NextFlash speaker_nextFlash;
    public Sprite[] Image_index = new Sprite[] { };

    public Image ImageDisplay;
    private Dictionary<string, string[]> dialogues = new Dictionary<string, string[]>();
    private Coroutine typingCoroutine;

    private AudioManager AudioPlayer;
    public AudioClip[] Clips;

    // 1 ��� �ҷ����� -> �ؽ�Ʈ Ÿ���� ���� -> ��� ����, ���ͷθ� �Ѿ�� -> ���� ������ ���� ��� <�ݺ� ���� ������> -> ������ ����� �α�(manager.EnableProceed())

    public void Start()
    {
        AudioPlayer = AudioManager.Instance;

        SynopsisLoadCSV("story1_synopsis");
        StartCoroutine(StartTextIndex("story1_synopsis"));

    }

    IEnumerator StartTextIndex(string prefix)
    {
        List<string> keys = StartTextList(prefix);
        bool isFirst = true;


        foreach (string key in keys)
        {
            if (!isFirst)
            {
                // ù ��° ���Ĵ� ���� �Է� ���
                yield return StartCoroutine(WaitForEnterKey());
            }

            // �ؽ�Ʈ ���
            yield return StartCoroutine(StartTextTyping(key));

            isFirst = false;
        }
        manager.sceneFader.FadeToSceneAdditive("RealMainGame");
    }

    IEnumerator WaitForEnterKey()
    {
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        AudioPlayer.PlaySE(Clips[1]);
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

    IEnumerator StartTextTyping(string index) //�ε����� ��ġ�� ���� ȭ�� ����
    {
        if (!dialogues.ContainsKey(index))
            yield break;

        string speaker = dialogues[index][0];
        string line = dialogues[index][1];
        string act = dialogues[index][2];
        string imageIndex = dialogues[index][3];  // "0"등 외의 값 부르기(추가된 거)

        TMP_Text target = null;
        Debug.Log(speaker.Contains("ink"));
        if (speaker == "ink")
        {
            if (LastSpeaker != "ink")
            {
                TextBox.SpeakerBox_Production_Start("ink");
                LastSpeaker = "ink";
            }
            speaker_panel.text = "잉크 헤드";
            target = startText;
            ApplyActImage(speaker, act);
            synopsisColorManager.SetCharacterColor("ink");
        }

        else if (speaker == "just_ink")
        {
            if (LastSpeaker != "ink")
            {
                TextBox.SpeakerBox_Production_Start("ink");
                LastSpeaker = "ink";
            }
            speaker_panel.text = "잉크 헤드";
            target = startText;
            ApplyActImage("ink", act);
            synopsisColorManager.SetCharacterColor("just_ink");

        }

        else if (speaker == "client")
        {
            if (LastSpeaker != "client")
            {
                TextBox.SpeakerBox_Production_Start("client");
                LastSpeaker = "client";
            }
            speaker_panel.text = "의뢰인";
            target = startText;
            ApplyActImage(speaker, act);
            synopsisColorManager.SetCharacterColor("client");
        }

        else if (speaker == "manager")
        {
            if (LastSpeaker != "manager")
            {
                TextBox.SpeakerBox_Production_Start("manager");
                LastSpeaker = "manager";
            }
            speaker_panel.text = "영화 감독";
            target = startText;
            synopsisColorManager.SetCharacterColor("manager");
        }

        else if (speaker == "just_manager")
        {
            if (LastSpeaker != "manager")
            {
                TextBox.SpeakerBox_Production_Start("manager");
                LastSpeaker = "manager";
            }
            speaker_panel.text = "영화 감독";
            target = startText;
            synopsisColorManager.SetCharacterColor("just_manager");
        }
        Debug.Log(index);
        ShowImageByIndex(imageIndex); // By image_index's value, change the image that HDH want. you know? GO 241's Line.
        TextBox.TextBox_Production_Start();
        typingCoroutine = StartCoroutine(TextTyping(target, line));


        // Ÿ���� ���߿� ����Ű�� ������ ������ �ڷ�ƾ �����ϰ� �������� �Ѿ
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
            Debug.LogError($"[����] '{act}'�� �ش��ϴ� �̹����� ã�� �� �����ϴ�. ��θ� �ٽ� Ȯ���ϼ���.");
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
            basePath = "Sysnopsis_Ink/";
        }
        else if (speaker.Contains("client"))
        {
            basePath = "Sysnopsis_Client/";
        }
        else
        {
            Debug.LogWarning("�� �� ���� ȭ���Դϴ�.");
            return null;
        }

        string fullPath = basePath + act;

        Sprite sprite = Resources.Load<Sprite>(fullPath);

        if (sprite == null)
        {
            Debug.LogError($"[�ε� ����] Sprite ���: Resources/{fullPath}.png �Ǵ� .jpg �� �������� ����.");
        }

        return sprite;
    }

    IEnumerator TextTyping(TMP_Text target, string text)
    {
        target.text = "";
        foreach (char c in text)
        {
            target.text += c;
            AudioPlayer.PlaySE(Clips[0]);
            yield return new WaitForSeconds(StartTextDelay);
        }

        yield return null;
        speaker_nextFlash.StartFlashButtonCoroutine();
    }

    private void ShowImageByIndex(string imageIndex) // Yeah this.
    {
        Debug.Log(imageIndex);
        if (imageIndex != "")
        {
            ImageDisplay.gameObject.SetActive(true);
            if (!int.TryParse(imageIndex, out int index)) return; //imageIndex -> sting value change int value -> on int index ->

            Sprite Sprite = Image_index[index];
            if (ImageDisplay != null) //if same number? then do not try saem code. you nam sang?
            {
                ImageDisplay.sprite = Sprite; //Display the image.
            }
        }
        else
        {
            ImageDisplay.gameObject.SetActive(false);
        }
    }


    private void SynopsisLoadCSV(string story) //�ε� �� ���� 
    {
        var csvData = CSVReader.Read(story); // Resources/dialogue.csv // �ӽ� ����-story1_0_3_0
        //story1_0_0_0
        foreach (var row in csvData)
        {
            if (!row.ContainsKey("keys") || !row.ContainsKey("speaker") || !row.ContainsKey("act") || !row.ContainsKey("dialogue") || !row.ContainsKey("image_index"))
                continue;

            string index = row["keys"].ToString().Trim();
            string speaker = row["speaker"].ToString().Trim();
            string animation = row["act"].ToString().Trim();
            string dialogue = row["dialogue"].ToString().Trim();
            string image = row["image_index"].ToString().Trim();

            if (!dialogues.ContainsKey(index))
                dialogues.Add(index, new string[] { speaker, dialogue, animation, image });
        }
    }
}
