using UnityEngine;

public enum EPlayerState
{
    Idle,
    Move,
    Jump,
    Attack
}

public class PlayerController : MonoBehaviour
{

    public EPlayerState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (value == CurrentState)
                return;

            _currentState = value;
            switch (_currentState)
            {
                case EPlayerState.Idle:
                    HandleIdle();
                    break;
                case EPlayerState.Jump:
                    HandleJump();
                    break;
                case EPlayerState.Attack:
                    HandleAttack();
                    break;
            }
        }
    }

    public float moveSpeed = 5f;            // �̵� �ӵ�
    public float jumpHeight = 2f;           // ���� ����
    public float gravity = -9.8f;           // �߷�
    public float attackRange = 2f;          // ���� ����
    public float attackCooldown = 1f;       // ���� ��Ÿ��
    public LayerMask enemyLayer;            // �� ���̾�

    private CharacterController characterController;
    private Vector3 velocity;               // ĳ������ �̵� ����
    private bool isGrounded;                // ĳ���Ͱ� �ٴڿ� ��Ҵ��� ����
    private float attackTimer = 0f;         // ���� ��Ÿ�� Ÿ�̸�

    private Vector3 _moveDirection = Vector3.zero;
    private EPlayerState _currentState = EPlayerState.Idle;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();

        HandleMovement();


        //�����̽��ٸ� Ŭ���� ��� ����
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CurrentState = EPlayerState.Jump;
        }

        //��Ŭ���� �� ��� ���� ���·� ��ȯ
        if (Input.GetMouseButtonDown(0))
        {
            CurrentState = EPlayerState.Attack;
        }

    }

    void GetInput()
    {
        if (CurrentState == EPlayerState.Idle || CurrentState == EPlayerState.Move)
        {
            float moveX = Input.GetAxis("Horizontal"); // A/D Ű (�Ǵ� ȭ��ǥ ��/������)
            float moveZ = Input.GetAxis("Vertical");   // W/S Ű (�Ǵ� ȭ��ǥ ��/�Ʒ�)

            _moveDirection = transform.right * moveX + transform.forward * moveZ;

            //_moveDirection�� �α� ��� (������)
            Debug.Log(_moveDirection);

            if (_moveDirection != Vector3.zero)
                CurrentState = EPlayerState.Move;
            else
                CurrentState = EPlayerState.Idle;
        }
    }

    void HandleIdle()
    {

    }

    void HandleMovement()
    {
        if(CurrentState == EPlayerState.Move)
        {
            characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
            _moveDirection = Vector3.zero;
        }
    }

    void HandleJump()
    {
        Debug.Log("Jump!");
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �ٴڿ� ���� ���� ���� �ӵ� �ʱ�ȭ
        }

        if (Input.GetButtonDown("Jump") && isGrounded) // Space Ű�� ����
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;  // �߷� ����
        characterController.Move(velocity * Time.deltaTime);

    }

    void HandleAttack()
    {
            Attack();
    }

    void Attack()
    {
        Debug.Log("Attack!");
        //attackTimer = attackCooldown;

        //// ���� ���� �ȿ� ���� �ִٸ�
        //Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        //foreach (Collider enemy in hitEnemies)
        //{
        //    Debug.Log("Enemy Hit: " + enemy.name);
        //    // ���⼭ ������ �������� �� �� ����
        //}
    }

    // ���� ������ �ð������� Ȯ���� �� �ֵ��� Gizmos�� �׷���
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}