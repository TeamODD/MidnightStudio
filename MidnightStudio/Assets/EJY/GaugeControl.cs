using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour
{
    public Image gaugebar;
    private float GAME_OVER_TIME = 80.0f;
    public float step_timer = 0.0f;


    // Update is called once per frame
    void Update()
    {
        this.step_timer += Time.deltaTime;
        gaugebar.fillAmount = 1 - (GAME_OVER_TIME - this.step_timer) / GAME_OVER_TIME;

    }
}
