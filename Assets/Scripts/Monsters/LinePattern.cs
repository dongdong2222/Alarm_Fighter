using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePattern : MonsterPattern
{
    public override int[] calculateIndex(int currentInd)
    {
        // ���Ͱ� �ڱ� �ڽ��� �ִ� �ڸ��� �ε����� �����ϸ� �ȵ� (1.29 ���� ����)
        int[] pattern = new int[2];
        for(int i = 0; i < pattern.Length; i++)
        {
            currentInd += 3;
            pattern[i] = currentInd;

        }
        return pattern;
    }
}
