using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class TrapSpawner : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Image trapPreviewSprite;
    public List<TrapScriptableObject> traps;

    private MeshRenderer mesh;

    private float transformY;
    private Vector2 movementInput;
    private Rigidbody rb;
    private TrapScriptableObject trap;
    [SerializeField] private PlayerInput playerInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // prevent tipping over
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // better collision
        
        mesh = GetComponent<MeshRenderer>();
        ChooseTrapsForTheRound();
        transformY = transform.position.y;
    }

    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(movementInput.y, 0f, movementInput.x);
        Vector3 targetPos = rb.position + move * movementSpeed * Time.fixedDeltaTime;
        targetPos.y = transformY;
        rb.MovePosition(targetPos);
    }

    public void ChooseTrapsForTheRound()
    {
        List<TrapScriptableObject> trapsForRound = new List<TrapScriptableObject>();
        for (int i = 0; i < 3; i++)
        {
            trapsForRound.Add(traps[Random.Range(0, traps.Count-1)]);
            print(trapsForRound[i]);
        }

        StartCoroutine(WaitToPlaceATrap(trapsForRound));
    }

    public IEnumerator WaitToPlaceATrap(List<TrapScriptableObject> trapsForRound)
    {
        mesh.enabled = false;

        for (int i = 0;i < trapsForRound.Count;i++)
        {
            trap = trapsForRound[i];
            GameObject previewInstance = Instantiate(trap.object3D, this.transform, worldPositionStays: false);
            previewInstance.transform.localPosition = Vector3.zero;
            previewInstance.transform.localRotation = Quaternion.identity;

            //MakeTheTrapGhost(previewInstance);

            //SetGhostColor(new Color(0, 1, 0, 0.5f));

            //SphereCollider previewTrapCollider = previewInstance.GetComponent<SphereCollider>();

            if (trap.uiPicture != null)
            {
                int num = i;
                trapPreviewSprite.sprite = trapsForRound[num++].uiPicture;
            }

            yield return StartCoroutine(PlaceTrap(playerInput));

            //SetGhostColor(new Color(1, 1, 1, 1));
            //SphereCollider sphereCollider = placedTrap.GetComponent<SphereCollider>();
            //sphereCollider.enabled = false;
            Destroy(previewInstance);
        }

      

        mesh.enabled = true;
    }

    private static void MakeTheTrapGhost(GameObject previewInstance)
    {
        Renderer[] renderers = previewInstance.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = 0.5f;
            mat.color = color;

            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;

        }
    }

    private IEnumerator PlaceTrap(PlayerInput playerInput)
    {
        InputAction placeAction = playerInput.actions["PlaceTrap"];

        yield return null;

        yield return new WaitUntil(() => placeAction.WasPerformedThisFrame());

        Instantiate(trap.object3D, transform.position, Quaternion.identity);
    }

    void SetGhostColor(Color color)
    {
        Renderer[] renderers = trap.object3D.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.sharedMaterial;
            mat.color = color;

        }
    }

    
}
