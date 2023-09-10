//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

public interface IState
{
    void OnEntry(AStateManager manager);
    void OnUpdate(AStateManager manager);
    void OnExit(AStateManager manager);
}