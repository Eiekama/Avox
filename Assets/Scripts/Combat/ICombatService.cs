using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatService
{
    /// <summary>
    /// Player character's attack.
    /// <br/>
    /// (Enemy's attack will be defined in <c>Enemy</c> class)
    /// </summary>
    void Attack();

    /// <summary>
    /// Player character taking damage.
    /// <br/>
    /// (Since more considerations need to be made when player is taking damage,
    /// this is separate from enemies taking damage, which will be defined in
    /// <c>Enemy</c> class)
    /// </summary>
    /// <param name="amount"></param>
    void TakeDamage(int amount);
}
