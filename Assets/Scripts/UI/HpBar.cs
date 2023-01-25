using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    PlayerVer2 player;
    // Start is called before the first frame update
    private void Update()
    {
        Slider slider = GetComponent<Slider>();
        slider.value = player.currentHp;

    }
}
