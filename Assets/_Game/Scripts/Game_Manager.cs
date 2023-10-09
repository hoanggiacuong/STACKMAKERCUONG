using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject pivotWall; // Đối tượng Prefab biểu thị mặt đất
    [SerializeField] private GameObject pivotBrick;
    [SerializeField] private GameObject pivot_Unbrik;
    [SerializeField] private GameObject winPos;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject player;
    [SerializeField]  public static Game_Manager Istance;
    public bool isPlayer=false;
 
    public List<GameObject> mapPrefabList;
    public  string mapFilePath1 = "E:/dev game/BTTH2/Assets/_Game/Text_Map/map1.txt";
    public  string mapFilePath2 = "E:/dev game/BTTH2/Assets/_Game/Text_Map/map2.txt";
    public string mapFilePath3 = "E:/dev game/BTTH2/Assets/_Game/Text_Map/map3.txt";
    public string mapFilePath4 = "E:/dev game/BTTH2/Assets/_Game/Text_Map/map4.txt";
    public string mapFilePath5 = "E:/dev game/BTTH2/Assets/_Game/Text_Map/map5.txt";// Đường dẫn đến tệp văn bản
    public  List<GameObject> map;

    public GameObject Map;
    public List<string> mapFilePath;

    public  string[] lines;
    public Vector3 initPos;

   

    private void Awake()
    {
        Istance = this;
        mapPrefabList.Add(pivotWall);
        mapPrefabList.Add(pivotBrick);
        mapPrefabList.Add(line);
        mapPrefabList.Add(winPos);
        mapPrefabList.Add(pivot_Unbrik);
        mapPrefabList.Add(brick);
        mapPrefabList.Add(player);

        mapFilePath.Add(mapFilePath1);
        mapFilePath.Add(mapFilePath2);
        mapFilePath.Add(mapFilePath3);
        mapFilePath.Add(mapFilePath4);
        mapFilePath.Add(mapFilePath5);



    }
    void Start()
    {
       
       // LoadMap(mapFilePath1, map[0]);
        //LoadMap(mapFilePath1,map[0]);
    }

    public void LoadMap(string path, GameObject mapName)
    {
        // mapPrefabEnum my_mapPrefabEnum=mapPrefabEnum.Brick;
      
         Map= Instantiate(mapName, new Vector3(0, 0, 0), Quaternion.identity);
        
       
        if (!File.Exists(path))
        {
            Debug.LogError("Không tìm thấy tệp văn bản.");
            return;
        }

         lines = File.ReadAllLines(path);


       // int width = lines[0].Length;
        int height = lines.Length;

        //mang 2 chieu luu map\
        Vector3 InitPos(int x,int y)
        {
           return  new Vector3(x, 0, -y);   
        }



        for (int y =0; y <height; y++)
        {
            for (int x =0; x <lines[y].Length ; x++)
            {
                int tileSymbol =Int32.Parse(lines[y][x].ToString());
                Vector3 position = InitPos(x, y); // Vị trí trong môi trường 3D

                if (tileSymbol ==0)
                {
                    Instantiate(mapPrefabList[0], position, Quaternion.identity,Map.transform);
                }
                else if (tileSymbol == 1)
                {
                    Instantiate(mapPrefabList[1], position, Quaternion.identity, Map.transform);
                }
                else if (tileSymbol == 2)
                {
                    Instantiate(mapPrefabList[2], new Vector3(position.x,position.y+2.5f,position.z), Quaternion.Euler(new Vector3(-90, 90, 0)), Map.transform);
                    //Instantiate(mapPrefabList[5], new Vector3(position.x, position.y + 2.5f, position.z), Quaternion.Euler(new Vector3(-90, 90, 0)));
                }
                else if (tileSymbol == 3)
                {
                    Instantiate(mapPrefabList[3], new Vector3(position.x-4f , 0, position.z), Quaternion.Euler(new Vector3(0, 90, 0)), Map.transform);
                   
                }
                else if (tileSymbol ==4)
                {
                    initPos = new Vector3(position.x, 3.5f, position.z);
                    Instantiate(mapPrefabList[4], position, Quaternion.Euler(new Vector3(90, 0, 0)), Map.transform);
                    Instantiate(mapPrefabList[6], initPos, Quaternion.identity, Map.transform);
                    isPlayer = true;


                    Debug.Log(initPos);
                }


            }
        }
     









}


    private enum mapPrefabEnum
    {
       pivotWal,
       pivotBrick,
       pivot_Unbrik,
       winPos,
       Line,
       Brick
    }
}




// This text is added only once to the file.
