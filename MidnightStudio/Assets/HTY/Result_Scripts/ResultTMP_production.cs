using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class ResultTMP_production : MonoBehaviour
{
    public GameObject RemainTime;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject Scene4;
    public GameObject Scene5;
    public GameObject Result;

    public void Start()
    {
        //StartShowResultObject();
    }

    public void StartShowResultObject() {
        StartCoroutine(ShowResultObject());
    }

    IEnumerator ShowResultObject() {

        RemainTime.SetActive(true);

        yield return new WaitForSeconds(1f);

        Scene1.SetActive(true);

        yield return new WaitForSeconds(1f);

        Scene2.SetActive(true);

        yield return new WaitForSeconds(1f);

        Scene3.SetActive(true);

        yield return new WaitForSeconds(1f);

        Scene4.SetActive(true);

        yield return new WaitForSeconds(1f);

        Scene5.SetActive(true);

        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.7f);

        Result.SetActive(true);
    }

}
