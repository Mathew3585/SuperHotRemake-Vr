using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMan : MonoBehaviour
{
    public VelocityEstimator Head;
    public VelocityEstimator LeftHand;
    public VelocityEstimator RightHand;

    public float sensitivity = 0.8f;
    public float mintimescale = 0.05f;

    private float initialFixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        initialFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        float VelocityMananger = Head.GetVelocityEstimate().magnitude + LeftHand.GetVelocityEstimate().magnitude + RightHand.GetVelocityEstimate().magnitude;

        Time.timeScale = Mathf.Clamp01(mintimescale + VelocityMananger * sensitivity);

        Time.fixedDeltaTime= initialFixedDeltaTime * Time.timeScale;
    }
}
