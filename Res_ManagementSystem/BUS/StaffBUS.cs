using Res_ManagementSystem.DAO;
using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.BUS
{
    public class StaffBUS
    {
        public static DataTable LayDSNhanVienCoMK()
        {
            DataTable _ds = StaffDAO.LayDSNhanVienCoMK();
            return _ds;
        }
        public static DataTable LayDSNhanVien()
        {
            DataTable _ds = StaffDAO.LayDSNhanVien();
            return _ds;
        }
        public static DataTable LayDSToanBoNhanVien()
        {
            DataTable _ds = StaffDAO.LayDSToanBoNhanVien();
            return _ds;
        }
        public static DataTable LayDSQuanLy()
        {
            DataTable _ds = StaffDAO.LayDSQuanly();
            return _ds;
        }
        public static bool KiemTraTenDNTonTai(string tenDN, string MaNV)
        {
            bool kq = StaffDAO.KiemTraTenDNTonTai(tenDN, MaNV);
            return kq;
        }
        public static bool ThemNhanVien(StaffDTO nv)
        {
            bool kq = StaffDAO.ThemNhanVien(nv);
            return kq;
        }
        public static bool XoaNhanVien(string manv)
        {
            bool kq = StaffDAO.XoaNhanVien(manv);
            return kq;
        }
        public static bool CapNhatLuongNhanVien(string manv, float salary)
        {
            bool kq = StaffDAO.CapNhatLuongNhanVien(manv, salary);
            return kq;
        }
        public static string TraCuuManvTheoTenDN(string user)
        {
            string manv = StaffDAO.TraCuuManvTheoTenDN(user);
            return manv;
        }
        public static DataTable TraCuuNhanVienTheoTen(string tenNV)
        {
            DataTable _ds = StaffDAO.TraCuuNhanVienTheoTen(tenNV);
            return _ds;
        }
        public static DataTable TraCuuNhanVienTheoMaNV(string maNV)
        {
            DataTable _ds = StaffDAO.TraCuuNhanVienTheoMaNV(maNV);
            return _ds;
        }
        public static bool CapNhatNhanVien(StaffDTO nv)
        {
            bool kq = StaffDAO.CapNhatNhanVien(nv);
            return kq;
        }
        public static string LayMatKhauTuTenDN(string tennv)
        {
            string MK = StaffDAO.LayMatKhauTuTenDN(tennv);
            return MK;
        }
    }
}
