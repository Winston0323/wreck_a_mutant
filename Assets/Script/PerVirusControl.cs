using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerVirusControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isMut;
    public float duration;
    public float lastMove;
    public Vector3 center;
    void Start()
    {
        isMut = false;
        center = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        duration = duration + Time.deltaTime;
        if (duration - lastMove > 0.1) {
            shake();
        }
    }
    private void shake() {
        float randX = Random.Range(-0.05f, 0.05f);
        float randY = Random.Range(-0.05f, 0.05f);
        this.transform.position = new Vector3(center.x + randX, center.y+randY, center.z);
    }
}
