using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound//���� Ŭ����
{
    public string name;
    public int bpm;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] bgm = null;
    [SerializeField] Sound[] sfx = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    public static AudioManager audioManager;

    private void Start()
    {
        audioManager = this;
    }

    public void PlayBGM(string name)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i].name == name)
            {
                bgmPlayer.clip = bgm[i].clip;
                Managers.Bpm.SetBpm(bgm[i].bpm);//���� �ڵ�
                bgmPlayer.Play();
                return;
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string name)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfx[i].name == name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("��� �÷��̾ ������Դϴ�.");
            }
        }
        Debug.Log(name + "���带 ã�� �� �����ϴ�.");
    }


}
