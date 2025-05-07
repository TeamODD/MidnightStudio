using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public Image Gauge;
    public Image gaugebar;
    public UI_Production ui_Production;
    private float GAME_OVER_TIME = 24.0f;
    public float step_timer = 0.0f;

    private bool isEndingTriggered = false; // 중복 호출 방지
    private bool triggered25 = false;
    private bool triggered50 = false;
    private bool triggered75 = false;

    // Update is called once per frame
    void Update()
    {
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
            SceneManager.LoadScene("Ending"); // ← Ending 씬으로 전환
        }
    }
}
