using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaunchable {
    Vector3 PosOG { get; set; }
    bool BeingLaunched { get; set; }
    bool HittingSomething { get; set; }
    bool InRiver { get; set; }

    void ResetObject();
}
