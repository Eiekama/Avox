//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

using UnityEngine;

public class MasterEnemyController : MonoBehaviour
{
    private AStateManager[] managers;

    private void Awake()
    {
        managers = GetComponentsInChildren<AStateManager>();
    }

    private void Start()
    {
        foreach (var manager in managers)
        {
            manager.ChangeState(manager.startState);
        }
    }

    private void Update()
    {
        foreach (var manager in managers)
        {
            if (manager.isActiveAndEnabled) { manager.currentState.OnUpdate(); }
        }
    }
}
