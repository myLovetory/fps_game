using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


///nơi này để lưu các thông tin từ đầu vào
public class InputManager : MonoBehaviour
{
    private PlayerInput playerinput;
    public PlayerInput.OnFootActions onFoot;
    
    
    private PlayerMotor motor;
    private PlayerLook look;

    private void Awake()
    {
        playerinput = new PlayerInput();
        onFoot = playerinput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        //gọi lambda expression Jump từ motor
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //cho cái tk player motor nơi mà lưu chuyển động nhân vật(cụ thể là nhét giá trị vào cho nó)
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    //hàm nhìn
    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    // hai trạng thái di chuyển của nvat
    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
