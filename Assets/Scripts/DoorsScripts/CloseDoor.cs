using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public float targetY = 1.6f;
    static float t = 0;
    bool done = false;

    void Update()
    {
        if(transform.position.y + targetY * 0.05 <= targetY && !done)
        {
            float newPositionY = Mathf.Lerp(transform.position.y, targetY, t);
            transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);
            t += 0.002f * Time.deltaTime;
        }
        else
        {
            done = true;
        }
    }
    
}
