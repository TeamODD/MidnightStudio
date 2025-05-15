using UnityEngine;

public class Cut_production : MonoBehaviour
{
    public UI_Production ui_production;

    public AudioClip[] Clips;
    private AudioManager AudioPlayer;

    private void Start()
    {
        AudioPlayer = AudioManager.Instance;
    }

    public void Cut_AlpahSetZero()
    { //ó�� 0���� �ʱ�ȭ
        ui_production.Alpha("Instant", 1f, 1, 0);
    }

    public void Cut_AlpahUpdate()
    { //���� ����
        ui_production.Scale("Lerp", 0.1f, new Vector3(3f, 3f, 1f), new Vector3(0.8f, 0.8f, 1f));
        ui_production.Alpha("Instant", 0.35f, 0, 1);
        AudioPlayer.PlaySE(Clips[1]);
    }

    public void Cut_AlpahFianl() //�� ����
    {
        ui_production.Scale("Smooth", 0.25f, new Vector3(0.8f, 0.8f, 1f), new Vector3(1f,1f,1f));
    }
}
