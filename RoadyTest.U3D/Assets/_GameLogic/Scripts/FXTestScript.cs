using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXTestScript : MonoBehaviour
{
    public int damage = 0;
    private bool stopped = true;
    public ParticleSystem tireSmokeR;
    public ParticleSystem tireSmokeL;
    public ParticleSystem SmokeR;
    public ParticleSystem SmokeL;
    private bool Go = false;
    // Start is called before the first frame update
    void Start()
    {
        tireSmokeL.Stop();
        tireSmokeR.Stop();
        SmokeL.Stop();
        SmokeR.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        if (damage > 0)
        {
            StartSmoke();
        }
        if (damage < 1)
        {
            StopSmoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Go = true;
        }
        else
        {
            Go = false;
            stopped = true;
        }

        if ((stopped = true) && (Go = true)) 
        {
            
            StartCoroutine(StartTireSmoke());
            stopped = false;
        }
    }
    void StartSmoke()
    {
        SmokeR.Play();
        SmokeL.Play();
    }
    void StopSmoke()
    {
        SmokeR.Stop();
        SmokeL.Stop();
    }
    IEnumerator StartTireSmoke()
    {
        tireSmokeL.Play();
        tireSmokeR.Play();
        yield return new WaitForSeconds(3);
        tireSmokeL.Stop();
        tireSmokeR.Stop();
    }
}
