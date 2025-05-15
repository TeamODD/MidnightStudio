using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonControl : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject optionPanel;
    public RectTransform optionImg;
    public SceneFader sceneFader;
    public Title_Production TitleProduction;
    public GameObject audioManagerPrefab;
    public SceneBGMSetter titleBGMSetter;
    public VolumeSlider titleVolumeSlider;

    public bool CanControl;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public float slideDuration = 1f;
    private RectTransform optionRect;
    public Vector2 offScreenPosition = new Vector2(0f, -Screen.height); // �Ʒ��� ȭ�� ��
    public Vector2 onScreenPosition = Vector2.zero; // ���� ��ġ (�߾� ����)

    public AudioClip[] Clips;
    private AudioManager AudioPlayer;
    

    void Start()
    {
        if (AudioManager.Instance == null)
        {
            Instantiate(audioManagerPrefab);
            Debug.Log("AudioManager 프리팹 생성됨");
        }
        else
        {
            Debug.Log("AudioManager 이미 존재함");
        }

        AudioPlayer = AudioManager.Instance;
        AudioPlayer.AudioSetting();

        titleBGMSetter.SettingBGM();

        titleVolumeSlider.sliderSetting();
    }
    private void Awake()
    {
        //optionRect = optionPanel.GetComponent<RectTransform>();
        //offScreenPosition = new Vector2(0f, -Screen.height); // �Ʒ��ʿ��� ����
    }
    // private void Update()
    // {
    //     // Option â�� �����ְ� ���콺 ���� Ŭ�� �� �˻�
    //     if (optionPanel.activeSelf && Input.GetMouseButtonDown(0))
    //     {
    //         if (!IsPointerOverUI(optionImg))
    //         {
    //             OnExitClosed();
    //         }
    //     }
    // }
    private bool IsPointerOverUI(RectTransform targetRect)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            // OptionImg �Ǵ� �� �ڽ��̸� ���� Ŭ������ ����
            if (result.gameObject.transform.IsChildOf(targetRect))
                return true;
        }

        return false; // �ܺ� Ŭ��
    }

    public void OnStartClicked()
    {
        if (CanControl)
        {
            CanControl = false;
            sceneFader.FadeToScene("Synopsis");
            AudioPlayer.PlaySE(Clips[0]);
        }
    }

    public void OnOptionClicked()
    {
        if (CanControl)
        {
            //sceneFader.FadeToGray(0.4f); // 40% �������� ȸ��
            //optionRect.anchoredPosition = offScreenPosition; // ���� ��ġ

            TitleProduction.Option_Production_Start();
            StartCoroutine(OnOptionClicked_ObjActive());
            Debug.Log("작동됨");
            // StartCoroutine(Slide(optionRect, onScreenPosition));
            AudioPlayer.PlaySE(Clips[0]);
        }
    }
    private IEnumerator OnOptionClicked_ObjActive()
    {
        CanControl = false;
        yield return new WaitForSeconds(0.5f);

        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
        yield return null;

        CanControl = true;
    }

    public void OnExitClosed()
    {
        if (CanControl)
        {
            //optionPanel.SetActive(false);
            //mainPanel.SetActive(true);
            //sceneFader.FadeOutGray();

            TitleProduction.Option_Production_End();
            StartCoroutine(OnExitClosed_ObjActive());
            AudioPlayer.PlaySE(Clips[0]);
        }
    }

    private IEnumerator OnExitClosed_ObjActive()
    {
        CanControl = false;
        yield return new WaitForSeconds(0.5f);

        optionPanel.SetActive(false);
        mainPanel.SetActive(true);
        yield return null;

        CanControl = true;
    }

    // private IEnumerator Slide(RectTransform rect, Vector2 target)
    // {
    //     Vector2 start = rect.anchoredPosition;
    //     float elapsed = 0f;

    //     while (elapsed < slideDuration)
    //     {
    //         rect.anchoredPosition = Vector2.Lerp(start, target, elapsed / slideDuration);
    //         elapsed += Time.deltaTime;
    //         yield return null;
    //     }

    //     rect.anchoredPosition = target;
    // }
    // private IEnumerator SlideAndDeactivate(RectTransform rect, Vector2 target, System.Action onComplete)
    // {
    //     yield return Slide(rect, target);
    //     onComplete?.Invoke();
    // }

    public void OnQuitClicked()
    {
        if (CanControl)
        {
            CanControl = false;
            AudioPlayer.PlaySE(Clips[0]);
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
