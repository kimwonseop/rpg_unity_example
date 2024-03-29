using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;


namespace RPG.Stats {
    public class Experience : MonoBehaviour, ISaveable {
        [SerializeField]
        private float experiencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float experience) {
            experiencePoints += experience;
            onExperienceGained();
        }

        public object CaptureState() {
            return experiencePoints;
        }

        public float GetPoints() {
            return experiencePoints;
        }

        public void RestoreState(object state) {
            experiencePoints = (float)state;
        }
    }
}

