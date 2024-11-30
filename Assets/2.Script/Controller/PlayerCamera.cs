using UnityEngine;
using Unity.Cinemachine;




public class PlayerCamera : MonoBehaviour
{
    public Transform player;  // 플레이어의 트랜스폼
    public Vector3 offset;    // 카메라와 플레이어 사이의 거리
    public float smoothSpeed = 0.125f;  // 카메라의 부드러운 이동 속도

    void LateUpdate()
    {
        // 플레이어의 위치에 offset을 더한 목표 위치를 계산
        Vector3 desiredPosition = player.position + offset;

        // 부드럽게 목표 위치로 카메라를 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 카메라의 위치를 목표 위치로 설정
        transform.position = smoothedPosition;

        // 카메라가 항상 플레이어를 바라보게 설정
        transform.LookAt(player);
    }
}
