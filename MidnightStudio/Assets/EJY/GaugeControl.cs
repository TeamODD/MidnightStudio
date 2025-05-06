using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public Image gaugebar;
    private float GAME_OVER_TIME = 240.0f;
    public float step_timer = 0.0f;

    private bool isEndingTriggered = false; // 중복 호출 방지

    // Update is called once per frame
    void Update()
    {
        this.step_timer += Time.deltaTime;
        gaugebar.fillAmount = 1 - (GAME_OVER_TIME - this.step_timer) / GAME_OVER_TIME;

        if (!isEndingTriggered && gaugebar.fillAmount >= 1f)
        {
            isEndingTriggered = true;
            SceneManager.LoadScene("Ending"); // ← Ending 씬으로 전환
        }
    }
}
