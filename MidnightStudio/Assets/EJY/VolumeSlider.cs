using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;

    private void Start()
    {
        // �ʱⰪ ����
        if (AudioManager.Instance != null)
        {
            bgmSlider.value = AudioManager.Instance.GetBGMVolume();
            seSlider.value = AudioManager.Instance.GetSEVolume();
        }

        // ���� �� AudioManager�� �ݿ�
        bgmSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance?.SetBGMVolume(v);
        });

        seSlider.onValueChanged.AddListener((v) =>
        {
            AudioManager.Instance?.SetSEVolume(v);
        });
    }

    public void sliderSetting()
    {
        if (AudioManager.Instance != null)
        {
            bgmSlider.value = AudioManager.Instance.GetBGMVolume();
            seSlider.value = AudioManager.Instance.GetSEVolume();
        }

        // ���� �� AudioManager�� �ݿ�
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
