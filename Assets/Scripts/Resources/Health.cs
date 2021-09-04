using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources {
    public class Health : MonoBehaviour, ISaveable {
        [SerializeField]
        private float healthPoints = 100f;
        private bool isDead = false;

        private void Start() {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead() {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage) {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0) {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator) {
            var experience = instigator.GetComponent<Experience>();

            if (experience == null) {
                return;
            }

            experience.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
        }

        public float GetPercentage() {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void Die() {
            if (isDead) {
                return;
            }

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState() {
            return healthPoints;
        }

        public void RestoreState(object state) {
            healthPoints = (float)state;

            if (healthPoints == 0) {
                Die();
            }
        }
    }
}
