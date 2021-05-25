using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DAO
{
    public class StaffDAO
    {
        public static DataTable LayDSNhanVienCoMK()
        {
            string sql = "SELECT * FROM QLYNhaHang.[dbo].[Account]";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDSNhanVien()
        {
            string sql = "SELECT sID as N'Mã Nhân Viên', DisplayName as N'Tên Nhân Viên', Type as N'Chức Vụ', Salary as N'Lương'" +
                " FROM QLYNhaHang.[dbo].[Account] where Type = 'Staff'";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDSQuanly()
        {
            string sql = "SELECT sID as N'Mã Nhân Viên', DisplayName as N'Tên Nhân Viên', Type as N'Chức Vụ', Salary as N'Lương'" +
                " FROM QLYNhaHang.[dbo].[Account] where Type = 'Manager'";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDSToanBoNhanVien()
        {
            string sql = "SELECT sID as N'Mã Nhân Viên', DisplayName as N'Tên Nhân Viên', Type as N'Chức Vụ', Salary as N'Lương'" +
                " FROM QLYNhaHang.[dbo].[Account]";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static bool CapNhatLuongNhanVien(string manv,float salary)
        {
            bool kq;
            //SqlCommand command = new SqlCommand();
            string sql =string.Format("update QLYNhaHang.[dbo].[Account] set Salary= {0} where SID='{1}'", salary,manv);
            //string sql = "update QLYNhaHang.[dbo].[Account] set Salary = @salary where SID = @id";
            //command.Parameters.Add("@salary", SqlDbType.Float).Value = salary;
            //command.Parameters.Add("@id", SqlDbType.Float).Value = manv;
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool ThemNhanVien(StaffDTO nv)
        {
            bool kq;

            string sql = string.Format("insert into QLYNhaHang.[dbo].[Account] values ('{0}','{1}','{2}',N'{3}','{4}','{5}')",
            nv.StaffID,nv.UserName,nv.PassWord,nv.DisplayName,nv.Type,nv.Salary);
            //string sql = "insert into QLYNhaHang.[dbo].[Account] values (@id,@uname,@pwd,@dpln,@typ,@salary)";
            //SqlCommand command = new SqlCommand(sql);
            //if (DataProvider.getconnect(command))
            //{
            //    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = nv.StaffID;
            //    command.Parameters.Add("@uname", SqlDbType.NVarChar).Value = nv.UserName;
            //    command.Parameters.Add("@pwd", SqlDbType.NVarChar).Value = nv.PassWord;
            //    command.Parameters.Add("@dpln", SqlDbType.NVarChar).Value = nv.DisplayName;
            //    command.Parameters.Add("@typ", SqlDbType.NVarChar).Value = nv.Type;
            //    command.Parameters.Add("@salary", SqlDbType.Float).Value = nv.Salary;
            //}
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;

        }
        public static bool XoaNhanVien(string manv)
        {
            bool kq;
            string sql = string.Format("delete from QLYNhaHang.[dbo].[Account] where sID = '{0}'",manv);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static DataTable TraCuuNhanVienTheoTen(string tenNV)
        {
            string sql = string.Format("select sID as 'Mã Nhân Viên', DisplayName as 'Tên Nhân Viên',Username as 'Tên Đăng Nhập', Type as 'Chức Vụ' from QLYNhaHang.[dbo].[Account] where DisplayName like N'%{0}%'", tenNV);
            DataTable dt =DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable TraCuuNhanVienTheoMaNV(string maNV)
        {
            string sql = string.Format("select sID as 'Mã Nhân Viên', DisplayName as 'Tên Nhân Viên',Username as 'Tên Đăng Nhập', Type as 'Chức Vụ' from QLYNhaHang.[dbo].[Account] where sID='{0}'",maNV);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        //public static DataTable TraCuuTheoKhoangLuong(float salary1,float salary2)
        //{
        //    string sql = string.Format("SELECT sID as N'Mã Nhân Viên', DisplayName as N'Tên Nhân Viên', Type as N'Chức Vụ', Salary as N'Lương'" +
        //        " FROM QLYNhaHang.[dbo].[Account] where Salary >= {0} and Salary <= {1}", salary1, salary2);
        //    //DataTable dt=Datap
        //}
        public static string TraCuuManvTheoTenDN(string name)
        {
            string maNV;
            string sql = string.Format("select sID from QLYNhaHang.[dbo].[Account] where Username = '{0}'", name);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                maNV = dt.Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
            return maNV;
        }
        public static bool KiemTraTenDNTonTai(string tenDN, string MaNV)
        {
            bool kq;
            // string sql = "select * from QLYNhaHang.[dbo].[Account] where Username = '" + tenDN + "'" + "and Username <> '' and sID <> " + MaNV;
            string sql = "select * from QLYNhaHang.[dbo].[Account] where Username = '"+tenDN+"' and Username <> '' and sID <> '"+MaNV+"'";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
                kq = true;
            else
                kq = false;
            return kq;
        }
        public static bool CapNhatNhanVien(StaffDTO nv)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[Account] set Username= N'{0}' , Password= N'{1}' , DisplayName = N'{2}' , Type = '{3}' where SID= N'{4}'",nv.UserName,nv.PassWord,nv.DisplayName,nv.Type,nv.StaffID);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static string LayMatKhauTuTenDN(string tennv)
        {
            string sql = "select * from QLYNhaHang.[dbo].[Account] where Username = N'" + tennv + "'";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            string MK = dt.Rows[0][2].ToString();
            return MK;
        }
        public static int MaTuTang()
        {
            string sql = "select * from QLYNhaHang.[dbo].[Account]";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            int maTuTang = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows.Count == maTuTang)
                    return maTuTang+1;
                maTuTang++;
            }
            return maTuTang;
        }
    }
}
