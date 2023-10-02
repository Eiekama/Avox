using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Player
{
    public class Status : ASystem, IStatus
    {


        public void ChangeMaxHP(int amount) {
            _player.data.maxHP += amount;
        }

        public void ChangeCurrentHP(int amount) {
            _player.data.currentHP += amount;
            if (StatusHUD.instance)
                StatusHUD.instance.UpdateHud();
        }

        public void ChangeMaxMP(int amount) {
            _player.data.maxMP += amount;
        }

        public void ChangeCurrentMP(int amount) {
            _player.data.currentMP += amount;
            if (StatusHUD.instance)
                StatusHUD.instance.UpdateHud();
        }

        private bool CanRecoverMP()
        {
            // ADD IMPLEMENTATION HERE
            return true; //replace
        }

        public IEnumerator RecoverMP()
        {
            float rechargeAmount = 0.0f;
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                if (CanRecoverMP())
                {
                    rechargeAmount += _player.data.MPRecoveryRate;
                    if (rechargeAmount > 1.0f)
                    {
                        ChangeCurrentMP(Mathf.FloorToInt(rechargeAmount));
                        rechargeAmount %= 1.0f;
                    }
                }
            }
        }
    }
}