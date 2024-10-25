using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    TextMeshProUGUI tmp;
    ProgressManager prm;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        prm = ProgressManager.prmInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //print(tmp.text);
        tmp.text = "Your current wallet balance is " + prm.wallet + " HotCoins!\nThe current price of HotCoin is " + prm.price + "$ for HotCoin.\nYour dollar account meanwhile has " + prm.dollars+"$!"; 
    }
}
