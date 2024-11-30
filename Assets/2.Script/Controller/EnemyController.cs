using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;         // �÷��̾��� Ʈ������
    public NavMeshAgent agent;       // AI ĳ������ NavMeshAgent

    public float stoppingDistance = 2f; // �÷��̾ ������ �� �ִ� �ּ� �Ÿ�

    void Start()
    {
        // NavMeshAgent ������Ʈ ��������
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        // �÷��̾ �����ϸ� ����
        if (player != null)
        {
            // �÷��̾��� ��ġ�� �̵�
            agent.SetDestination(player.position);

            // ���� �Ÿ� ���� ������ ������ ���ߵ��� ����
            if (Vector3.Distance(transform.position, player.position) <= stoppingDistance)
            {
                agent.isStopped = true;  // �̵��� ���߱�
            }
            else
            {
                agent.isStopped = false; // �ٽ� �̵� ����
            }
        }
    }
}
