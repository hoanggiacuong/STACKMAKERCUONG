using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private bool isCollider = true;
    // Start is called before the first frame update
    [SerializeField] GameObject unBrick;
    private void OnTriggerEnter(Collider other)
    {

        if (isCollider)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().AddStack();
                this.unBrick.SetActive(false);
                isCollider = false;

            }
          
        }

        // Update is called once per frame


    }
}

