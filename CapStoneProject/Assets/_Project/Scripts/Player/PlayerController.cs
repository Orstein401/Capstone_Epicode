using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Componets")]
    private CharacterController cc;
    private PlayerAnimation anim;
    [SerializeField] private Transform cam;

    [Header("Input")]
    private float horizontal;
    private float vertical;
    private bool run;
    private bool jump;

    [Header("Movement")]
    private float gravity = -9.81f;
    private Vector3 direction;
    private Vector3 velocity;
    private bool isActive = true;

    [Header("Parametres")]
    [SerializeField] private float jumpHeigth = 0.5f;
    [SerializeField] private float speedRotation = 3.5f;
    [SerializeField] private float minSpeed = 3.5f;
    [SerializeField] private float maxSpeed = 5.5f;
    private float currentSpeed;

    [Header("CheckGround")]
    [SerializeField] private Transform checkerGround;
    [SerializeField] private float radiusChecker;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = false;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<PlayerAnimation>();
    }
    private void Start()
    {
        currentSpeed = minSpeed;
    }
    private void Update()
    {
        if (!DialogueManager.Instance.IsDialoguePlaying())
        {
            if (isActive)
            {
                InputPlayer();
            }
            Gravity();
            Jump();
            CalculateVelocity();
            CalculateRotation();
            cc.Move(velocity * Time.deltaTime);
        }
        else
        {
            run = false;
            jump = false;
            direction = Vector3.zero;
        }
        anim.UpdateStates(direction, run, isGrounded);
    }
    private void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        run = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKeyDown(KeyCode.Space);
    }
    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(checkerGround.position, radiusChecker, groundLayer);
        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = 0f;
            return;
        }
        velocity.y += gravity * Time.deltaTime;
    }
    private void Jump()
    {
        if (jump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigth * -2f * gravity);
            anim.TriggerJump();
        }
    }
    private void CalculateDirection()
    {
        direction = cam.forward * vertical + cam.right * horizontal;
        direction.y = 0f;
        direction.Normalize();
    }
    private void CalculateRotation()
    {
        Vector3 lookDirection = cam.forward;
        lookDirection.Normalize();
        lookDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }
    private void CalculateVelocity()
    {
        currentSpeed = run ? maxSpeed : minSpeed;
        CalculateDirection();
        velocity = new Vector3(direction.x * currentSpeed, velocity.y, direction.z * currentSpeed);
    }
    public void ActiveOrDisactiveInput()
    {
        isActive = !isActive;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(checkerGround.position, radiusChecker);
    }
}
