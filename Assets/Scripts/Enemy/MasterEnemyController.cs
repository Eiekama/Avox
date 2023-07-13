//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

using UnityEngine;

public class MasterEnemyController : MonoBehaviour
{
    AStateManager[] managers;

    private void Awake()
    {
        managers = GetComponentsInChildren<AStateManager>();
    }

    private void Start()
    {
        foreach (var manager in managers)
        {
            manager.ChangeState(manager.defaultState);
        }
    }

    private void Update()
    {
        foreach (var manager in managers)
        {
            manager.currentState.OnUpdate(manager);
        }
    }
}
