using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource moneySource;

    [SerializeField] AudioSource denySource;

    [SerializeField] private AudioSource placeSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMoney()
    {
        moneySource.Play();
    }

    public void playDeny()
    {
        denySource.Play();
    }

    public void playPlace()
    {
        placeSource.Play();
    }
}
