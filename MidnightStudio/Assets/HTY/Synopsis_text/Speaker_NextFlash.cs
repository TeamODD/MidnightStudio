using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Speaker_NextFlash : MonoBehaviour
{
    public UI_Production speaker_next;
    private bool isFlash = true;
    public float Flash = 20f;

    //��簡 ���� ��� -> flashbutton ���� -> ���� ������ ��� ��� (�ݺ�)

    public void Start()
    { 
        speaker_next.gameObject.SetActive(false);
    }

    public void StartFlashButtonCoroutine() {
        StartCoroutine(FlashNextButton());
    }
    IEnumerator FlashNextButton()
    {
        isFlash = true;
        speaker_next.gameObject.SetActive(true);
        while (isFlash)
        {
            // ��� On
            speaker_next.Alpha("Lerp", Flash, 1f, 0f);
            yield return StartCoroutine(WaitWhileCheckingInput(Flash));

            if (!isFlash) yield break;

            // ��� Off
            speaker_next.Alpha("Lerp", Flash, 0f, 1f);
            yield return StartCoroutine(WaitWhileCheckingInput(Flash));
        }
        
    }
    // Flash �ʸ�ŭ �� ������ ����ϸ�, �� ���� Enter Ű�� ������ isFlash = false ó��
    IEnumerator WaitWhileCheckingInput(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isFlash = false;
                speaker_next.Alpha("Instant", 0f, 0f, 0f);
                speaker_next.gameObject.SetActive(false);
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
