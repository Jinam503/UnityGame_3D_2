using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float lookSpeed;
    [SerializeField] private float camRotateLimit;
    private float currentCamRotateX = 0f;

    [SerializeField] Camera cam;

    private Animator anim;

    private CharacterController characterController;

    [SerializeField]private float jumpPower;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float vel;
    private Vector3 moveDir;

    private bool canAttack = true;
    //private int numOfJumps;
    //[SerializeField] private int maxNumOfJumps = 2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        moveDir = Vector3.zero;
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CharacterRotation();
        CameraRotate();
        CharacterAnimation();
        Jump();
        ApplyGravity();
        SwordAttack();
        
        characterController.Move(moveDir * Time.deltaTime);
    }
    private void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            anim.SetTrigger("Attack");
            canAttack = false;
        }
    }
    private void ApplyGravity()
    {
        if( IsCheckGrounded() && vel < 0.0f)
        {
            vel = -1.0f;
        }
        else
        {
            vel += gravity * gravityMultiplier * Time.deltaTime;
        }
        moveDir.y = vel;
    }   
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsCheckGrounded()) return;
            //if (!IsCheckGrounded() && numOfJumps >= maxNumOfJumps) return;
            //if (numOfJumps == 0) StartCoroutine(WaitForLanding());
            //numOfJumps++;
            vel = jumpPower;
        }

    }
    //private IEnumerator WaitForLanding()
    //{
    //    yield return new WaitUntil(() => !IsCheckGrounded());
    //    yield return new WaitUntil(IsCheckGrounded);
    //
    //    //numOfJumps = 0;
    //}
    private void CharacterAnimation()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        anim.SetFloat("Ver", ver);
        anim.SetFloat("Hor", hor);
    }
    private void CharacterRotation()
    {
        float yR = Input.GetAxisRaw("Mouse X");
        Vector3 vec = new Vector3(0f, yR, 0f) * lookSpeed;
        transform.Rotate(vec);

    }
    private void CameraRotate()
    {
        float xR = Input.GetAxisRaw("Mouse Y");
        float camRX = xR * lookSpeed;
        currentCamRotateX -= camRX;
        currentCamRotateX = Mathf.Clamp(currentCamRotateX, -camRotateLimit, camRotateLimit);

        cam.transform.localEulerAngles = new Vector3(currentCamRotateX, 0f, 0f);
    }
    private bool IsCheckGrounded()
    {
        
        // CharacterController.IsGrounded가 true라면 Raycast를 사용하지 않고 판정 종료
        if (characterController.isGrounded) return true;
        // 발사하는 광선의 초기 위치와 방향
        // 약간 신체에 박혀 있는 위치로부터 발사하지 않으면 제대로 판정할 수 없을 때가 있다.
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        // 탐색 거리
        var maxDistance = 0.2f;
        // 광선 디버그 용도
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        // Raycast의 hit 여부로 판정
        // 지상에만 충돌로 레이어를 지정
        return Physics.Raycast(ray, maxDistance, layerMask);
    }
    public void CanAttack()
    {
        canAttack = true;
    }
}
