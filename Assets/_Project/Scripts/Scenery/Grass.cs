using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private ParticleSystem fxHit;

    private bool isCut;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetHit(int amount)
    {
        if (!isCut)
        {
            transform.localScale = Vector3.one;
            fxHit.Emit(Random.Range(10, 50));
            isCut = true;
        }
        
    }
}
