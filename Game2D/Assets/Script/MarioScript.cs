using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScript : MonoBehaviour
{
    public float VanToc;
    private float TocDo=0; //toc do
    private bool DuoiDat=true;//kiem tra duoi dat
    public float NhayCao; //get toc do nhay nv
    public float RoiXuong;
    public float NhayThap;
    private bool ChuyenHuong=false;//kiemtrachuyenhuong
    private bool QuayPhai = true;//kiem tra nv quay huong nao

    private Rigidbody2D r2d;
    private Animator HoatHoa;

    // Start is called before the first frame update
    void Start()
    {
        r2d= GetComponent<Rigidbody2D>();
        HoatHoa = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HoatHoa.SetFloat("TocDo", TocDo);
        HoatHoa.SetBool("DuoiDat", DuoiDat);
        HoatHoa.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
    }
    private void FixedUpdate()
    {
        DiChuyen();
    }
    void DiChuyen() 
    {
        //chon nut di chuyen (mui ten, A D)
        float PhimNhanPhaiTrai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc*PhimNhanPhaiTrai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimNhanPhaiTrai);
        if (PhimNhanPhaiTrai > 0 && !QuayPhai) HuongMario();
        if (PhimNhanPhaiTrai < 0 && QuayPhai) HuongMario();
    }
    void HuongMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;

    }
    void NhayLen()
    {
        if(Input.GetKeyDown(KeyCode.X)&&DuoiDat==true)
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            DuoiDat = false;
        }
        //
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong-1)*Time.deltaTime;
        }
        else if(r2d.velocity.y>0&&! Input.GetKey(KeyCode.X))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;

        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="NenDat")
        {
            DuoiDat=true;
        }       
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }
}
