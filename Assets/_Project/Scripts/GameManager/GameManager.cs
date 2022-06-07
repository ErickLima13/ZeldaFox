using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState
{
    IDLE,
    ALERT,
    PATROL,
    FOLLOW,
    FURY
}

public class GameManager : MonoBehaviour
{
    public Transform player;

    [Header("SlimeIA")]
    public GameObject[] slimeWayPoints;
    public float slimeIdleWaitTime = 5f;
    public float slimeDistanceToAttack = 2.3f;
    public float slimeAlertTime = 3f;
    public float slimeAttackDelay = 1f;

    private void Initialization()
    {
        slimeWayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
