using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSense = 100f;
    public float mouseX;
    public float mouseY;
    public GameObject VirusArray;
    public bool[] mutate;
    public bool hold = false;
    public int killNum = 0;
    public Text killText;
    public AudioClip hitSound;
    public AudioClip missSound;
    public AudioSource audPlay;
    public bool miss;
    public bool start;
    public bool played;
    public bool triggered;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0.0f;
        audPlay = GetComponent<AudioSource>();
        start = false;
        miss = true;
    }

    // Update is called once per frame
    void Update()
    {

        
        mutate = VirusArray.GetComponent<virusControll>().mutate;
        mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;
        this.transform.position = new Vector3(this.transform.position.x + mouseX, this.transform.position.y+ mouseY, this.transform.position.z);
        //Debug.Log("Ha");
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("HOLD");
            hold = true;
            if (triggered == false && played == false && start == true) {
                audPlay.PlayOneShot(missSound);
                Debug.Log("Play miss in update");
                played = true;
            }
        }
        else {
            hold = false;
            miss = false;
            played = false;
        }
        killText.text = killNum.ToString("000");
        if (start == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1.0f;
                start = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        triggered = true;
        //Debug.Log(other.gameObject.transform.parent.tag.ToString());
        if (other.gameObject.tag.ToString() != "wall") {
            if (other.gameObject.transform.parent.tag.ToString() == "virus")
            {
                //Debug.Log(other.gameObject.transform.parent.tag.ToString());
                if (other.gameObject.transform.parent.GetComponent<PerVirusControl>().isMut == true)
                {
                    //Debug.Log("CAUGGGGGGGGGGGGHHHHHHHHHHHHHHHHHHHHHHT");

                    if (hold == true)
                    {
                        miss = false;
                        //Debug.Log("HIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIT");
                        
                        audPlay.PlayOneShot(hitSound);
                        Debug.Log("Play hit in trigger");
                        played = true;
                        killNum++;
                        VirusArray.GetComponent<virusControll>().recMut();//recover the virus
                        VirusArray.GetComponent<virusControll>().randMut();//recover the virus
                    }
                }
                else {
                    if (hold == true) {
                        miss = true;
                        Debug.Log("Hit" + other.gameObject.transform.parent.name.ToString());
                    }
                }
            }
            else
            {
                if (hold == true)
                {
                    miss = true;
                    Debug.Log("Hit noneVirus");
                }
            }
        }
        else
        {
            if (hold == true)
            {
                miss = true;
                Debug.Log("Hit wall");
            }
        }
        if (other.gameObject == null) {
            if (hold == true)
            {
                miss = true;
                Debug.Log("Hit Nothing");
            }
        }
        if (miss == true && played == false)
        {
            audPlay.PlayOneShot(missSound);
            Debug.Log("Play miss in trigger");
            played = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}
