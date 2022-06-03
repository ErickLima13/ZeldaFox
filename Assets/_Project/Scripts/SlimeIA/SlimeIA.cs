using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeIA : MonoBehaviour
{
    private GameManager _gameManager;

    private Animator animator;

    [SerializeField] private ParticleSystem fxHit;

    [SerializeField] private int HP;

    private bool isDie;

    [SerializeField] enemyState state;

    public const float idleWaitTime = 3f;
    public const float patrolWaitTime = 5f;

    //IA
    private NavMeshAgent agent;

    private Vector3 destination;

    private int idWaypoint;

    private void Initialization()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ChangeState(state);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        StateManager();
    }

    #region My metods

    private IEnumerator Died()
    {
        isDie = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void GetHit(int amount)
    {
        if (isDie)
        {
            return;
        }

        HP -= amount;

        if (HP > 0)
        {            
            animator.SetTrigger("GetHit");
            fxHit.Emit(Random.Range(5, 10));
        }
        else
        {
            animator.SetTrigger("Die");
            StartCoroutine(nameof(Died));
        }
    }


    private void StateManager()
    {
        switch (state)
        {
            case enemyState.IDLE:

                break;
            case enemyState.ALERT:

                break;
            case enemyState.EXPLORE:

                break;
            case enemyState.FOLLOW:

                break;
            case enemyState.FURY:

                break;
            case enemyState.PATROL:

                break;
        }


    }

    private void ChangeState(enemyState newState)
    {
        StopAllCoroutines();
        state = newState;
        print(newState);

        switch (state)
        {
            case enemyState.IDLE:
                destination = transform.position;
                agent.destination = destination;
                StartCoroutine(nameof(IDLE));
                break;
            case enemyState.ALERT:

                break;
            case enemyState.EXPLORE:

                break;
            case enemyState.FOLLOW:

                break;
            case enemyState.FURY:

                break;
            case enemyState.PATROL:
                idWaypoint = Random.Range(0, _gameManager.slimeWayPoints.Length);
                destination = _gameManager.slimeWayPoints[idWaypoint].transform.position;
                agent.destination = destination;
                StartCoroutine(nameof(PATROL));
                break;
        }
    }

    private IEnumerator IDLE()
    {
        yield return new WaitForSeconds(idleWaitTime);
        StayStiil(50);

    }

    private IEnumerator PATROL()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        StayStiil(30);


    }

    private int RandomRange()
    {
        int random = Random.Range(0, 100);
        return random;
    }

    private void StayStiil(int yes)
    {
        if(RandomRange() <= yes)
        {
            ChangeState(enemyState.IDLE);
        }
        else
        {
            ChangeState(enemyState.PATROL);
        }
    }

    #endregion

}
