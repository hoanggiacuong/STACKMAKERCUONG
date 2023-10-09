using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ClearStack();
            Debug.Log("endbox");
            UI_Manager.UI_Istance.ShowbtnWin();
            return;
           
        }
    }
    // Start is called before the first frame update
}
