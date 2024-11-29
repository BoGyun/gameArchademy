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

    public float moveSpeed = 5f;            // 이동 속도
    public float jumpHeight = 2f;           // 점프 높이
    public float gravity = -9.8f;           // 중력
    public float attackRange = 2f;          // 공격 범위
    public float attackCooldown = 1f;       // 공격 쿨타임
    public LayerMask enemyLayer;            // 적 레이어

    private CharacterController characterController;
    private Vector3 velocity;               // 캐릭터의 이동 벡터
    private bool isGrounded;                // 캐릭터가 바닥에 닿았는지 여부
    private float attackTimer = 0f;         // 공격 쿨타임 타이머

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


        //스페이스바를 클릭한 경우 점프
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CurrentState = EPlayerState.Jump;
        }

        //좌클릭을 한 경우 공격 상태로 전환
        if (Input.GetMouseButtonDown(0))
        {
            CurrentState = EPlayerState.Attack;
        }

    }

    void GetInput()
    {
        if (CurrentState == EPlayerState.Idle || CurrentState == EPlayerState.Move)
        {
            float moveX = Input.GetAxis("Horizontal"); // A/D 키 (또는 화살표 왼/오른쪽)
            float moveZ = Input.GetAxis("Vertical");   // W/S 키 (또는 화살표 위/아래)

            _moveDirection = transform.right * moveX + transform.forward * moveZ;

            //_moveDirection의 로그 출력 (디버깅용)
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
            velocity.y = -2f; // 바닥에 있을 때는 수직 속도 초기화
        }

        if (Input.GetButtonDown("Jump") && isGrounded) // Space 키로 점프
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;  // 중력 적용
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

        //// 공격 범위 안에 적이 있다면
        //Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        //foreach (Collider enemy in hitEnemies)
        //{
        //    Debug.Log("Enemy Hit: " + enemy.name);
        //    // 여기서 적에게 데미지를 줄 수 있음
        //}
    }

    // 공격 범위를 시각적으로 확인할 수 있도록 Gizmos로 그려줌
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}