using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{
    public AudioClip bgmClip;

    void Start()
    {
        AudioManager.Instance?.PlayBGM(bgmClip);
    }
}
