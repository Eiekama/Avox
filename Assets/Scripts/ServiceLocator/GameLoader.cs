// credit: https://briantria.com/ioc-service-locator-unity/#service_locator_singleton

using UnityEngine;

public class GameLoader : MonoBehaviour
{
    private void Awake()
    {
        IServiceLocator serviceLocator = ServiceLocator.Instance;
        serviceLocator.Register<IStatusService>(new StatusService());
    }
}
