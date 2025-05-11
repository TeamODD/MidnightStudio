using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Speaker_NextFlash : MonoBehaviour
{
    public GameObject speaker_next;
    private bool isFlash = true;
    public float Flash = 20f;

    //��簡 ���� ��� -> flashbutton ���� -> ���� ������ ��� ��� (�ݺ�)

    public void Start()
    { 
        speaker_next.SetActive(false);
    }

    public void StartFlashButtonCoroutine() {
        StartCoroutine(FlashNextButton());
    }
    IEnumerator FlashNextButton()
    {
        isFlash = true;
        while (isFlash)
        {
            // ��� On
            speaker_next.SetActive(true);
            yield return StartCoroutine(WaitWhileCheckingInput(Flash));

            if (!isFlash) yield break;

            // ��� Off
            speaker_next.SetActive(false);
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
                speaker_next.SetActive(false);
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
