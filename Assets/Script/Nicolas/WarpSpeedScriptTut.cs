using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WarpSpeedScriptTut : MonoBehaviour
{
    public VisualEffect warpSeedVFX;
    public float rate = 0.02f;
    private bool warpActive;

    void Start()
    {
        warpSeedVFX.Stop();
        warpSeedVFX.SetFloat("WarpAmount", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // a la fin du niveau
        {
            warpActive = true;
            StartCoroutine(ActivateParticules());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            warpActive = false;
            StartCoroutine(ActivateParticules());   
        }
    }

    private IEnumerator ActivateParticules()
    {
        if (warpActive)
        {
            warpSeedVFX.Play();

            float amount = warpSeedVFX.GetFloat("WarpAmount");
            while (amount < 1 && warpActive)
            {
                amount += rate;
                warpSeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            float amount = warpSeedVFX.GetFloat("WarpAmount");
            while (amount > 0 && !warpActive)
            {
                amount -= rate;
                warpSeedVFX.SetFloat("WarpAmount", amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= 0 +rate)
                {
                    amount = 0;
                    warpSeedVFX.SetFloat("WarpAmount", amount);
                    warpSeedVFX.Stop();
                }
            }
        }
    }
}
