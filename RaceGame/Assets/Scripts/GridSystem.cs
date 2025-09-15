using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject objectToPlace;
    public float gridSize = 1f;
    public Camera gridCamera;
    private GameObject ghostObject;
    private HashSet<Vector3> occupiedPosition = new HashSet<Vector3>();

    

    private void Start()
    {
        CreateGhostObject();
    }

    private void Update()
    {
        UpdateGhostPosition();
        if(Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }
    void CreateGhostObject()
    {
        ghostObject =Instantiate(objectToPlace);
        ghostObject.GetComponent<Collider>().enabled = false;

        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = 0.5f;
            mat.color = color;

            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend",(int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_APHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;

        }
    }

    void UpdateGhostPosition()
    {
        Ray ray =gridCamera.ScreenPointToRay(Input.mousePosition);//or mouseCursorJoystick.transform.position

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;

            Vector3 snappedPostion = new Vector3(

                Mathf.Round(point.x/gridSize)*gridSize,
                Mathf.Round(point.y/gridSize)*gridSize,
                Mathf.Round(point.z/gridSize)*gridSize
                );

            ghostObject.transform.position = snappedPostion;

            if (occupiedPosition.Contains(snappedPostion))
            {
                SetGhostColor(new Color(1, 0, 0, 0.5f));
            }
            else
            {
                SetGhostColor(new Color(0,1,0,0.5f));
            }
            
        }
    }

    void SetGhostColor(Color color)
    {
        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            mat.color = color;

        }
    }

    void PlaceObject()
    {
        Vector3 placementPosition = ghostObject.transform.position;

        if (!occupiedPosition.Contains(placementPosition))
        {
            Instantiate(objectToPlace,placementPosition, Quaternion.identity);

            occupiedPosition.Add(placementPosition);
        }
    }
}
