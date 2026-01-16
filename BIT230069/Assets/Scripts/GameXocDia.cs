using UnityEngine;
using UnityEngine.UI;
using TMPro; // Bắt buộc có dòng này để dùng chữ TextMeshPro

public class GameXocDia : MonoBehaviour
{
    // --- KHAI BÁO BIẾN (Để tí nữa kéo thả) ---
    [Header("Kéo hình ảnh vào đây")]
    public Image hinhXucXac1;      // Tí nữa kéo dice1 vào đây
    public Image hinhXucXac2;      // Tí nữa kéo dice2 vào đây
    public Sprite[] danhSachAnhDice; // Tí nữa kéo 6 cái ảnh xúc xắc vào đây
    public GameObject caiBat;      // Tí nữa kéo Bowl vào đây

    [Header("Kéo chữ và nút vào đây")]
    public TextMeshProUGUI textKetQua; // Tí nữa kéo Text (TMP) vào đây
    public Button nutXoc;          // Tí nữa kéo nút roll vào đây
    public Button nutChan;         // Tí nữa kéo nút chan vào đây
    public Button nutLe;           // Tí nữa kéo nút le vào đây

    // --- BIẾN NGẦM ---
    private int tongDiem = 0;
    private bool daXocXong = false;

    void Start()
    {
        // Mới vào game: Mở bát cho xem, khóa nút chọn
        caiBat.SetActive(false); 
        KhoaNutChon(true); 
    }

    // --- HÀM CHO NÚT XÓC (ROLL) ---
    public void BamNutXoc()
    {
        // 1. Úp bát
        caiBat.SetActive(true);
        textKetQua.text = "Đang lắc... Chọn Chẵn hay Lẻ?";
        
        // 2. Tính toán ngầm
        int mat1 = Random.Range(0, 6); // Random từ 0-5
        int mat2 = Random.Range(0, 6);
        
        // Gán hình xúc xắc (Người chơi chưa thấy vì bát đang úp)
        hinhXucXac1.sprite = danhSachAnhDice[mat1];
        hinhXucXac2.sprite = danhSachAnhDice[mat2];
        
        tongDiem = (mat1 + 1) + (mat2 + 1);

        // 3. LOGGING (Đề bài yêu cầu): Hiện ra console
        Debug.Log($"[SERVER] Đã xóc xong! Kết quả ngầm: {mat1+1} và {mat2+1}. Tổng = {tongDiem}");

        // 4. Mở khóa nút chọn
        daXocXong = true;
        KhoaNutChon(false);
        nutXoc.interactable = false; // Khóa nút Xóc
    }

    // --- HÀM CHO NÚT CHẴN ---
    public void ChonChan()
    {
        KiemTraKetQua(true); // true = Chẵn
    }

    // --- HÀM CHO NÚT LẺ ---
    public void ChonLe()
    {
        KiemTraKetQua(false); // false = Lẻ
    }

    // --- HÀM KIỂM TRA THẮNG THUA ---
    void KiemTraKetQua(bool chonChan)
    {
        if (!daXocXong) return;

        // Log lựa chọn
        Debug.Log($"[PLAYER] Người chơi chọn: {(chonChan ? "CHẴN" : "LẺ")}");

        // Mở bát
        caiBat.SetActive(false);

        bool ketQuaLaChan = (tongDiem % 2 == 0);
        string tenKetQua = ketQuaLaChan ? "CHẴN" : "LẺ";
        Debug.Log($"[RESULT] Kết quả thật: {tongDiem} ({tenKetQua})");

        if (chonChan == ketQuaLaChan)
        {
            textKetQua.text = $"Về {tongDiem} ({tenKetQua}). BẠN THẮNG!";
            textKetQua.color = Color.green;
        }
        else
        {
            textKetQua.text = $"Về {tongDiem} ({tenKetQua}). BẠN THUA!";
            textKetQua.color = Color.red;
        }

        // Reset
        daXocXong = false;
        KhoaNutChon(true);
        nutXoc.interactable = true;
    }

    void KhoaNutChon(bool khoa)
    {
        nutChan.interactable = !khoa;
        nutLe.interactable = !khoa;
    }
}