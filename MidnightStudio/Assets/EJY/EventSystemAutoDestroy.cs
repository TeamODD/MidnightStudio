using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemAutoDestroy : MonoBehaviour
{
    void Awake()
    {
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1)
        {
            // 가장 먼저 생성된 것을 남기고, 자신이면 파괴
            if (systems[0] != GetComponent<EventSystem>())
            {
                Destroy(gameObject);
            }
        }
    }
}
