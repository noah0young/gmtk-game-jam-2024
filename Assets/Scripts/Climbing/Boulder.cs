using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : BreakableObj
{
    [SerializeField] private GameObject destroyEffectPrefab;


    protected override void Break()
    {
        AudioManager.PlaySFX("RockBreak");
        if (destroyEffectPrefab != null)
        {
            GameObject destroyEffect = Instantiate(destroyEffectPrefab);
            destroyEffect.transform.position = transform.position;
        }
        Destroy(gameObject);
    }

}
