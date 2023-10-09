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
    private  string mapFilePath1 =  "map1";
    private string mapFilePath2 =  "map2";
    private string mapFilePath3 = "map3";
    private string mapFilePath4 =  "map4";
    private string mapFilePath5 =  "map5";// Đường dẫn đến tệp văn bản
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

    public void LoadMap(string pathName, GameObject mapName)
    {
        // mapPrefabEnum my_mapPrefabEnum=mapPrefabEnum.Brick;
        Map = Instantiate(mapName, new Vector3(0, 0, 0), Quaternion.identity);

        TextAsset txtAsset = Resources.Load<TextAsset>(pathName);
        lines = txtAsset.text.Split('\n');





        // // doc text tư pathfile
        // string filePath = Path.Combine( Application.streamingAssetsPath ,pathName);
        // //string filePath1 = Application.persistentDataPath + pathName;
        // UI_Manager.UI_Istance.satus_text.text = filePath;
        //Debug.Log(filePath);
        // if (!File.Exists(filePath))
        // {
        //     Debug.LogError("Không tìm thấy tệp văn bản.");
        //     UI_Manager.UI_Istance.satus_text.text = filePath+"khong co file";
        //     return;
        // }
        // //  if (!Directory.Exists(filePath)) { Directory.CreateDirectory(filePath); }
        // lines = File.ReadAllLines(filePath);


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
                int tileSymbol;
               Int32.TryParse(lines[y][x].ToString(),out tileSymbol);
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
