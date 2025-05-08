using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public Image Gauge;
    public Image gaugebar;
    public UI_Production ui_Production;
    public DialogueParser parser;
    private float GAME_OVER_TIME = 240.0f;
    public float step_timer = 0.0f;

    private bool isEndingTriggered = false; // 중복 호출 방지
    private bool triggered25 = false;
    private bool triggered50 = false;
    private bool triggered75 = false;

    private bool isTimerActive = false; // 타이머 활성 여부

    // Update is called once per frame
    void Update()
    {
        if (!isTimerActive) return;

        this.step_timer += Time.deltaTime;
        gaugebar.fillAmount = 1 - (GAME_OVER_TIME - this.step_timer) / GAME_OVER_TIME;

        float percent = gaugebar.fillAmount;

        if (percent >= 0.25f && !triggered25)
        {
            triggered25 = true;
            ui_Production.Scale("Smooth", 1f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1f, 1f, 1f));
        }

        if (percent >= 0.5f && !triggered50)
        {
            triggered50 = true;
            ui_Production.Scale("Smooth", 1f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1f, 1f, 1f));
        }

        if (percent >= 0.75f && !triggered75)
        {
            triggered75 = true;
            ui_Production.Scale("Smooth", 1f, new Vector3(1.1f, 1.1f, 1.1f), new Vector3(1f, 1f, 1f));
        }

        if (!isEndingTriggered && gaugebar.fillAmount >= 1f)
        {
            isEndingTriggered = true;
            parser.CutProduction();
        }
    }

    public void StartGauge()
    {
        isTimerActive = true;
    }

    // 필요하다면 일시정지도 가능
    public void PauseGauge()
    {
        isTimerActive = false;
    }
}
