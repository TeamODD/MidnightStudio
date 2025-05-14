using UnityEngine;
using System.Collections;
using UnityEditor.Rendering.Universal;

public class ResultCharaPoyong : MonoBehaviour
{
    public Obj_Production Obj;

    private void Start()
    {
        StartCoroutine(PlayAnime());
    }

    private IEnumerator PlayAnime()
    {
        while (true)
        {
            Obj.Move("Smooth", true, 2f, "y", -1.94f, -2.1f);
            Obj.Scale("Smooth", 2f, new Vector3(0.9275f, 0.9275f, 1f), new Vector3(0.9275f, 0.875f, 1f));
            yield return new WaitForSeconds(2);

            Obj.Move("Smooth", true, 2f, "y", -2.1f, -1.94f);
            Obj.Scale("Smooth", 2f, new Vector3(0.9275f, 0.875f, 1f), new Vector3(0.9275f, 0.9275f, 1f));
            yield return new WaitForSeconds(2f);
        }
    }
}
