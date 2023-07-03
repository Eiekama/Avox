using System.Collections;

public interface IStatusService
{
    void ChangeMaxHP(int amount);
    void ChangeCurrentHP(int amount);
    void ChangeMaxMP(int amount);
    void ChangeCurrentMP(int amount);
    IEnumerator RecoverMP();
}
