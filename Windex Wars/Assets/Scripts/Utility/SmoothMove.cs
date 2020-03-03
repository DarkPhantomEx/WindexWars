using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    // Inspector Fields
    public List<Transform> Transforms; // The transforms to move to
    public List<float> Delays; // The delay between each movement
    public List<float> Durations; // The time it takes to move from one transform to the other

    private int index;
    private float currentTime;
    private float delayTime;
    private Vector3 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial starting position
        index = 0;
        currentTime = 0;
        delayTime = 0;

        StartPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (index < Transforms.Count)
        {
            // Delay between movement
            if (delayTime < Delays[index])
            {
                delayTime += Time.deltaTime;
            }
            // Slerp towards the next transform
            else if (transform.position != Transforms[index].position && transform.rotation != Transforms[index].rotation)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Durations[index];
                transform.position = Vector3.Slerp(startPos, Transforms[index].position, value);
                transform.rotation = Quaternion.Slerp(startRot, Transforms[index].rotation, value);
            }
            // Start delay for next transform
            else
            {
                index++;
                delayTime = 0;
            }
        }
    }

    void StartPath()
    {
        currentTime = 0;
        startPos = transform.position;
        startRot = transform.rotation;
    }
}
