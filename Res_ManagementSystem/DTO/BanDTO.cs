using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DTO
{ 
    public class BanDTO
    {
        private int _maBan;
        private int _soGhe;
        private string _tinhTrang;

            //Phương thức khởi tạo mặc định
        public BanDTO()
        {
            _maBan = 0;
            _soGhe = 0;
            _tinhTrang = "";

        }

            //Phương thức khởi tạo có tham số
        public BanDTO(int maBan, int soGhe,string tinhTrang)
        {
            _maBan = maBan;
            _soGhe = soGhe;
            _tinhTrang = tinhTrang;
        }

            //Phương thức khởi tạo sao chép.
        public BanDTO(BanDTO HoaDon)
        {
            _maBan = HoaDon._maBan;
             _soGhe = HoaDon._soGhe;
            _tinhTrang = HoaDon._tinhTrang;
        }

            //Properties
        public int MaBan
        {
            get { return _maBan; }
            set { _maBan = value; }
        }
        public int SoGhe
        {
            get { return _soGhe; }
            set { _soGhe = value; }
        }
        public string TinhTrang
        {
            get { return _tinhTrang; }
            set { _tinhTrang = value; }

        }
    }
}
