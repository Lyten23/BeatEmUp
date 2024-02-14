using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXAuraController : MonoBehaviour
{
    public GameObject particleSystem;
    public void ActivedAura() => StartCoroutine(VFXAuraTime());

    IEnumerator VFXAuraTime()
    {
        particleSystem.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        particleSystem.SetActive(false);
    }
}
