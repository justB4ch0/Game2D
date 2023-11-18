using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatDichuyenDuocScript : MonoBehaviour
{
   
    private void Oncollisiontater2D(collision2D collision)
    {
        if (collision.collider.tag != "Player" && collision.contacts[0].normal.x > 0)
        {
            DiChuyenTrai = true;
            QuayMat();
        }

        else if(collision.collider.tag == "Player" && collision.contacts[0].normal.x > 0)
        { 
            DiChuyenTrai - false;
            QuayMat();
        }

        void QuayMat()
        {
            DiChuyenTrai = !DiChuyenTrai;

            Vector2 HuongQuay = transform.localscale;
            HuongQuay.x *= -1;
            transform.localScale HuongQuay;
        }

        
        
    }
}
