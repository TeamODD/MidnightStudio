using UnityEngine;
using UnityEngine.UI;

public class CompleteButtonControl : MonoBehaviour
{
    public Button targetButton;
    public DialogueParser parser;

    private float clickDelay = 0.2f;
    private float lastClickTime = 0f;
    private int clickCount = 0;

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OnClickHandler);
        }
    }

    void OnClickHandler()
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;

        if (timeSinceLastClick <= clickDelay)
        {
            clickCount++;

            if (clickCount == 2)
            {
                OnDoubleClick();
                clickCount = 0;
            }
        }
        else
        {
            clickCount = 1; // 새 클릭 시작
        }
    }

    void OnDoubleClick()
    {
        Debug.Log("버튼 더블클릭 감지됨!");
        parser.CutProduction();
    }
}
