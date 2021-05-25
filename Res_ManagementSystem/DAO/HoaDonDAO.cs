using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DAO
{
    public class HoaDonDAO
    {
        //Rút trích dữ liêu: select 
        public static DataTable LayDSHoaDon()
        {
            string sql = "select hd.SoHD as 'Số HĐ', hd.ThoiGianLap as 'TG Lập', hd.MaSoBan as 'MS Bàn', hd.SoKhach as 'Số Khách', nv.DisplayName as 'Người Lập', hd.TongTien as 'Tổng Tiền' from QLYNhaHang.[dbo].[HoaDon] hd, QLYNhaHang.[dbo].[Account] nv where nv.sID = hd.MaNVLap and nv.sID = hd.MaNVTT";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }

        public static int LaySoHoaDonTuMaBan(int maBan)
        {
            int soHD = 0;
            string sql = "select * from QLYNhaHang.[dbo].[HoaDon] where MaSoBan = " + maBan + " and TongTien = 0";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                soHD = int.Parse(dt.Rows[0]["SoHD"].ToString());
            }
            return soHD;
        }
        public static int LaySoKhachTuSoHD(int soHD)
        {
            int soKhach = 0;
            string sql = "select * from QLYNhaHang.[dbo].[HoaDon] where SoHD = " + soHD;
            DataTable dt = DataProvider.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                soKhach = int.Parse(dt.Rows[0]["SoKhach"].ToString());
            }
            return soKhach;
        }

        public static List<int> LayDSBanChuaThanhToan()
        {
            List<int> _ds = new List<int>();
            string sql = "select * from QLYNhaHang.[dbo].[HoaDon] where TongTien = 0";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int maBan = int.Parse(dt.Rows[i]["MaSoBan"].ToString());
                _ds.Add(maBan);
            }
            return _ds;
        }

        public static int LayGioLapHDChuaThanhToanTheoMaBan(int maBan)
        {
            string sql = string.Format("select convert(varchar(2), ThoiGianLap, 108)as 'GioLap' from QLYNhaHang.[dbo].[HoaDon] where MaSoban = {0} and TongTien = 0", maBan);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            int gio = int.Parse(dt.Rows[0]["GioLap"].ToString());
            return gio;
        }

        public static bool LapHoaDon(HoaDonDTO hd)
        {
            hd.SoHD = MaTuTang();
            string sql = string.Format("set dateformat DMY insert into QLYNhaHang.[dbo].[HoaDon] values ({0}, '{1}', {2}, {3}, '{4}', '{5}', {6})", hd.SoHD, DateTime.Now, hd.MsBan, hd.SoKhach, hd.MsNVLap, hd.MsNVTT, hd.TongTien);
            bool kq;
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool CapNhatLapHoaDon(HoaDonDTO hd)
        {
            string sql = string.Format("set dateformat DMY update QLYNhaHang.[dbo].[HoaDon] set ThoiGianLap = '{0}', MaNVTT = '{1}', TongTien = {2} where SoHD = {3}", DateTime.Now, hd.MsNVTT, hd.TongTien, hd.SoHD);
            bool kq;
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }

        public static bool CapNhatSoKhach(int SoKhach, int SoHD)
        {
            string sql = string.Format("update QLYNhaHang.[dbo].[HoaDon] set SoKhach = {0} where SoHD = {1}", SoKhach, SoHD);
            bool kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }

        public static DataTable ThongKeHDTheoNgay(DateTime ngay)
        {
            string sql = "set dateformat DMY select SoHD as 'Số HĐ', ThoiGianLap as 'Thời Gian Lập', MaSoBan as 'Mã Bàn', SoKhach as 'Số Khách', nv.Displayname as 'Người Lập', TongTien as 'Tổng Tiền' from QLYNhaHang.[dbo].[HoaDon], QLYNhaHang.[dbo].[Account] nv where QLYNhaHang.[dbo].[HoaDon].MaNVLap = nv.sID and QLYNhaHang.[dbo].[HoaDon].MaNVTT = nv.sID and convert(varchar(10), ThoiGianLap,103) = convert(varchar(10), convert(datetime, '" + ngay + "'), 103)";
            DataTable kq = DataProvider.ExecuteQuery(sql);
            return kq;
        }

        public static DataTable ThongKeHDTheoThang(int thang, int nam)
        {
            string sql = string.Format("select hd.SoHD as 'Số HĐ', hd.ThoiGianLap as 'Thời Gian Lập', hd.MaSoBan as 'Mã Bàn', hd.SoKhach as 'Số Khách', nv.Displayname as 'Người Lập', hd.TongTien as 'Tổng Tiền' from QLYNhaHang.[dbo].[HoaDon] hd, QLYNhaHang.[dbo].[Account] nv where hd.MaNVLap = nv.sID and hd.MaNVTT = nv.sID and convert(nvarchar(10), ThoiGianLap, 103) like '%{0}/{1}'", thang, nam);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }

        public static DataTable ThongKeHDTheoKhoangNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = string.Format("set dateformat DMY select SoHD as 'Số HĐ', ThoiGianLap as 'Thời Gian Lập', MaSoBan as 'Mã Bàn', SoKhach as 'Số Khách', nv.DisplayName as 'Người Lập', TongTien as 'Tổng Tiền' from QLYNhaHang.[dbo].[HoaDon], QLYNhaHang.[dbo].[Account] nv where QLYNhaHang.[dbo].[HoaDon].MaNVLap = nv.sID and QLYNhaHang.[dbo].[HoaDon].MaNVTT = nv.sID and convert(varchar(10), ThoiGianLap,103) >= convert(varchar(10),convert(datetime,'{0}'), 103) and convert(varchar(10), ThoiGianLap,103) <= convert(varchar(10),convert(datetime,'{1}'), 103)", tuNgay, denNgay);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }

        public static DataTable LayCacThangLapHD()
        {
            string sql = "select MONTH(ThoiGianLap) as ThangLap from QLYNhaHang.[dbo].[HoaDon] group by MONTH(ThoiGianLap)";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static bool XoaHDTheoSoHD(int SoHD)
        {
            bool kq;
            string sql = string.Format("delete QLYNhaHang.[dbo].[HoaDon] where SoHD = {0}", SoHD);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static string LayMaNVTTTuSoHD(int soHD)
        {
            string manv = "";
            string sql = string.Format("select * from QLYNhaHang.[dbo].[HoaDon] where SoHD = {0}", soHD);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            if(dt.Rows.Count>0)
            {
                manv = dt.Rows[0][5].ToString();
            }
            return manv;

        }

        public static int MaTuTang()
        {
            string sql = "select * from QLYNhaHang.[dbo].[HoaDon]";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            int maTuTang = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (int.Parse(dt.Rows[i][0].ToString()) != maTuTang)
                {
                    return maTuTang;
                }
                maTuTang++;
            }
            return maTuTang;
        }
    }
}
