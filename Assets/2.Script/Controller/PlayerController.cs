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

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Camera mainCamera;

    // ���� ���� ����
    public float detectionRadius = 5f;  // ���� ���� �ݰ�
    public Transform firePoint;         // �÷��̾��� ���� ��ġ (��: �ѱ��� �߻� ����)

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
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

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        // ī�޶��� y�� ȸ���� �������� �յ��¿� ������ ����
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        m_Movement = (forward * vertical + right * horizontal).normalized;


        // �̵�
        transform.position += m_Movement * moveSpeed * Time.deltaTime;

        // ȸ��
        if (m_Movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(m_Movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //����
            m_Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider[] hitColliders = Physics.OverlapSphere(firePoint.position, detectionRadius);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    // Enemy ������Ʈ�� �����ϸ� �α� ���
                    Debug.Log("Enemy detected: " + hitCollider.name);
                }
            }

        }
    }


    void OnDrawGizmos()
    {
        // ������� ���� ��ü���� ������ �� �信�� �� �� �ְ� Gizmo�� ǥ��
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, detectionRadius);
        }
    }
}