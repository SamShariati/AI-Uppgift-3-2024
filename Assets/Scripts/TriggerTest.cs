using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Detected");
        if (other != null & other.gameObject.CompareTag("Food"))
        {
            Debug.Log("Det Funkar");
        }
    }
}
