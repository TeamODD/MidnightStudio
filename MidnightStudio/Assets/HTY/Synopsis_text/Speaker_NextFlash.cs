using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Speaker_NextFlash : MonoBehaviour
{
    public GameObject speaker_next;
    private bool isFlash = true;
    public float Flash = 20f;

    //대사가 전부 출력 -> flashbutton 깜빡 -> 엔터 누르면 대사 출력 (반복)

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
            // 토글 On
            speaker_next.SetActive(true);
            yield return StartCoroutine(WaitWhileCheckingInput(Flash));

            if (!isFlash) yield break;

            // 토글 Off
            speaker_next.SetActive(false);
            yield return StartCoroutine(WaitWhileCheckingInput(Flash));
        }
        
    }
    // Flash 초만큼 매 프레임 대기하며, 그 사이 Enter 키를 누르면 isFlash = false 처리
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
