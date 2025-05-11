using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SynopsisManager : MonoBehaviour
{
    public GameObject NextImg;
    public SceneFader sceneFader;
    public float waitTime = 3f; // NextImg Ȱ��ȭ ��� �ð�

    private bool canProceed = false;

    private void Start()
    {
        // �������ڸ��� ���̵� ��
        if (sceneFader != null)
        {
            sceneFader.FadeInNow();
        }

        // ó���� NextImg�� ��Ȱ��ȭ
        NextImg.SetActive(false);

        // ���� �ð� �� NextImg�� �Ѱ� �Է� ���
        //Invoke(nameof(EnableProceed), waitTime);
    }

    // public void EnableProceed()
    // {
    //     NextImg.SetActive(true);
    //     canProceed = true;
    // }

    // private void Update()
    // {
    //     if (canProceed && Input.GetKeyDown(KeyCode.Return))
    //     {
    //         //SceneManager.LoadScene("MainGame");
    //         sceneFader.FadeToSceneAdditive("RealMainGame");
    //     }
    // }
}
