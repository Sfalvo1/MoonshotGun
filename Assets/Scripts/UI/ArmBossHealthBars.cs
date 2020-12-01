using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBossHealthBars : MonoBehaviour
{

    [SerializeField] ArmHealth[] armHealthBars;

    public ArmHealth GetHealthBar(int index)
    {
        return armHealthBars[index];
    }

}
