using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputManager
{
    float steer { get; set; }
    float throttle { get; set; }
    bool brake { get; set; }
}
