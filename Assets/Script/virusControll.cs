using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virusControll : MonoBehaviour
{
    // Start is called before the first frame update
    public float deltaTime;
    public float lastMut = 0;
    public float duration = 0;
    public bool[] mutate;
    public int mutIndex = -1;
    void Start()
    {
        mutate = new bool[25];
        for (int i = 0; i < 25; i++)
        {
            mutate[i] = false;
        }
        //randMut()vir
    }

    // Update is called once per frame
    void Update()
    {
        //update time
        deltaTime = deltaTime + Time.deltaTime;
        duration = deltaTime - lastMut;
        //if the duration is bigger than 1
        if (deltaTime - lastMut >= 1.3) {
            if (mutIndex != -1)
            {
                recMut();//recover last mutation
            }
        }
        if (deltaTime - lastMut >= 1.5) {


           
            randMut();//mutate one random virus
        }
    }
    public void randMut() {
        lastMut = deltaTime;//update last mutation time
        mutIndex = Random.Range(0, 24);//update mutation index
        mutate[mutIndex] = true;
        this.transform.GetChild(mutIndex).GetComponent<PerVirusControl>().isMut = true; //set the value per virus
        //this.transform.GetChild(mutIndex).GetChild(0).GetComponent<MeshRenderer>().material.SetColor(Color.yellow);
        (this.transform.GetChild(mutIndex).GetChild(2).GetComponent("Halo") as Behaviour).enabled = true;
    }
    public void recMut() {
        
        mutate[mutIndex] = false;//set the last mutation to false
        this.transform.GetChild(mutIndex).GetComponent<PerVirusControl>().isMut = false; //set the value per virus
        (this.transform.GetChild(mutIndex).GetChild(2).GetComponent("Halo") as Behaviour).enabled = false;
        mutIndex = -1;//set the mutation index back to null
    }
}
