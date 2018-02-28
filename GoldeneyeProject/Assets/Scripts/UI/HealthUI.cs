using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    public CanvasGroup HPGroup;
    public GameObject HealthBar;
    public GameObject HealthBack;
    public GameObject ArmourBar;
    public GameObject ArmourBack;
    public GameObject Player;
    Image HealthImage;
    Image HealthBackImage;
    Image ArmourImage;
    Image ArmourBackImage;
    float timehit = 0.0f;
    int hitshowntime = 1;

    void Start()
    {
        HealthImage = HealthBar.GetComponent<Image>();
        HealthBackImage = HealthBack.GetComponent<Image>();
        ArmourImage = ArmourBar.GetComponent<Image>();
        ArmourBackImage = ArmourBack.GetComponent<Image>();
    }

    public void UpdateHealth(float HP)
    {
        timehit = Time.time;
        HealthImage.fillAmount = HP / 100;
        StartCoroutine(FadeBars(false));
    }

    public void UpdateArmour(float AP)
    {
        timehit = Time.time;
        ArmourImage.fillAmount = AP / 100;
        StartCoroutine(FadeBars(false));
    }

    void HideBars()
    {
        HPGroup.alpha = 0;
    }

    IEnumerator FadeBars(bool down)
    {
        if (down)
        {
            while (HPGroup.alpha > 0)
            {
                HPGroup.alpha -= Time.deltaTime * 1;
                yield return null;
            }
        }
        else
        {
            while (HPGroup.alpha < 1)
            {
                HPGroup.alpha += Time.deltaTime * 100;
                yield return null;
            }
        }

        yield return 0;
    }
	
	// Update is called once per frame
	void Update ()
    {    
        if (Time.time - timehit > hitshowntime)
        {
            StartCoroutine(FadeBars(true));
        }       
    }
}
