using UnityEngine;

public class Line_Production : MonoBehaviour
{
    public AudioClip Clip;
    private AudioManager AudioPlayer;

    public UI_Production ui_production;
    public RectTransform lineRect;
    public Obj_Production Bg_Production;

    private void Start()
    {
        AudioPlayer = AudioManager.Instance;
    }

    public void Line_align()
    {
        ui_production.Move("Lerp", 0.1f, "x", 0f, 0f);
        //AudioPlayer.PlaySE(Clip);
    }

    public void Line_Ink() {
        ui_production.Move("Smooth", 0.2f, "x", lineRect.anchoredPosition.x, 300f);
        Bg_Production.Move("Smooth", true, 0.2f, "x", Bg_Production.transform.position.x, 1.5f);
        AudioPlayer.PlaySE(Clip);
    }

    public void Line_Client()
    {
        ui_production.Move("Smooth", 0.2f, "x", lineRect.anchoredPosition.x, -300f);
        Bg_Production.Move("Smooth", true, 0.2f, "x", Bg_Production.transform.position.x, -1.5f);
        AudioPlayer.PlaySE(Clip);
    }
}
