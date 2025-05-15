using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public GameObject Staff;
    public GameObject KJH;
    public GameObject HDH;
    public GameObject KCW;
    public GameObject JES;
    public GameObject EJY;
    public GameObject PJH;
    public GameObject HTY;
    public UI_Production ThanksForPlaying;
    public UI_Production Logo;

    private AudioManager AudioPlayer;
    public AudioClip[] Clips;

    public void Start()
    {
        AudioPlayer = AudioManager.Instance;
        StartCoroutine(CreditCoroutine());
    }

    IEnumerator CreditCoroutine()
    {

        yield return new WaitForSeconds(1f);
        AudioPlayer.PlayBGM(Clips[0]);
        // 기획자
        Staff.SetActive(true);

        yield return new WaitForSeconds(4f);
        Staff.SetActive(false);
        HDH.SetActive(true);

        yield return new WaitForSeconds(4f);
        HDH.SetActive(false);

        // 프로그래밍
        EJY.SetActive(true);

        yield return new WaitForSeconds(4f);
        EJY.SetActive(false);
        PJH.SetActive(true);

        yield return new WaitForSeconds(4f);
        PJH.SetActive(false);
        HTY.SetActive(true);

        yield return new WaitForSeconds(4f);
        HTY.SetActive(false);

        // 아트
        JES.SetActive(true);

        yield return new WaitForSeconds(4f);
        JES.SetActive(false);
        KCW.SetActive(true);

        yield return new WaitForSeconds(4f);
        KCW.SetActive(false);

        // 멘토
        KJH.SetActive(true);

        yield return new WaitForSeconds(4f);
        KJH.SetActive(false);

        ThanksForPlaying.gameObject.SetActive(true);

        ThanksForPlaying.Alpha("Lerp", 2.5f, 0f, 1f);
        yield return new WaitForSeconds(4f);
        ThanksForPlaying.gameObject.SetActive(false);


        Logo.gameObject.SetActive(true);

        Logo.Alpha("Lerp", 2.5f, 0f, 1f);
        yield return new WaitForSeconds(5f);

        Logo.Alpha("Lerp", 2.5f, 1f, 0f);
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Result");
    }
}
