using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCollider : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent<IReset>(out var obj)) {
            obj.OnReset();
        }
    }
}
