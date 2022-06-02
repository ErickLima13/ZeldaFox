using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCam : MonoBehaviour
{
    [SerializeField] private GameObject secondCam;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                secondCam.SetActive(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "CamTrigger":
                secondCam.SetActive(false);
                break;
        }
    }
}
