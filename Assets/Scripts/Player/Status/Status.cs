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
            
            if(_player.data.currentHP == 0){
                _player.combat.Die();
            }
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
            if (player.movement.lastOnGroundTime > 0
                && player.data.currentMP < player.data.maxMP)
            {
                return true;
            }

            return false;
        }

        public IEnumerator RecoverMP()
        {
            float rechargeAmount = 0.0f;
            while (true)
            {
                yield return new WaitForFixedUpdate();
                if (CanRecoverMP())
                {
                    rechargeAmount += Time.deltaTime;
                    if (rechargeAmount > player.data.MPRecoveryRate)
                    {
                        ChangeCurrentMP(1);
                        rechargeAmount = 0;
                    }
                } else
                {
                    rechargeAmount = 0;
                }
            }
        }
    }
}