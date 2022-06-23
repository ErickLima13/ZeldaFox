using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    private GameManager _gameManager;

    public bool isRain;

    private void Initialization()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
     
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.OnOffRain(isRain);
        }
    }
}
