using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastpoistion;

    [SerializeField]
    private LayerMask placementLayerMask;

    //public GameObject mouseUI;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition; //mouseUI.transform.position;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100, placementLayerMask))
        {
            lastpoistion = hit.point;
        }
        return lastpoistion;
    }
}
