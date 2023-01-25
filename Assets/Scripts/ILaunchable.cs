using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaunchable {
    bool BeingLaunched { get; set; }
    bool HittingSomething { get; set; }
    bool InRiver { get; set; }

    void ResetObject();
}
