using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePattern :MonsterPattern
{
    public override int[] calculateIndex(int currentIndex)
    {
        int[] index = new int[4];

        for(int i=0; i<4; i++)
        {
            index[i] = currentIndex - 3;
        }

        return index;
    }


}
