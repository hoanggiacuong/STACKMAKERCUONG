using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Player : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isMoving = false;
    private Vector3 targetPos_3D;
    private Vector3 curPos_3D;
    private Vector3 rayPos;
    private Vector3 curStep3D;
    Vector2 swipeDirection;

    [SerializeField] private bool isRayCast=true;
    [SerializeField] private GameObject Brick;
    [SerializeField] private GameObject unBrick;
    [SerializeField] private Transform avatar;


    private Stack<GameObject> BrickStack = new Stack<GameObject>();
   


    public bool detectSwipeOnlyAfterRelease = false;
    public float minDistanceForSwipe = 20f;
    Direct direct = Direct.None;

    // Start is called before the first frame update

    void Start()
    {
       Invoke(nameof(InitPos), 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("getbuttonDown");
            fingerDownPosition = Input.mousePosition;
            fingerUpPosition = Input.mousePosition;
        }

        //if (!detectSwipeOnlyAfterRelease && Input.GetMouseButton(0))
        //{
        //    fingerUpPosition = Input.mousePosition;
        //   direct= DetectSwipe();
        //    Debug.Log(direct);
        //    Move();
        //}

        if (Input.GetMouseButtonUp(0))
        {

            fingerUpPosition = Input.mousePosition;
            direct = DetectSwipe();
            Debug.Log(direct);
            isMoving = true;

        }
        StepRaycast();
        if (isMoving)
        {
            Move();
        }


        if (isRayCast)
        {
            BrickProcess();
        }
        
        

    }

    private Direct DetectSwipe()
    {
        
        if (Vector2.Distance(fingerDownPosition, fingerUpPosition) >= minDistanceForSwipe)
        {
           
            
        }
        swipeDirection = fingerUpPosition - fingerDownPosition;
        swipeDirection.Normalize();

        // Xác định hướng vuốt dựa trên swipeDirection
        if (swipeDirection.y > 0 && swipeDirection.x > -0.5f && swipeDirection.x < 0.5f)
        {
            Debug.Log("Len");
            return Direct.Forward;


        }
        else if (swipeDirection.y < 0 && swipeDirection.x > -0.5f && swipeDirection.x < 0.5f)
        {
            Debug.Log("Vuốt xuống");

            return Direct.Back;


        }
        else if (swipeDirection.x < 0 && swipeDirection.y > -0.5f && swipeDirection.y < 0.5f)
        {
            Debug.Log("Vuốt sang trái");
            return Direct.Left;



        }
        else if (swipeDirection.x > 0 && swipeDirection.y > -0.5f && swipeDirection.y < 0.5f)
        {
            Debug.Log("Vuốt sang phải");
            return Direct.Right;


        }
        // }
        return Direct.None;
    }

    private void Move()
    {
        switch (direct)
        {
            case Direct.Forward:

                targetPos_3D = TargetPosMoveForward();
                break;

            case Direct.Back:
                targetPos_3D = TargetPosBack();
                break;

            case Direct.Right:
                targetPos_3D = TargetPosRight();
                break;

            case Direct.Left:
                targetPos_3D = TargetPosLeft();
                break;

            case Direct.None:
                break;
            default:
                break;
        }
        // Debug.Log(transform.position);
        // Debug.Log(targetPos_3D);

        
        transform.position = Vector3.MoveTowards(transform.position, targetPos_3D, Time.deltaTime * 10f);

        curPos_3D = targetPos_3D;
        
        if (Vector3.Distance(transform.position, targetPos_3D) < 0.001f)
        {
            isMoving = false;
            curStep3D = curPos_3D;
        }
    }

    public enum Direct
    {
        Forward = 0,
        Back = 1,
        Right = 2,
        Left = 3,
        None = -1

    }

   

    public void InitPos()
    {

        //for (int y = 0; y < Game_Manager.Istance.lines.Length; y++)
        //{
        //    for (int x = 0; x < Game_Manager.Istance.lines[y].Length; x++)
        //    {
        //        int pos = Int32.Parse(Game_Manager.Istance.lines[y][x].ToString());
        //        if (pos == 5)
        //        {

        //            initPos = new Vector2Int(y,x);
        //            curPos_3D = new Vector3(initPos.x, 0,-initPos.y);
        //            transform.position = curPos_3D;

        //        }

        //    }

        //}

        //duyệt khi vuốt lên
        curPos_3D = Game_Manager.Istance.initPos;
        transform.position = curPos_3D;
        targetPos_3D = curPos_3D;
        curStep3D = curPos_3D;
       // Debug.Log(curPos_3D);



    }

    //vị trí đi đén
    private Vector3 TargetPosMoveForward()
    {
        int row = -Mathf.RoundToInt(curPos_3D.z) - 1;
        int col = Mathf.RoundToInt(curPos_3D.x);

        while (row >= 0 && Game_Manager.Istance.lines[row][col].ToString() == "1" || Game_Manager.Istance.lines[row][col].ToString() == "4" || Game_Manager.Istance.lines[row][col].ToString() == "2")
        {

            row--;
        }
        return new Vector3(col, 3.5f, -row - 1f);

    }

    private Vector3 TargetPosBack()
    {
        int row = -Mathf.RoundToInt(curPos_3D.z) + 1;
        int col = Mathf.RoundToInt(curPos_3D.x);

        while (Game_Manager.Istance.lines[row][col].ToString() == "1" || Game_Manager.Istance.lines[row][col].ToString() == "4" || Game_Manager.Istance.lines[row][col].ToString() == "2")
        {

            row++;
        }
        return new Vector3(col, 3.5f, -row + 1);

    }
    private Vector3 TargetPosRight()
    {
        int row = -Mathf.RoundToInt(curPos_3D.z);
        int col = Mathf.RoundToInt(curPos_3D.x) + 1;

        while (Game_Manager.Istance.lines[row][col].ToString() == "1" || Game_Manager.Istance.lines[row][col].ToString() == "4" || Game_Manager.Istance.lines[row][col].ToString() == "2")  
        {

            col++;
        }
        return new Vector3(col - 1, 3.5f, -row);

    }
    private Vector3 TargetPosLeft()
    {
        int row = -Mathf.RoundToInt(curPos_3D.z);
        int col = Mathf.RoundToInt(curPos_3D.x) - 1;

        //Debug.LogError($"{row} - {col}");
        while (Game_Manager.Istance.lines[row][col].ToString() == "1" || Game_Manager.Istance.lines[row][col].ToString() == "4" || Game_Manager.Istance.lines[row][col].ToString() == "2")
        {
            //Debug.Log($"{row} - {col}");
            if (col == 0)
            {
                return new Vector3(col, 3.5f, -row);
            }
            else
            {
                col--;
            }

            // col--;


        }

        return new Vector3(col + 1, 3.5f, -row);

    }

    public void AddStack()
    {
        
       // Transform peopleTf = transform.GetChild(0);
        Vector3 newPosition = avatar.position;
        newPosition.y += 0.2f; // Đặt giá trị Y mới ở đây
        avatar.position = newPosition;
        GameObject addBrick = Instantiate(Brick, avatar.position, Quaternion.Euler(new Vector3(90, 0, 0)));
        BrickStack.Push(addBrick);

        addBrick.transform.SetParent(transform);

    }

    public void RemoveStack()
    {
        //if//
        if (BrickStack.Count > 0)
        {


            GameObject BrickPop = BrickStack.Pop();
            Destroy(BrickPop);
            // Transform peopleTf = transform.GetChild(0);//
            Vector3 newPosition = avatar.position;
            newPosition.y -= 0.2f; // Đặt giá trị Y mới ở đây
            avatar.position = newPosition;
          
           // Vector3 posUnBrick = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            //GameObject addBrick = Instantiate(unBrick,posUnBrick, Quaternion.Euler(new Vector3(-90, 0, 0)));
            //addBrick.tag = "Brick";
            //Invoke(nameof(Addphysic), 0.1f);
            //void Addphysic()
            //{
            //    addBrick.AddComponent<BoxCollider>().isTrigger = true;
            //}

        }
    }
    public void ClearStack()
    {
        while (BrickStack.Count != 0)
        {
            GameObject BrickPop = BrickStack.Pop();
            Destroy(BrickPop);
           
        }
        transform.position += new Vector3(1f, -1f, 0);
        avatar.position = transform.position;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            //Destroy(other.gameObject);
           // Debug.Log("da va cham gach");
        }

       

    }
   
    private int DetectBrick()
    {
        
        switch (direct)
        {
            case Direct.Forward:
                rayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z+1f);
           
                break;

            case Direct.Back:
                rayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
                break;

            case Direct.Right:
                rayPos = new Vector3(transform.position.x+1f, transform.position.y, transform.position.z );
                break;

            case Direct.Left:
                rayPos = new Vector3(transform.position.x-1f, transform.position.y, transform.position.z );
                break;

            case Direct.None:
                break;
            default:
                break;
        }

        
            Ray ray = new Ray(rayPos, Vector3.down*2);
            //Debug.Log("dang ban rayas");
            RaycastHit hitInfo;

        //Debug.DrawLine(rayPos,  hitInfo.point, Color.red);
        if (Physics.Raycast(ray, out hitInfo))
            {
                // Xử lý khi va chạm xảy ra
                if (hitInfo.collider.CompareTag("unBrick"))
                {
                

                return 0;
                    // Debug.Log("Va chạm với đối tượng có tag ");

                }
                else if (hitInfo.collider.CompareTag("Brick"))
                {
                    return 1;
                }

            }
           
        
       

        return 2;
    }
    private void BrickProcess()
    {
        int stateBrick=DetectBrick();
        switch (stateBrick)
        {
            case 0:
                RemoveStack();
                break;
            case 1:
                AddStack();
                break;

        }
            
    }

    private void StepRaycast()
    {
        //int curStep=Mathf.RoundToInt(curPos_3D.z);
        //while (-transform.position.z + targetPos_3D.z<0.001f)
        //{
        //    if (transform.position.z == curStep)
        //    {
        //        isRayCast = true;
        //        curStep += 1;   
        //    }
        //    else
        //    {
        //        isRayCast = false;
        //    }
        //}

       
        
       if (isMoving)
       {
           // Debug.Log("pos" + transform.position+"curstp= "+curStep3D+"diẻct="+direct);
            

            if (Vector3.Distance(transform.position, curStep3D) < 0.1f)
                //Vector3.Distance(transform.position, curStep3D) < 00.1
            {
               // Debug.Log("raycsst true");
               // Debug.Log("trươc"+curStep3D);
                isRayCast = true;
                Debug.Log(direct);
                switch (direct)
                {
                    case Direct.Forward:

                        curStep3D += new Vector3(0, 0, 1f);
                       // Debug.Log("tang1"+curStep3D);
                        //Debug.Log("pos"+transform.position);
                        break;

                    case Direct.Back:
                        curStep3D += new Vector3(0, 0, -1f);
                        Debug.Log(curStep3D);
                        break;

                    case Direct.Right:
                        curStep3D += new Vector3(1f, 0, 0);
                        break;

                    case Direct.Left:
                        curStep3D += new Vector3(-1f, 0, 0);
                        break;
                }
              


            }
            else
            {
                isRayCast = false;
            }

        }
        else
        {
            isRayCast = false;
        }
    }

   

     // Kiểm tra va chạm với đoạn tia
  
}    






