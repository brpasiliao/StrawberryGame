using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
    bool HittingSomething { get; set; }
    bool InRiver { get; set; }

    void ThrownCollisionEnter(Collision2D collision);
    void ThrownCollisionExit(Collision2D collision);
    void ThrownTriggerEnter(Collider2D collision);
    void ThrowingObject();
    void ResetObject();
}
