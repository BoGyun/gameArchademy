using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;         // 플레이어의 트랜스폼
    public NavMeshAgent agent;       // AI 캐릭터의 NavMeshAgent

    public float stoppingDistance = 2f; // 플레이어에 접근할 수 있는 최소 거리

    void Start()
    {
        // NavMeshAgent 컴포넌트 가져오기
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        // 플레이어가 존재하면 추적
        if (player != null)
        {
            // 플레이어의 위치로 이동
            agent.SetDestination(player.position);

            // 일정 거리 내에 들어오면 추적을 멈추도록 설정
            if (Vector3.Distance(transform.position, player.position) <= stoppingDistance)
            {
                agent.isStopped = true;  // 이동을 멈추기
            }
            else
            {
                agent.isStopped = false; // 다시 이동 시작
            }
        }
    }
}
