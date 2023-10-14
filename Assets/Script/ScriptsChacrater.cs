using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsChacrater : MonoBehaviour
{
    public float VanToc;
    private float TocDo=0;//set speed
    private bool DuoiDat = true;//check don tho glitch
    private bool ChuyenHuong = false;//quay xe
    private Animator Ani;
    private Rigidbody2D r2d;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ani.SetFloat("TocDo", TocDo);
        Ani.SetBool("DuoiDat", DuoiDat);
        Ani.SetBool("ChuyenHuong", ChuyenHuong);
    }

    private void FixedUpdate()
    {
        DiChuyen();
    }
    void DiChuyen()
    {
        //chon nut di chuyen
        float NutDiChuyenTraiNPhai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc* NutDiChuyenTraiNPhai, r2d.velocity.y);
    }
   

}
