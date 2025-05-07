using UnityEngine;

public class QuestionProduction : MonoBehaviour
{
    public UI_Production[] circle = new UI_Production[3];
    public UI_Production[] text = new UI_Production[3];

    public void objectAppear()
    {
        foreach (UI_Production obj in text)
        {
            obj.Alpha("Lerp", 0.1f, 0f, 1f);
        }
        foreach (UI_Production obj in circle)
        {
            obj.Alpha("Lerp", 0.1f, 0f, 1f);
        }
        
    }
    public void objectDisappear()
    {
        foreach (UI_Production obj in text)
        {
            obj.Alpha("Lerp", 0.1f, 1f, 0f);
        }
        foreach (UI_Production obj in circle)
        {
            obj.Alpha("Lerp", 0.1f, 1f, 0f);
        }
    }
}


