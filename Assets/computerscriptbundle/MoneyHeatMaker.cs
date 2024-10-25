using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoneyHeatMaker : MonoBehaviour
{
    public float money = 0;
    public float temp = 0;
    public float moneyinterval=15f;
    public float tempinterval=15f;
    bool on;
    // Start is called before the first frame update
    public void SetUpMoneyHeatMaker(Vector2 h, Vector2 m){
        money = m.x;
        moneyinterval = m.y;
        temp = h.x;
        tempinterval = h.y;
        //print(ProgressManager.prmInstance);
        ProgressManager.prmInstance.contributors.Add(this);
        
    }
    public void TurnOn()
    {
        if (!on){
            on = true;
            StartCoroutine(PassTimeMoney());
            StartCoroutine(PassTimeTemp());
        }
    }

    public void TurnOff(){
        if(on){
            StopAllCoroutines();
            on=false;
        }
    }

    public void PassTimeInstantly(float t){
        if (on){
            ProgressManager.prmInstance.wallet += math.floor(t/moneyinterval)*money;
            ProgressManager.prmInstance.ChangeTemperature(math.floor(t/tempinterval)*temp);
        }
    }

    IEnumerator PassTimeMoney(){
        while(true){
            yield return new WaitForSeconds(moneyinterval);
            ProgressManager.prmInstance.wallet += money;
        }
    }

    IEnumerator PassTimeTemp(){
        while (true){
            ProgressManager.prmInstance.ChangeTemperature(temp);
            yield return new WaitForSeconds(tempinterval);
        }
    }
}
