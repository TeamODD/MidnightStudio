using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;

    private void Start()
    {
        // 초기값 설정
        if (AudioManager.Instance != null)
        {
            bgmSlider.value = AudioManager.Instance.GetBGMVolume();
            seSlider.value = AudioManager.Instance.GetSEVolume();
        }

        // 변경 시 AudioManager에 반영
        bgmSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance?.SetBGMVolume(v);
        });

        seSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance?.SetSEVolume(v);
        });
    }
}
