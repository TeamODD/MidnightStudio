using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SynopsisManager : MonoBehaviour
{
    public GameObject NextImg;
    public SceneFader sceneFader;
    public float waitTime = 3f; // NextImg 활성화 대기 시간

    private bool canProceed = false;

    private void Start()
    {
        // 시작하자마자 페이드 인
        if (sceneFader != null)
        {
            sceneFader.FadeInNow();
        }

        // 처음엔 NextImg를 비활성화
        NextImg.SetActive(false);

        // 일정 시간 후 NextImg를 켜고 입력 허용
        Invoke(nameof(EnableProceed), waitTime);
    }

    private void EnableProceed()
    {
        NextImg.SetActive(true);
        canProceed = true;
    }

    private void Update()
    {
        if (canProceed && Input.GetKeyDown(KeyCode.Return))
        {
            //SceneManager.LoadScene("MainGame");
            sceneFader.FadeToSceneAdditive("main");
        }
    }
}
