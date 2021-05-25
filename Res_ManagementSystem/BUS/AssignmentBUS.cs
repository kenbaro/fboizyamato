using Res_ManagementSystem.DAO;
using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;


namespace Res_ManagementSystem.BUS
{
    public class AssignmentBUS
    {
        public static DataTable LayDSChiaCa()
        {
            DataTable _ds = DAO.AssignmentDAO.LayDsChiaca();
            return _ds;
        }
        public static DataTable LayDSChiaCaTuMaNV(string manv)
        {
            DataTable _ds = DAO.AssignmentDAO.LayDsChiacaTuMaNV(manv);
            return _ds;
        }
        public static bool ThemPhanCong(AssignmentDTO agm)
        {
            bool kq = AssignmentDAO.ThemPhanCong(agm);
            return kq;
        }
        public static bool XoaPhanCong(string manv)
        {
            bool kq = AssignmentDAO.XoaPhanCong(manv);
            return kq;
        }
        public static int LayCaTheoGio(int gio)
        {
            int ca = AssignmentDAO.LayCaTheoGio(gio);
            return ca;
        }
        public static bool ThemLuongTheoNgay(float salary,float psh_salary,string manv)
        {
            bool kq = AssignmentDAO.ThemLuongTheoNgay(salary,psh_salary,manv);
            return kq;
        }

        public static DataTable LayLuongNhanVienCungCa( int ca)
        {

            DataTable _ds = AssignmentDAO.LayLuongNhanVienCungCa(ca);
            return _ds;
        }
        public static DataTable KiemTraCacNVDiTre(int catre)
        {
            DataTable _ds = DAO.AssignmentDAO.KiemTraCacNVDTre(catre);
            return _ds;
        }
        public static bool ThemPhanCongTheoMaTuTang(StaffDTO nv)
        {
            int[,] Assignment_Array = DataProvider.Assignment();
            AssignmentDTO pc = new AssignmentDTO();
            int maTuTang = StaffDAO.MaTuTang() - 2;// trừ 2 vì trong danh sách có 2 người quản lý 
            pc.SID = nv.StaffID;
            pc.Ca1 = Assignment_Array[(maTuTang-2) % 4, 0];
            pc.Ca2 = Assignment_Array[(maTuTang-2) % 4, 1];
            pc.Ca3 = Assignment_Array[(maTuTang-2) % 4, 2];
            pc.Salary = 0;
            bool kq = false;
            if (ThemPhanCong(pc)==true)
            {
                kq = true;
            }
            return kq;
        }
        public static bool ThemTienThuong(float exsalary, int ca)
        {
            bool kq = AssignmentDAO.ThemTienThuong(exsalary, ca);
            return kq;
        }
        public static bool ThemGioTreCaTre(string manv, int sogiocham, int ca)
        {
            bool kq = AssignmentDAO.ThemGioTreCaTre(manv, sogiocham, ca);
            return kq;
        }
        public static DataTable NVLamTrongCa(string ca, string manv)
        {
            DataTable _ds = AssignmentDAO.NVLamTrongCa(ca, manv);
            return _ds;
        }
        public static DataTable KiemTraMaNVRong(string maNV)
        {
            DataTable dt = AssignmentDAO.KiemTraMaNVRong(maNV);
            return dt;
        }
        public static bool CapNhatCheckin(string manv)
        {
            bool kq = AssignmentDAO.CapNhatCheckin(manv);
            return kq;
        }
        public static float TongTienPhatTrong1Ca(int catre)
        {
            float sum = AssignmentDAO.TongTienPhatTrong1Ca(catre);
            return sum;
        }
    }
}
