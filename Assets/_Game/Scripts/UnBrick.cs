using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    private bool isCollider = true;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    [SerializeField] GameObject brick;
    private void OnTriggerEnter(Collider other)
    {
        mesh = this.GetComponent<MeshRenderer>();

        if (other.CompareTag("winPos"))
        {
            mesh.enabled = false;
            Destroy(brick);

        }
        if (isCollider)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().RemoveStack();
               
                if (brick !=null)
                {
                    brick.SetActive(true);
                }
                isCollider = false;

            }

        }


        // Update is called once per frame


    }
}

