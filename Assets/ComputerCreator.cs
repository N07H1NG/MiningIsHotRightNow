using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
public class ComputerCreator : MonoBehaviour
{
    
    public static ComputerCreator crtInst;
    [SerializeField] GameObject door;
    [SerializeField] Camera c;
    public  GameObject[] types;
    public GameObject[] boxTypes;
    public  Vector2[] heatvalues;
    public  Vector2[] moneyvalues;

    public bool boxPickedUp = false;
    public bool doorOpened = false;
    public AudioClip close;
    bool canDeliver = true;
    List<int> orderStack = new List<int>();
    void Awake(){
        crtInst = this;
    }
    
    void Start(){
        StartCoroutine(ManageOrders());
    }
    // Start is called before the first frame update
    public void CreateComputer(int index,Vector3 pos,Quaternion rot){
        GameObject cmptInst = Instantiate(types[index]);
        cmptInst.transform.position = pos;
        cmptInst.transform.rotation = rot * cmptInst.transform.rotation;
        //cmptInst.transform.rotation =  rot*transform.rotation;
        cmptInst.GetComponent<MoneyHeatMaker>().SetUpMoneyHeatMaker(heatvalues[index],moneyvalues[index]);
    }

    public void OrderBox(int index){
        orderStack.Add(index);
    }

    IEnumerator ManageOrders(){
        while(true){
            yield return new WaitForSeconds(Random.Range(12f,35f));
            if (canDeliver && orderStack.Count!=0){
                StartCoroutine(DeliverOrder(orderStack[0]));
                orderStack.RemoveAt(0);
                canDeliver = false;
            }
        }
    }

    void CreateBox(int index){
        GameObject b = Instantiate(boxTypes[index]);
        b.transform.position = transform.position;
        b.GetComponent<BoxedPC>().cam = c;
    }

    IEnumerator DeliverOrder(int index){
        doorOpened = false;
        door.GetComponent<Door>().delivery = true;
        door.GetComponent<AudioSource>().Play();
        while (!doorOpened){yield return null;}
        boxPickedUp = false;
        door.GetComponent<Animator>().SetBool("open",true);
        //yield return new WaitForSeconds(0.2f);
        CreateBox(index);
        while (!boxPickedUp){yield return null;}
        yield return new WaitForSeconds(0.3f);
        door.GetComponent<Animator>().SetBool("open",false);
        door.GetComponent<AudioSource>().PlayOneShot(close);
        door.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(2f);
        canDeliver = true;
        
    }
}
