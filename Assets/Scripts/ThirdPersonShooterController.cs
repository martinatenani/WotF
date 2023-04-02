using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

//Nota: con il metodo hitscan il proiettile non è visibile, si vede solo l'effetto finale dell'impatto.

/// <summary>
/// Script controls 3rd person shooter elements: aim camera, aim carachter movement...
/// DOES NOT COVER carachter base movement.
/// </summary>
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    [SerializeField] private Transform debugTransform;

    //questo può essere riassunto in una struttura? prefab + posizione
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spnBulletPosition;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Raycaster - we use the center of the screen because without a mouse connected the game would break.
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        //Hitscan method for the projectile
        Transform hitTransform = null;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) 
        {
            debugTransform.transform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;

            //Hitscan
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y; //left-right rotation
            //up-down rotation?
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); //lerp to give smoothness
        }
        else 
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
        
        if (starterAssetsInputs.shoot)
        {
            //Hitscan
            if(hitTransform != null)
            {
                //hit something
                if(hitTransform.GetComponent<EnemyTarget>() != null)
                {
                    Debug.Log("hit enemy!");
                }
                else
                {
                    Debug.Log("hit other.");
                }
            }
            //Vector3 aimDir = (mouseWorldPosition - spnBulletPosition.position).normalized;
            //Instantiate(pfBulletProjectile,spnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }
        
    }
}
