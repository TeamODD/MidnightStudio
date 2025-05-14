using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public GameObject KJH;
    public GameObject HDH;
    public GameObject KCW;
    public GameObject JES;
    public GameObject EJY;
    public GameObject PJH;
    public GameObject HTY;
    public GameObject ThanksForPlaying;

    public void Start()
    {
        StartCoroutine(CreditCoroutine());
    }

    IEnumerator CreditCoroutine() {

        yield return new WaitForSeconds(2f);
        KJH.SetActive(true);

        yield return new WaitForSeconds(4f);
        KJH.SetActive(false);
        HDH.SetActive(true);

        yield return new WaitForSeconds(4f);
        HDH.SetActive(false);
        KCW.SetActive(true);

        yield return new WaitForSeconds(4f);
        KCW.SetActive(false);
        JES.SetActive(true);

        yield return new WaitForSeconds(4f);
        JES.SetActive(false);
        EJY.SetActive(true);

        yield return new WaitForSeconds(4f);
        EJY.SetActive(false);
        PJH.SetActive(true);

        yield return new WaitForSeconds(4f);
        PJH.SetActive(false);
        HTY.SetActive(true);

        yield return new WaitForSeconds(4f);
        HTY.SetActive(false);
        ThanksForPlaying.SetActive(true);
    }
}
