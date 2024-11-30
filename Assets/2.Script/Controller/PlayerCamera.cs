using UnityEngine;
using Unity.Cinemachine;




public class PlayerCamera : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Ʈ������
    public Vector3 offset;    // ī�޶�� �÷��̾� ������ �Ÿ�
    public float smoothSpeed = 0.125f;  // ī�޶��� �ε巯�� �̵� �ӵ�

    void LateUpdate()
    {
        // �÷��̾��� ��ġ�� offset�� ���� ��ǥ ��ġ�� ���
        Vector3 desiredPosition = player.position + offset;

        // �ε巴�� ��ǥ ��ġ�� ī�޶� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // ī�޶��� ��ġ�� ��ǥ ��ġ�� ����
        transform.position = smoothedPosition;

        // ī�޶� �׻� �÷��̾ �ٶ󺸰� ����
        transform.LookAt(player);
    }
}
