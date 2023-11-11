using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScript : MonoBehaviour
{
    private float VanToc=7;
    private float VanTocToiDa = 14f; //Vận tốc tối đa khi giữ phím Z
    private float TocDo=0; //toc do
    private bool DuoiDat=true;//kiem tra duoi dat
    private float NhayCao=500; //get toc do nhay nv
    private float NhayThap=5; //Áp dụng khi mario nhảy thấp
    private float RoiXuong=5; //Luc xuống cho mario
    private bool ChuyenHuong=false;//kiemtrachuyenhuong
    private bool QuayPhai = true;//kiem tra nv quay huong nao
    private float KTGiuPhim = 0.2f;
    private float TGGiuPhim = 0;

    private Rigidbody2D r2d;
    private Animator HoatHoa;

    //Hiện thị cấp độ lớn của Mario
    public int CapDo = 0;
    public bool BienHinh = false;

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
        BanDanVaTangToc();
        //if(bienhinh == true)
        //{
        //    switch(capdo) 
        //    { 
        //        case 0:
        //            {
        //                startcoroutine(mariothunho());
        //                bienhinh = false;
        //                break;
        //            }
        //        case 1:
        //            {
        //                startcoroutine(marioannam());
        //                bienhinh = false;
        //                break;
        //            }
        //        case 2:
        //            {
        //                startcoroutine(marioanhoa());
        //                bienhinh = false;
        //                break;
        //            }
        //        default:bienhinh = false;
        //                break;
        //    }
        //}
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
        if (TocDo > 1) StartCoroutine(MarioChuyenHuong());

    }
    void NhayLen()
    {
        if(Input.GetKeyDown(KeyCode.X)&&DuoiDat==true)
        {
            r2d.AddForce((Vector2.up) * NhayCao);
            DuoiDat = false;
        }
        // áp dụng lực hút trái đất cho mario - rơi xuống nhanh hơn 
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y*(RoiXuong -1)* Time.deltaTime;
        }
        else if (r2d.velocity.y>0 && !Input.GetKey(KeyCode.X))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag =="NenDat")
        {
            DuoiDat = true;
        }    
    }

    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }

    //Bắn đạn và chạy nhanh hơn 
    void BanDanVaTangToc()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            TGGiuPhim += Time.deltaTime;
            if (TGGiuPhim < KTGiuPhim)
            {
                print("Đang bắn đạn chíu chíu");
            }
            else
            {
                VanToc = VanToc * 1.1f;
                if (VanToc > VanTocToiDa)
                    VanToc = VanTocToiDa;
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            VanToc = 7f;
            TGGiuPhim = 0;
        }
    }

    //Thay đổi lớn của Mario
    //IEnumerator MarioAnNam()
    //{
    //    float DoTre = 0.1f;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);
    //}

    //IEnumerator MarioAnHoa()
    //{
    //    float DoTre = 0.1f;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 1;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 1;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 1;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 1;
    //    yield return new WaitForSeconds(DoTre);
    //}

    //IEnumerator MarioThuNho()
    //{
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);

    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioNho"), 1;
    //    HoatHoa.SetLayerWeight(HoatHoa.("MarioLon"), 0;
    //    HoatHoa.SetLayerWeight(HoatHoa.("AnHoa"), 0;
    //    yield return new WaitForSeconds(DoTre);
    //}
}
