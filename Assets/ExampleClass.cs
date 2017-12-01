using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    UnityEvent m_MyEvent;

    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        m_MyEvent.AddListener(Ping);
        m_MyEvent.AddListener(Ping1);
    }

    void Update()
    {
        if (Input.anyKeyDown && m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }
    }

    void Ping()
    {
        Debug.Log("Ping");
    }
    void Ping1()
    {
        Debug.Log("Ping");
    }
}