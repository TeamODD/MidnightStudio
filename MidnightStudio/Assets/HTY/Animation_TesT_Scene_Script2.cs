using UnityEngine;

public class Animation_TesT_Scene_Script2 : MonoBehaviour
{
    public UI_Production Animation_TesT_Scene_Script;
    public void Show() {
        Animation_TesT_Scene_Script.Move("Lerp", 10f, "x", -980f, 980f);
        Animation_TesT_Scene_Script.Alpha("Lerp", 5f, 1, 0);

    }

    private void Start()
    {
        Show();
    }
}
