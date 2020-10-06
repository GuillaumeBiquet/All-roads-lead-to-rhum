using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhumBarrel : RhumBottle
{
    public override void PlayerCollide()
    {
        if (!m_AlreadyHit)
        {
            ScoreScript.UpdateBarrelValue(m_Score);
            m_AlreadyHit = true;
            Destroy(gameObject);
        }
    }
}
