using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Player ���� ������ ���� PlayerPattern Ŭ���� (1.29 ���� �߰�)
public class DefaultOnetilePattern : PlayerPattern
{
    public override int[] calculateIndex(int currentInd)
    {
        // pattern �迭�� ������ �迭�� �ε������� ��� (1.29 ���� �߰�)
        int[] pattern = new int[1];
        for (int i = 0; i < pattern.Length; i++)
        {
            currentInd += 3;
            pattern[i] = currentInd;
            Debug.Log(currentInd);
        }
        return pattern;
    }
}
