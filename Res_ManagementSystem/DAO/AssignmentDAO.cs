using Res_ManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DAO
{
    public class AssignmentDAO
    {
        //public static object SqlDataAccessHelper { get; private set; }

        public static DataTable LayDsChiaca()
        {
            string sql = "SELECT sID as N'Mã Nhân Viên', Ca1,Ca2,Ca3, LuongNgay as N'Lương Ngày',SoGioCham,CaTre,TienPhat,TienThuong,TinhtrangCheckin FROM QLYNhaHang.[dbo].[ChiaCa] ORDER BY sID";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDsChiacaTuMaNV(string manv)
        {
            string sql = "SELECT sID as N'Mã Nhân Viên', Ca1,Ca2,Ca3, LuongNgay as N'Lương Ngày',SoGioCham,CaTre,TienPhat,TienThuong,TinhtrangCheckin FROM QLYNhaHang.[dbo].[ChiaCa] where sID='"+ manv+"'";
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayDsNhanVienTrongMotCa(int ca)
        {
            // string sql = "SELECT sID as N'Mã Nhân Viên', Ca1,Ca2,Ca3, LuongNgay as N'Lương Ngày',SoGioCham,CaTre,TienPhat,TienThuong,TinhtrangCheckin FROM QLYNhaHang.[dbo].[ChiaCa] where sID='" + manv + "'";
            string sql = string.Format("select sID and from QLYNhaHang.[dbo].[ChiaCa] where Ca{0}=1 and TinhtrangCheckin=1", ca);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static bool ThemPhanCong(AssignmentDTO agm)
        {
            bool kq;
            string sql = string.Format("insert into QLYNhaHang.[dbo].[ChiaCa] values('{0}', '{1}', '{2}','{3}','{4}',{5},{6},{7},{8},{9})", agm.SID,agm.Ca1,agm.Ca2,agm.Ca3,agm.Salary,agm.SoGioCham,agm.TienPhat,agm.TienThuong,agm.CaTre,agm.TinhTrangCK);
            kq = Res_ManagementSystem.DAO.DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool XoaPhanCong(string manv)
        {
            bool kq;
            string sql = string.Format("delete from QLYNhaHang.[dbo].[ChiaCa] where SID = '{0}'",
                                        manv);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static DataTable NVLamTrongCa(string ca,string manv)
        {
            
            string sql = string.Format("select sID,Ca{0} from QLYNhaHang.[dbo].[ChiaCa] where sID = '{1}' and Ca{2} = 1 ",ca,manv,ca);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static bool CapNhatCheckin(string manv)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[ChiaCa] set TinhtrangCheckin=1 where sID = '{0}'", manv);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        //public static DataTable TimCaTheoMaNV(string manv)
        //{

        //    string sql = string.Format("select * from QLYNhaHang.[dbo].[ChiaCa] where sID = '{0}'",manv);
        //    DataTable dt = DataProvider.ExecuteQuery(sql);
        //    return dt;
        //}
        public static bool ThemLuongTheoNgay(float salary,float psh_salary,string manv)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[ChiaCa] set LuongNgay= {0},TienPhat={1} where sID = '{2}'", salary,psh_salary,manv);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static DataTable KiemTraCacNVDTre(int catre)
        {  
            string sql = string.Format("select * from QLYNhaHang.[dbo].[ChiaCa] where SoGioCham <> 0 and TinhTrangCheckin=1 and CaTre={0}",catre);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static DataTable LayLuongNhanVienCungCa(int ca)
        {

            string sql = string.Format("select * from QLYNhaHang.[dbo].[ChiaCa] where Ca{0}=1 and CaTre<> {1} and TinhTrangCheckin=1", ca,ca);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static int LayCaTheoGio(int gio)
        {
            int ca=0;
            if (gio >= 7 && gio < 11)
                ca = 1;
            else if (gio >= 11 && gio < 15)
                    ca = 2;
            else if(gio >= 18&& gio<22)
                    ca = 3;
            return ca;
        }
        public static float TongTienPhatTrong1Ca(int catre)
        {
            float sum = 0;
            string sql = string.Format("select SUM(TienPhat) as 'TongTienPhat' from QLYNhaHang.[dbo].[ChiaCa] where CaTre={0}", catre);
            DataTable _ds = DataProvider.ExecuteQuery(sql);
            if(_ds.Rows.Count>0)
            {
                sum= float.Parse(_ds.Rows[0][0].ToString());
            }
            return sum;
        }

        public static bool ThemTienThuong(float exsalary,int ca)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[ChiaCa] set TienThuong={0} where CaTre<>{1} and Ca{2}=1 and TinhtrangCheckin=1",exsalary,ca,ca);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool ThemGioTreCaTre(string manv,int sogiocham,int ca)
        {
            bool kq;
            string sql = string.Format("update QLYNhaHang.[dbo].[ChiaCa] set SoGioCham = {0},CaTre={1} where sID = '{2}' ",sogiocham,ca, manv);
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static bool XoaToanBoDSPhanCong()
        {
            bool kq;
            string sql = string.Format("delete from QLYNhaHang.[dbo].[ChiaCa] where Ca1=0 or Ca1=1");
            kq = DataProvider.ExecuteNonQuery(sql);
            return kq;
        }
        public static DataTable KiemTraMaNVRong(string maNV)
        {
            string sql = string.Format("select * from QLYNhaHang.[dbo].[ChiaCa] where sID= {0}", maNV);
            DataTable dt = DataProvider.ExecuteQuery(sql);
            return dt;
        }
        public static bool CapNhatDoiCa(string manv1,string manv2,int ca)
        {
            bool kq=false;
            string sql1 = string.Format("update QLYNhaHang.[dbo].[ChiaCa] set Ca{0}=1 where sID={1}",ca,manv1);
            string sql2 = string.Format("update QLYNhaHang.[dbo].[ChiaCa] set Ca{0}=1 where sID={1}", ca, manv2);
            if (DataProvider.ExecuteNonQuery(sql1) && DataProvider.ExecuteNonQuery(sql2))
                kq = true;
            return kq;
        }

    }
}
