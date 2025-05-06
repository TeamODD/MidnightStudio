using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemAutoDestroy : MonoBehaviour
{
    void Awake()
    {
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1)
        {
            // ���� ���� ������ ���� �����, �ڽ��̸� �ı�
            if (systems[0] != GetComponent<EventSystem>())
            {
                Destroy(gameObject);
            }
        }
    }
}
