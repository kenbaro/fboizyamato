using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DAO
{
    public class CTHoaDonDAO
    {
        //Rút trích dữ liêu: select 
        public static DataTable LayDSCTHDTuMaHD(int maHD)
        {
            string sql = "select TenThucDon as 'Tên TĐ', DonGia as 'Đơn Giá', SoLuong as 'Số Lượng' from QLYNhaHang.[dbo].[ChiTietHD] ct, QLYNhaHang.[dbo].[ThucDon] td where ct.MaThucDon = td.MaThucDon and ct.SoHD = " + maHD;
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDSCTHD(int SoHD)
        {
            string sql = string.Format("select * from QLYNhaHang.[dbo].[ChiTietHD] where SoHD = {0}", SoHD);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static bool ThemChiTietHoaDon(CTHoaDonDTO cthd)
        {
            bool kq;
            string sql = string.Format("insert into QLYNhaHang.[dbo].[ChiTietHD] values ({0}, {1}, {2}, {3})", cthd.SoHD, cthd.MaTD, cthd.SoLuong, cthd.DonGia);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool XoaCTHDTheoSoHD(int soHD)
        {
            bool kq;
            string sql = string.Format("delete QLYNhaHang.[dbo].[ChiTietHD] where SoHD = {0}", soHD);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool XoaCTHDTheoSoHDVaMaTD(int soHD, int maTD)
        {
            bool kq;
            string sql = string.Format("delete QLYNhaHang.[dbo].[ChiTietHD] where SoHD = {0} and MaThucDon = {1}", soHD, maTD);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static int MaTuTang()
        {
            string sql = "select * from QLYNhaHang.[dbo].[ChiTietHD]";
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
