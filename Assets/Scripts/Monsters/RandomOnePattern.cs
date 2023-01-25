using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOnePattern : MonsterPattern
{
    public override int[] calculateIndex(int currentIndex)
    {
        int[] index = new int[1];
        index[0] = (int)Random.Range(0f, 8f);
        return index;

    }
}
