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
    public class CTHoaDonBUS
    {
        public static bool ThemChiTietHoaDon(CTHoaDonDTO cthd)
        {
            bool kq = CTHoaDonDAO.ThemChiTietHoaDon(cthd);
            return kq;
        }
        public static bool XoaCTHDTheoSoHD(int soHD)
        {
            bool kq = CTHoaDonDAO.XoaCTHDTheoSoHD(soHD);
            return kq;
        }
        public static bool XoaCTHDTheoSoHDVaMaTD(int soHD, int MaTD)
        {
            bool kq = CTHoaDonDAO.XoaCTHDTheoSoHDVaMaTD(soHD, MaTD);
            return kq;
        }
        public static DataTable LayDSCTHDTuMaHD(int maHD)
        {
            DataTable _ds = CTHoaDonDAO.LayDSCTHDTuMaHD(maHD);
            return _ds;
        }
        public static DataTable LayDSCTHD(int SoHD)
        {
            DataTable dt = CTHoaDonDAO.LayDSCTHD(SoHD);
            return dt;
        }
    }
}
