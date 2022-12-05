using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HyperSpeedEffect : MonoBehaviour
{
    public VisualEffect warpSeedVFX;
    public MeshRenderer cylinder;
    public float rate = 0.02f;
    public float delay = 2.5f;
    private bool warpActive;

    void Start()
    {
        warpSeedVFX.Stop();
        warpSeedVFX.SetFloat("WarpAmount", 0);

        cylinder.material.SetFloat("Active_",0);
    }

    void Update()
    {
        if (GameManager.instance.ActualGameState == GameState.ENDROUND) // a la fin du niveau
        {
            warpActive = true;
            StartCoroutine(ActivateParticules());
            StartCoroutine(ActivateShader());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            warpActive = false;
            StartCoroutine(ActivateParticules());
            StartCoroutine(ActivateShader());
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
    private IEnumerator ActivateShader()
    {
        if (warpActive)
        {
            yield return new WaitForSeconds(delay);
            float amount = cylinder.material.GetFloat("Active_");
            while (amount < 1 && warpActive)
            {
                amount += rate;
                cylinder.material.SetFloat("Active_", amount);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            float amount = cylinder.material.GetFloat("Active_");
            while (amount > 0 && !warpActive)
            {
                amount -= rate;
                cylinder.material.SetFloat("Active_", amount);
                yield return new WaitForSeconds(0.1f);

                if (amount <= 0 + rate)
                {
                    amount = 0;
                    cylinder.material.SetFloat("Active_", amount);
                }
            }
        }
    }
}
