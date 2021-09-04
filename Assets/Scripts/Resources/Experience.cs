using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;


namespace RPG.Resources {
    public class Experience : MonoBehaviour, ISaveable {
        [SerializeField]
        private float experiencePoints = 0;

        public object CaptureState() {
            return experiencePoints;
        }

        public void GainExperience(float experience) {
            experiencePoints += experience; 
        }

        public void RestoreState(object state) {
            experiencePoints = (float)state;
        }
    }
}

