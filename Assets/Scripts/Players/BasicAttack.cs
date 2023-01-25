using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : PlayerPattern
{
    public override int[] calculateIndex(int currentIndex)
    {
        int[] pattern = new int[2];
        //int gridIndex = Managers.Field.GetIndex(currentIndex, 1);
        for(int i=0; i<pattern.Length; i++)
        {
            currentIndex += 3;
            pattern[i] = currentIndex;
        }
        return pattern;
    }
}
