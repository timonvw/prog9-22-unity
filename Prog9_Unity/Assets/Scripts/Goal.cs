using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent<IScoreable>(out var obj)) {
            obj.OnScore();
            GameMaster.Instance.score += obj.scorePoints;
            StartCoroutine(TimeScaleReset());
        }
    }

    private IEnumerator TimeScaleReset() {
        Time.timeScale = 0.6f;  
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
    }
}
