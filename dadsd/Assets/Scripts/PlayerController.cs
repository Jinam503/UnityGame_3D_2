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

    [SerializeField]private TrailRenderer trailEffect;
    [SerializeField]private BoxCollider meleeArea;
    //private int numOfJumps;
    //[SerializeField] private int maxNumOfJumps = 2;
    // Start is called before the first frame update

    float hor;
    float ver;

    bool jump_k;
    bool attack_k;

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
        GetInput();
        CharacterRotation();
        CameraRotate();
        CharacterAnimation();
        Jump();
        ApplyGravity();
        Attack();
        
        characterController.Move(moveDir * Time.deltaTime);
    }
    void GetInput()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        jump_k = Input.GetButtonDown("Jump");
        attack_k = Input.GetButtonDown("Fire1");
    }
    private void Attack()
    {
        if (attack_k && PlayerData.Instance.CanDamage)
        {
            StartCoroutine(Swing());
            PlayerData.Instance.CanDamage = false;
        }
    }
    IEnumerator Swing()
    {
        if (PlayerData.Instance.CanDamage)
        {
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.1f);
            meleeArea.enabled = true;
            trailEffect.enabled = true;
            yield return new WaitForSeconds(0.3f);
            
            yield return new WaitForSeconds(0.05f);
            meleeArea.enabled = false;
            trailEffect.enabled = false;
            
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
        if (jump_k)
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
        if (characterController.isGrounded) return true;
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        var maxDistance = 0.2f;
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        return Physics.Raycast(ray, maxDistance, layerMask);
    }
    public void FinSwing()
    {
        PlayerData.Instance.CanDamage = true;
    }
}
