using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AudioSource backgrndMusic;
    public float shakeDistance = 0.1f;
    public float shakespeed = 1;

    Vector3 initialPosition;
    Vector3 shakeOffset;
    bool isShaking = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        //backgrndMusic = GetComponent<AudioSource>();
        backgrndMusic.playOnAwake = true;
        backgrndMusic.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Shake camera when ground object falls
        if (isShaking)
        {
            Vector3 pos = transform.position;
            Vector3 offsetPos = pos + shakeOffset;
            float currentDistance = offsetPos.y - initialPosition.y;
            if(shakespeed >= 0)
            {
                if(currentDistance > shakeDistance)
                {
                    shakespeed *= -1;
                }
            }
            else
            {
                if (currentDistance < -shakeDistance)
                {
                    shakespeed *= -1;
                }
            }
            shakeOffset.y += shakespeed * Time.fixedDeltaTime;
            if(shakeOffset.y > shakeDistance)
            {
                shakeOffset.y = shakeDistance;
            }
            if(shakeOffset.y < -shakeDistance)
            {
                shakeOffset.y = -shakeDistance;
            }
            transform.position = initialPosition + shakeOffset;
        }
    }

    public void startShaking()
    {
        isShaking = true;
    }
    public void stopShaking()
    {
        isShaking = false;
        transform.position = initialPosition;
    }
}
