using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    #region REFERENCES
    [Header("Referencias")]
    private NavMeshAgent agent;
    #endregion

    #region STATE VARIABLES
    [Header("IdleState")]
    public float idleTime;
    private float elapsedIdleTime;
    #endregion

    #region PATROL POINTS
    [Header("Patrol")]
    public Transform[] PatrolPointsArray;
    #endregion

    public enum STATE
    {
        Idle,
        Walking
    }

    public STATE currentState;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (currentState == STATE.Idle)
        {
            elapsedIdleTime += Time.deltaTime;
            if (elapsedIdleTime >= idleTime)
            {
                elapsedIdleTime = 0;
                ChangeState(STATE.Walking);
            }
        }
        else if (currentState == STATE.Walking)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                ChangeState(STATE.Idle);
            }
        }
    }

    private void ChangeState(STATE newState)
    {
        currentState = newState;
        if (newState == STATE.Walking)
        {
            agent.SetDestination(PatrolPointsArray[Random.Range(0, PatrolPointsArray.Length)].position);
        }
    }
}
