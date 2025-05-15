using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{
    public AudioClip bgmClip;

    void Start()
    {
        AudioManager.Instance?.PlayBGM(bgmClip);
    }

    public void SettingBGM()
    {
        AudioManager.Instance?.PlayBGM(bgmClip);
    }
}
