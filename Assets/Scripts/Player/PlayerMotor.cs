using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    //:> vector tốc độ nì lại còn ba chiều :>
    private Vector3 playerVelocity;
    public bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;

    public float JumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //lấy thông tin của thành phần "CharacterController" đính kèm
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    // quá trình di chuyển, do input là 1 vector(mô phỏng trên hình tròn lượng giác) 
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        //bug 1 do gợi ý của vscode gán giá trị của y cho y nên sinh ra bug
        moveDirection.z = input.y;
        
        //nào gọi hàm di chuyển trên bộ ddkhien nhân vật :> 
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0 )
        {
            //vector hg xuống
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        //Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            // quảng đường trong chuyển động chậm dần đều ((Vsau) ^ 2 - (v0) ^ 2) = 2aS
            //playerVeclocity là vector vận tốc chuyển động chậm dần đều

            playerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * gravity);
        }
    }    
}
