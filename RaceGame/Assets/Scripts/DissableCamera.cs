using UnityEngine;

public class DissableCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    public void RemoveCamera()
    {
        camera.enabled = false;
    }
    public void AddCamera()
    {
        camera.enabled = true;
    }
}
