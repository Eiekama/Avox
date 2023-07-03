using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusService : IStatusService
{
    private readonly PlayerData _data = PlayerData.Instance;

    public void ChangeMaxHP(int amount) { _data.maxHP += amount; }
    public void ChangeCurrentHP(int amount) { _data.currentHP += amount; }
    public void ChangeMaxMP(int amount) { _data.maxMP += amount; }
    public void ChangeCurrentMP(int amount) { _data.currentMP += amount; }

    private bool CanRecoverMP()
    {
        // ADD IMPLEMENTATION HERE
        return false; //replace
    }

    public IEnumerator RecoverMP()
    {
        float rechargeAmount = 0.0f;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (CanRecoverMP())
            {
                rechargeAmount += _data.MPRecoveryRate;
                if (rechargeAmount > 1.0f)
                {
                    ChangeCurrentMP(Mathf.FloorToInt(rechargeAmount));
                    rechargeAmount %= 1.0f;
                }
            }
        }
    }
}