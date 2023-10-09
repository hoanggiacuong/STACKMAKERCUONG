using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvManager : MonoBehaviour
{
    public  GameObject maplv;
    public string mapFilePath;
    private int curLv;
    
    // Start is called before the first frame update
    public  void StartPlay()
    {
        curLv = 0;
        maplv = Game_Manager.Istance.map[curLv];
        mapFilePath = Game_Manager.Istance.mapFilePath[curLv];
        Game_Manager.Istance.LoadMap(mapFilePath, maplv);

       UI_Manager.UI_Istance.HidebtnPlay();


    }
    public void StartNextLv()
    {

        // Game_Manager.Istance.map[curLv].gameObject.SetActive(false);
        Destroy(Game_Manager.Istance.Map);
        curLv += 1;
        maplv = Game_Manager.Istance.map[curLv];
        mapFilePath = Game_Manager.Istance.mapFilePath[curLv];
       Game_Manager.Istance.LoadMap(mapFilePath, maplv);

        UI_Manager.UI_Istance.HidebtnWin();
        
    }
    public void RetryLv()
    {
        Destroy(Game_Manager.Istance.Map);
        // Game_Manager.Istance.map[curLv].gameObject.SetActive(false);
        maplv = Game_Manager.Istance.map[curLv];
        mapFilePath = Game_Manager.Istance.mapFilePath[curLv];
        Game_Manager.Istance.LoadMap(mapFilePath, maplv);
        UI_Manager.UI_Istance.HidebtnWin();
    }

}
