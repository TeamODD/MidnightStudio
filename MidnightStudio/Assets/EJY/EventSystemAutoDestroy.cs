using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemAutoDestroy : MonoBehaviour
{
    void Start()
    {
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1)
        {
            foreach (EventSystem es in systems)
            {
                if (es != EventSystem.current)
                {
                    Debug.Log("�ߺ� EventSystem ���ŵ�: " + es.gameObject.name);
                    Destroy(es.gameObject);
                }
            }
        }
    }
}
