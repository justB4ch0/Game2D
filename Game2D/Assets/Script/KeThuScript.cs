using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeThuScript : MonoBehaviour
{
    GameObject Mario;
    Vecto2 ViTriChet;

    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        ViTriChet = transfrom.localPosition;
    }

    private void OnCollisionEnter20(Collision20 collision)
    {
        if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x > || collision.contacts[0].normal.x))
        {
            if (Mario.GetComponent<MarioScript>().CapDo > 1)
            {
                Mario.GetComponent<MarioScript>().CapDo = 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
            }
            else
            {
                Mario.GetComponent<MarioScript>().MarioChet();
            }
        }
        if (collision.collider.tag == "Player" && collision.contacts[0].normal.y < 0)
        {
            Destroy(gameObject);

        }
    }
}
