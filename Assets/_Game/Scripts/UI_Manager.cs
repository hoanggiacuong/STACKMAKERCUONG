using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager UI_Istance;
   [SerializeField] private Button BtnPlay;
    [SerializeField] private Button BtnNextLv;
    [SerializeField] private Button BtnRetry;
    // Start is called before the first frame update
    void Start()
    {
        UI_Istance = this;
        
    }

    public void HidebtnPlay()
    {
        BtnPlay.gameObject.SetActive(false);
    }
    public void ShowbtnWin()
    {
        BtnNextLv.gameObject.SetActive(true);
        BtnRetry.gameObject.SetActive(true);
    }

    public void HidebtnWin()
    {
        BtnNextLv.gameObject.SetActive(false);
        BtnRetry.gameObject.SetActive(false);
    }

}
