//credit: https://gamedevbeginner.com/interfaces-in-unity/#interface_state_machine

public interface IState
{
    void OnEntry();
    void OnUpdate();
    void OnExit();
}