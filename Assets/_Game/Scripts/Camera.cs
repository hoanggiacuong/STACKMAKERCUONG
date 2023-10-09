using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private float speed = 20f;
   
    // Start is called before the first frame update
    void Start()
    {

       

    }

    // Update is called once per frame
    void FixedUpdate()

    {
        if (Game_Manager.Istance.isPlayer == true)
        {
            target = FindObjectOfType<Player>().transform;
            Game_Manager.Istance.isPlayer = false;
        }
       
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);

    }
}
