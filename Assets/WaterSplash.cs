using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplash : MonoBehaviour
{
    public GameObject waterDrop;
    public float height;
    public ParticleSystem splash;
    void Start()
    {
        Instantiate(waterDrop);
        waterDrop.transform.position = new Vector2(this.transform.position.x , this.transform.position.y  + height);
        waterDrop.transform.localScale = new Vector3(10.2f, 10.2f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        splash.Play();
        Start();
    }
}
