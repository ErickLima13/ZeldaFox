using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

    [Header("Rain Manager")]
    public PostProcessVolume postB;

    public ParticleSystem rainParticle;
    private ParticleSystem.EmissionModule rainModule;

    public int rainRateOverTime;
    public int rainIncrement;

    public float rainIncrementDelay;




    private void Initialization()
    {
        slimeWayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rainModule = rainParticle.emission;
        rainRateOverTime = Random.Range(1000, 2000);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    public void OnOffRain(bool isRain)
    {
        StopCoroutine(nameof(RainManager));
        StopCoroutine(nameof(PostBManager));
        StartCoroutine("RainManager", isRain);
        StartCoroutine("PostBManager", isRain);
    }

    IEnumerator RainManager(bool isRain)
    {
        switch (isRain)
        {
            case true:

                for(float r = rainModule.rateOverTime.constant; r < rainRateOverTime; r += rainIncrement)
                {
                    rainModule.rateOverTime = r;
                    yield return new WaitForSeconds(rainIncrementDelay);
                }

                rainModule.rateOverTime = rainRateOverTime;

                break;

            case false:

                for (float r = rainModule.rateOverTime.constant; r > 0; r -= rainIncrement)
                {
                    rainModule.rateOverTime = r;
                    yield return new WaitForSeconds(rainIncrementDelay);
                }

                rainModule.rateOverTime = 0;

                break;
        }
    }

    IEnumerator PostBManager(bool isRain)
    {
        switch (isRain)
        {
            case true:

                for (float w = postB.weight; w < 1; w += 1 * Time.deltaTime)
                {
                    postB.weight = w;
                    yield return new WaitForEndOfFrame();
                }

                postB.weight = 1;

                break;

            case false:

                for (float w = postB.weight; w > 0; w -= 1 * Time.deltaTime)
                {
                    postB.weight = w;
                    yield return new WaitForEndOfFrame();
                }

                postB.weight = 0;

                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
