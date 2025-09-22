using UnityEngine;
using UnityEngine.Events;

public class UnityEventInvoker : MonoBehaviour
{
    public UnityEvent eventToInvoke;

    public void InvokeEvent()
    {
        eventToInvoke.Invoke();
    }
}
