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

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleFire();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        // 카메라의 y축 회전을 기준으로 앞뒤좌우 방향을 설정
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        m_Movement = (forward * vertical + right * horizontal).normalized;


        // 이동
        transform.position += m_Movement * moveSpeed * Time.deltaTime;

        // 회전
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
            //점프
            m_Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    void HandleFire()
    {
        if (Input.GetMouseButtonDown(0))
        {


        }
    }

}