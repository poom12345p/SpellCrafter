using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProcessBar : MonoBehaviour {
    Quaternion oringinRotation;
    [SerializeField] Image delayGaugeImage;
    [SerializeField] Image gaugeImage;
    [Range(0f,1f)]
    public float maxGaugue=1;
    [Range(0f, 1f)]
    public float changeSpeed;
    float currentGaugue;
    float realGaugue;
    // Use this for initialization
    void Start () {
        oringinRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (delayGaugeImage != null)
        {
            if (delayGaugeImage.fillAmount < realGaugue)
            {
                delayGaugeImage.fillAmount += changeSpeed * Time.fixedDeltaTime;
            }
            else if (delayGaugeImage.fillAmount > realGaugue)
            {
                delayGaugeImage.fillAmount -= changeSpeed * Time.fixedDeltaTime;
            }
            if (delayGaugeImage.fillAmount < realGaugue + changeSpeed * Time.fixedDeltaTime && delayGaugeImage.fillAmount > realGaugue - changeSpeed * Time.fixedDeltaTime)
            {
                delayGaugeImage.fillAmount = realGaugue;
            }
        }

        if (gaugeImage.fillAmount < realGaugue + changeSpeed * Time.fixedDeltaTime )
        {
            gaugeImage.fillAmount += changeSpeed * Time.fixedDeltaTime;
            //gaugeImage.fillAmount = Mathf.Clamp(gaugeImage.fillAmount,0, realGaugue);
        }
    }
    private void LateUpdate()
    {
        transform.rotation = oringinRotation;
    }

    public void updateGauge(float max,float cur)
    {
        updateRealGauge(max, cur);
        if (cur > 0 && cur <= max)
        {
            realGaugue= ((float)cur / (float)max) * maxGaugue;
            // gaugeImage.fillAmount = ((float)cur/ (float)max ) * maxhpGaugue;
        }
        else
        {
            realGaugue = 0;
        }
        
    }

    public void updateGaugeImediate(float max, float cur)
    {
        if (cur > 0)
        {
            realGaugue = ((float)cur / (float)max) * maxGaugue;
            //if (gaugeImage.fillAmount > realGaugue)
            //{
                gaugeImage.fillAmount = realGaugue;
           // }
        }
        else
        {
            realGaugue = 0;
            gaugeImage.fillAmount = realGaugue;
        }
    }
    public void updateRealGauge(float max, float cur)
    {
        if (cur > 0)
        {
            realGaugue = ((float)cur / (float)max) * maxGaugue;
            if (gaugeImage.fillAmount > realGaugue)
            {
            gaugeImage.fillAmount = realGaugue;
            }
        }
        else
        {
            realGaugue = 0;
            gaugeImage.fillAmount = realGaugue;
        }
    }
}
