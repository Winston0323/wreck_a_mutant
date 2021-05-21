using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    // Start is called before the first frame update
    public Text counterText;
    public Text finishText;
    public float second;
    public float startTime = 10;
    public float currTime;
    public GameObject virusArray;
    public AudioSource audPlay;
    public AudioClip finish;
    
    void Start()
    {

        currTime = startTime;
        counterText.text = "000";
        audPlay = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        second += Time.deltaTime;
        currTime = startTime - second;
        if (currTime <= 0f) {
            audPlay.PlayOneShot(finish);
            Time.timeScale = 0;
            finishText.gameObject.SetActive(true);
        }
        if (counterText != null)
        {
            counterText.text = currTime.ToString("00");
        }
        else
        {
            Debug.Log("couterText is null");
        }
    }
    public void end()
    {
        counterText.text = "00";
    }
    public void Reset()
    {
        counterText.text = "30";
        //minute = 0.0f;
        second = 0.0f;
    }
}
