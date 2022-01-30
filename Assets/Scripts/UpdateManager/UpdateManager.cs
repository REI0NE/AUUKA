using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    Singleton _singleton = Singleton.GetInstance();

    private void LateUpdate()
    {
        for (int i = 0; i < _singleton.Data.MonoCacheList.GetLateTicks.Count; i++)
            _singleton.Data.MonoCacheList.GetLateTicks[i].LateTick();
    }

    private void Update()
    {
        for (int i = 0; i < _singleton.Data.MonoCacheList.GetUpdateTicks.Count; i++)
            _singleton.Data.MonoCacheList.GetUpdateTicks[i].UpdateTick();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _singleton.Data.MonoCacheList.GetFixedTicks.Count; i++)
            _singleton.Data.MonoCacheList.GetFixedTicks[i].FixedTick();
    }
}
