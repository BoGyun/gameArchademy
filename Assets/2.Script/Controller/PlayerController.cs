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
    public float moveSpeed = 40f;
    public float turnSpeed = 5f;

    Rigidbody _rigidbody;
    Vector3 _movement;
    Camera _mainCamera;

    public float detectionRadius = 5f;
    public GameObject bulletPrefab;   // 총알 프리팹
    public Transform firePoint;       // 총구 위치 (플레이어 앞)
    public float bulletSpeed = 10f;   // 총알의 속도
    EPlayerState _currentState;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;

        Singleton<ObjectPool>.Inst.SetPool(bulletPrefab, 50);
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = _mainCamera.transform.forward;
        Vector3 right = _mainCamera.transform.right;

        // ī�޶��� y�� ȸ���� �������� �յ��¿� ������ ����
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        _movement = (forward * vertical + right * horizontal).normalized;


        // �̵�
        transform.position += _movement * moveSpeed * Time.deltaTime;

        // ȸ��
        if (_movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //����
            _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {

        GameObject bullet = Singleton<ObjectPool>.Inst.GetObject(5f);

        bullet.transform.position = firePoint.position;

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = firePoint.forward * bulletSpeed;
        }


    }


    void OnDrawGizmos()
    {

        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, detectionRadius);
        }
    }
}