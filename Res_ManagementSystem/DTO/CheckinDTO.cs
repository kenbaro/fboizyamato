using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DTO
{
    public class CheckinDTO
    {
        private string _manv;
        private int _checkin1;
        private int _checkin2;
        private int _checkin3;
        private int _checkout1;
        private int _checkout2;
        private int _checkout3;
        private int _giolam;
        private int _checkindg;

        public CheckinDTO()
        {
            _manv = "";
            _checkin1 = 0;
            _checkin1 = 0;
            _checkin1 = 0;
            _checkout1 = 0;
            _checkout2 = 0;
            _checkout3 = 0;
            _giolam = 0;
            _checkindg = 0;
        }
        public CheckinDTO(string manv,int checkin1,int checkin2,int checkin3,int checkout1,int checkout2,int checkout3,int giolam,int checkindg)
        {
            _manv = manv;
            _checkin1 = checkin1;
            _checkin1 = checkin2;
            _checkin1 = checkin3;
            _checkout1 = checkout1;
            _checkout2 = checkout2;
            _checkout3 = checkout3;
            _giolam = giolam;
            _checkindg = checkindg;
        }
        public CheckinDTO(CheckinDTO chk)
        {
            _manv = chk._manv;
            _checkin1 = chk._checkin1;
            _checkin1 = chk._checkin2;
            _checkin1 = chk._checkin3;
            _checkout1 = chk._checkout1;
            _checkout2 = chk._checkout2;
            _checkout3 = chk._checkout3;
            _giolam = chk._giolam;
            _checkindg = chk._checkindg;
        }
        public string MaNV
        {
            get { return _manv; }
            set { _manv = value; }
        }
        public int Checkin1
        {
            get { return _checkin1; }
            set { _checkin1 = value; }
        }
        public int Checkin2
        {
            get { return _checkin2; }
            set { _checkin2 = value; }
        }
        public int Checkin3
        {
            get { return _checkin3; }
            set { _checkin3 = value; }
        }
        public int Checkout1
        {
            get { return _checkout1; }
            set { _checkout1 = value; }
        }
        public int Checkout2
        {
            get { return _checkout2; }
            set { _checkout2 = value; }
        }
        public int Checkout3
        {
            get { return _checkout3; }
            set { _checkout3 = value; }
        }
        public int GioLam
        {
            get { return _giolam; }
            set { _giolam = value; }
        }
        public int CheckinDungGio
        {
            get { return _checkindg; }
            set { _checkindg = value; }
        }
    }
}
