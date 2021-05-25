using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DAO
{
    public class CheckinDAO
    {
        public static bool ThemMoiNhanVienVaoCheckIn(CheckinDTO chk)
        {
            bool kq;
            string sql = string.Format("insert into QLYNhaHang.[dbo].[checkin] values ('{0}',{1},{2},{3},{4},{5},{6},{7},{8})"
                , chk.MaNV, chk.Checkin1, chk.Checkout1, chk.Checkin2, chk.Checkout2, chk.Checkin3, chk.Checkout3, chk.GioLam,chk.CheckinDungGio);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool CapNhatCheckin(CheckinDTO chk)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[checkin] set  GioIn1={0}, GioIn2={1}, GioIn3={2} where MaNhanVien='{3}'"
                , chk.Checkin1, chk.Checkin2,chk.Checkin3,chk.MaNV);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool CapNhatCheckinDungGio(CheckinDTO chk)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[checkin] set CheckinDungGio=1 where MaNhanVien='{1}'"
                , chk.CheckinDungGio, chk.MaNV);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool CapNhatCheckOut(CheckinDTO chk)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[checkin] set  GioOut1={0}, GioOut2={1}, GioOut3={2} where MaNhanVien='{3}'"
                , chk.Checkout1, chk.Checkout2, chk.Checkout3, chk.MaNV);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool CapNhatGioLam(CheckinDTO chk)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[checkin] set  GioLam={0} where MaNhanVien='{1}'"
                , chk.GioLam, chk.MaNV);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static DataTable LayDSCheckin()
        {
            string sql = "select * from QLYNhaHang.[dbo].[checkin]";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDSCheckinMotNhanVien(string manv)
        {
            string sql = "select * from QLYNhaHang.[dbo].[checkin] where MaNhanVien='"+manv+"'";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
    }
}
