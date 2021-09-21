using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics {
    public class CinematicControlRemover : MonoBehaviour {
        private GameObject player;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
        }

        private void OnEnable() {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable() {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        private void DisableControl(PlayableDirector pd) {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControl(PlayableDirector pd) {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
