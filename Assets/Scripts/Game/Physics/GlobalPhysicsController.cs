using System.Collections;
using UnityEngine;

public class GlobalPhysicsController : MonoBehaviour
{
    private bool globalSlowDownEffectActive = false;
    
    private void Awake()
    {
        GameplayEvents.FreezePotion.AddListener(SlowDownAllFlyingUnits);
        GameplayEvents.Restart.AddListener(GameplayEvents.SendStopMagneticEffect);
    }

    private void SlowDownAllFlyingUnits(float slowMultiplier, float time)
    {
        if (globalSlowDownEffectActive) return;
        
        GameplayEvents.SendSlowDownAllUnitsEvent(slowMultiplier);
        StartCoroutine(TemporaryGlobalSlowDownEffect(time));
    }

    private IEnumerator TemporaryGlobalSlowDownEffect(float time)
    {
        globalSlowDownEffectActive = true;
        yield return new WaitForSeconds(time);
        GameplayEvents.SendStopGlobalSlowDownEffect();
        globalSlowDownEffectActive = false;

    }
}
