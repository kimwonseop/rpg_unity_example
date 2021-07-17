using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {
        [SerializeField]
        private float timeBetweenAttacks = 1f;
        [SerializeField]
        private Transform rightHandTransform = null;
        [SerializeField]
        private Transform leftHandTransform = null;
        [SerializeField]
        private Weapon defaultWeapon = null;
 
        private float timeSinceLastAttack = Mathf.Infinity;
        private Health target;
        private Weapon currentWeapon = null;

        private void Start() {
            EquipWeapon(defaultWeapon);
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) {
                return;
            }

            if (target.IsDead()) {
                return;
            }

            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon) {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void AttackBehaviour() {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks) {
                //This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack() {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        public bool CanAttack(GameObject combatTarget) {
            if(combatTarget == null) {
                return false;
            }

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel() {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack() {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Animation Event
        public void Hit() {
            if(target == null) {
                return;
            }

            target.TakeDamage(currentWeapon.GetDamage());
        }
    }
}