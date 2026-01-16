using UnityEngine;
using TMPro; // Thư viện chữ đẹp

public class HienThiLog : MonoBehaviour
{
    public TextMeshProUGUI bangChu; // Nơi sẽ hiện chữ
    string noiDungLog = "";

    void OnEnable()
    {
        // Đăng ký nhận tin nhắn từ hệ thống
        Application.logMessageReceived += XuLyLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= XuLyLog;
    }

    void XuLyLog(string logString, string stackTrace, LogType type)
    {
        // Tô màu cho đẹp: Lỗi thì màu đỏ, Log thường thì màu trắng
        string mauSac = "white";
        if (type == LogType.Error || type == LogType.Exception) mauSac = "red";
        else if (type == LogType.Warning) mauSac = "yellow";

        // Cộng dồn log mới vào đầu dòng (Mới nhất hiện trên cùng)
        noiDungLog = $"<color={mauSac}>{logString}</color>\n" + noiDungLog;

        // Giới hạn độ dài để không bị lag (chỉ giữ 1000 ký tự)
        if (noiDungLog.Length > 2000)
        {
            noiDungLog = noiDungLog.Substring(0, 2000);
        }

        bangChu.text = noiDungLog;
    }
}