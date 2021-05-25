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
   public class CheckinBUS
    {
        public static bool ThemMoiNhanVienVaoCheckIn(CheckinDTO chk)
        {
            bool kq = CheckinDAO.ThemMoiNhanVienVaoCheckIn(chk);
            return kq;
        }
        public static bool CapNhatCheckin(CheckinDTO chk)
        {
            bool kq = CheckinDAO.CapNhatCheckin(chk);
            return kq;
        }
        public static bool CapNhatCheckinDungGio(CheckinDTO chk)
        {
            bool kq = CheckinDAO.CapNhatCheckinDungGio(chk);
            return kq;
        }
        public static bool CapNhatCheckOut(CheckinDTO chk)
        {
            bool kq = CheckinDAO.CapNhatCheckOut(chk);
            return kq;
        }
        public static bool CapNhatGioLam(CheckinDTO chk)
        {
            bool kq = CheckinDAO.CapNhatGioLam(chk);
            return kq;
        }
        public static DataTable LayDSCheckin()
        {
            DataTable _ds = CheckinDAO.LayDSCheckin();
            return _ds;
        }
        public static DataTable LayDSCheckinMotNhanVien(string manv)
        {
            DataTable _ds = CheckinDAO.LayDSCheckinMotNhanVien(manv);
            return _ds;
        }
    }
}
