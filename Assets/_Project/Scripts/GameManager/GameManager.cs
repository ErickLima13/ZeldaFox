using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState
{
    IDLE,
    ALERT,
    EXPLORE,
    PATROL,
    FOLLOW,
    FURY
}

public class GameManager : MonoBehaviour
{
    [Header("SlimeIA")]
    public GameObject[] slimeWayPoints;

    private void Initialization()
    {
        slimeWayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
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
