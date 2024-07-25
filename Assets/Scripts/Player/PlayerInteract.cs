using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;

    
    private PlayerUi playerUi;
    private InputManager inputManager;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUi = GetComponent<PlayerUi>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUi.UpdateText(string.Empty);

        //tạo 1 tia giữa camera
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; //biến lưu trữ truy cập raycast
        //chỉ chạy nếu raycast oánh trúng j đó

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //cập nhật thông điệp in ra màn hình

                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUi.UpdateText(interactable.promptMessenge);
                
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
           
        }
    }
}
