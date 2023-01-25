using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable {
    bool HittingSomething { get; set; }
    bool InRiver { get; set; }

    void ThrowingObject();
    void ResetObject();
}
