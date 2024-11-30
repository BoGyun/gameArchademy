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

        EventManager.Instance.EnrollEvent(Events.OnScoreUpgrade, () =>
        {
            //������ ���� 
        });
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Singleton<ObjectPool>.Inst.ReturnObject(collision.gameObject);
            EventManager.Instance.TriggerEvent(Events.OnScoreUpgrade);
        }
    }


}
