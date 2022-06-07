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

    //IA
    private NavMeshAgent agent;

    private Vector3 destination;

    private int idWaypoint;

    private bool isWalk;
    private bool isAlert;
    private bool isPlayerVisible;
    private bool isAttack;

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
        Walk();
        StateManager();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerVisible = true;

            if(state == enemyState.IDLE || state == enemyState.PATROL)
            {
                ChangeState(enemyState.ALERT);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerVisible = false;
        }
    }

    #region My metods

    private IEnumerator Died()
    {
        isDie = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void Walk()
    {
        if (agent.desiredVelocity.magnitude >= 0.1f)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isAlert", isAlert);
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
            ChangeState(enemyState.FURY);
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
            case enemyState.FOLLOW:
                destination = _gameManager.player.position;
                agent.destination = destination;

                if(agent.remainingDistance <= agent.stoppingDistance)
                {
                    Attack();
                }

                break;
            case enemyState.FURY:
                destination = _gameManager.player.transform.position;
                agent.destination = destination;

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Attack();
                }

                break;
            case enemyState.PATROL:

                break;
        }


    }

    private void ChangeState(enemyState newState)
    {
        StopAllCoroutines();
        isAlert = false;
        state = newState;

        switch (newState)
        {
            case enemyState.IDLE:
                agent.stoppingDistance = 0;
                destination = transform.position;
                agent.destination = destination;
                StartCoroutine(nameof(IDLE));
                break;
            case enemyState.ALERT:
                agent.stoppingDistance = 0;
                destination = transform.position;
                agent.destination = destination;
                isAlert = true;
                StartCoroutine(nameof(ALERT));
                break;
            case enemyState.FOLLOW:
                agent.stoppingDistance = _gameManager.slimeDistanceToAttack;
                StartCoroutine(nameof(FOLLOW));
                break;
            case enemyState.FURY:
                agent.stoppingDistance = _gameManager.slimeDistanceToAttack;
                break;
            case enemyState.PATROL:
                agent.stoppingDistance = 0;
                idWaypoint = Random.Range(0, _gameManager.slimeWayPoints.Length);
                destination = _gameManager.slimeWayPoints[idWaypoint].transform.position;
                agent.destination = destination;
                StartCoroutine(nameof(PATROL));
                break;
        }

        
    }

    private IEnumerator IDLE()
    {
        yield return new WaitForSeconds(_gameManager.slimeIdleWaitTime);
        StayStiil(50);

    }

    private IEnumerator PATROL()
    {
        yield return new WaitUntil(() => agent.remainingDistance <= 0);
        StayStiil(30);
    }

    private IEnumerator FOLLOW()
    {
        yield return new WaitUntil(() => !isPlayerVisible);

        print("perdi");

        yield return new WaitForSeconds(_gameManager.slimeAlertTime);

        StayStiil(50);
    }

    private IEnumerator ALERT()
    {
        yield return new WaitForSeconds(_gameManager.slimeAlertTime);
        if (isPlayerVisible)
        {
            ChangeState(enemyState.FOLLOW);
        }
        else
        {
            StayStiil(10);
        }
    }

    private IEnumerator ATTACK()
    {
        yield return new WaitForSeconds(_gameManager.slimeAttackDelay);
        isAttack = false;
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

    private void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            animator.SetTrigger("Attack");
        }
    }

    private void AttackIsDone()
    {
        StartCoroutine(nameof(ATTACK));
    }

    #endregion

}
