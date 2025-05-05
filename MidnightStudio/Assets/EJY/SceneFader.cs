using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    /// <summary>
    /// �Ϲ����� �� ��ȯ (��� LoadScene, ������ ��쿡 ���)
    /// </summary>
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(FadeIn());
    }

    /// <summary>
    /// �ε巯�� �� ��ȯ (Additive�� ���� �� �ε� �� ���� �� ��ε�)
    /// </summary>
    public void FadeToSceneAdditive(string sceneName)
    {
        StartCoroutine(FadeAndLoadAdditive(sceneName));
    }

    private IEnumerator FadeAndLoadAdditive(string nextScene)
    {
        // 1. Fade Out
        yield return StartCoroutine(FadeOut());

        // 2. Additive�� �� �� �ε�
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        while (!async.isDone)
            yield return null;

        // 3. �� ���� Ȱ��ȭ ������ ����
        Scene newScene = SceneManager.GetSceneByName(nextScene);
        SceneManager.SetActiveScene(newScene);

        // 4. ���� ���� ��ε�
        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene != newScene && scene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        // 5. Fade In
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0f, 1f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(1f);
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1f, 0f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }

    public void FadeInNow()
    {
        StartCoroutine(FadeIn());
    }
}
