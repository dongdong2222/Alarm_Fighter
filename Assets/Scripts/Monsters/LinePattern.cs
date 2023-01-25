using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinePattern :MonsterPattern
{
    public override int[] calculateIndex(int currentIndex)
    {
        int[] index = new int[3];
        int gridIndex = Managers.Field.GetIndex(currentIndex);
        for(int i=0; i<3; i++)
        {
            gridIndex -= 3;
            index[i] = gridIndex;
        }

        return index;
    }


}
