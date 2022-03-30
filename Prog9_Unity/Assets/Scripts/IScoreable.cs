using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreable {
    int scorePoints { get; set; }
    void OnScore();
}
