using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource seSource;
    public AudioClip defaultSEClip;

    private float bgmVolume;
    private float seVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 저장된 볼륨 불러오기, 기본값은 0.7
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.7f);
        seVolume = PlayerPrefs.GetFloat("SEVolume", 0.7f);
    }

    private void Start()
    {
        ApplyVolume();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        if (bgmSource != null) bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSEVolume(float volume)
    {
        seVolume = volume;
        if (seSource != null) seSource.volume = volume;
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

    public float GetBGMVolume() => bgmVolume;
    public float GetSEVolume() => seVolume;

    public void PlaySE(AudioClip clip = null)
    {
        if (seSource != null)
        {
            seSource.PlayOneShot(clip != null ? clip : defaultSEClip, seVolume);
        }
    }

    public void PlayBGM(AudioClip newClip, bool loop = true)
    {
        if (bgmSource == null || newClip == null) return;
        if (bgmSource.clip == newClip) return;

        bgmSource.clip = newClip;
        bgmSource.loop = loop;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    private void ApplyVolume()
    {
        if (bgmSource != null) bgmSource.volume = bgmVolume;
        if (seSource != null) seSource.volume = seVolume;
    }
}
