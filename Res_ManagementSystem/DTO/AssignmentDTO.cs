using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DTO
{
   public class AssignmentDTO
    {
        private int _ca1;
        private int _ca2;
        private int _ca3;
        private string _sID;
        private float _salary;
        private float _tienphat;
        private float _tienthuong;
        private int _sogiotre;
        private int _catre;
        private int _tinhtrangck;
        // Phương thức khởi tạo mặc định
        public AssignmentDTO()
        {
            _sID = "";
            _ca1 = 0;
            _ca2 = 0;
            _ca3 = 0;
            _salary = 0;
            _tienphat = 0;
            _tienthuong = 0;
            _sogiotre = 0;
            _catre = 0;
            _tinhtrangck = 0;
        }
        //Phương thức khởi tạo có tham số
        public AssignmentDTO(string sID,int ca1,int ca2,int ca3,float salary,float tienphat,float tienthuong,int sogiotre,int catre,int tinhtrangck)
        {
            _sID = sID;
            _ca1 = ca1;
            _ca2 = ca2;
            _ca3 = ca3;
            _salary = salary;
            _tienphat = tienphat;
            _tienthuong = tienthuong;
            _sogiotre = sogiotre;
            _catre = catre;
            _tinhtrangck = tinhtrangck;
        }
        // phương thức khởi tạo sao chép
        public AssignmentDTO(AssignmentDTO Agm)
        {
            _sID = Agm._sID;
            _ca1 = Agm._ca1;
            _ca2 = Agm._ca2;
            _ca3 = Agm._ca3;
            _salary = Agm._salary;
            _tienphat = Agm._tienphat;
            _tienthuong = Agm._tienthuong;
            _sogiotre = Agm._sogiotre;
            _catre = Agm._catre;
            _tinhtrangck = Agm._tinhtrangck;
        }
        // Properties
        public string SID
        {
            get { return _sID; }
            set { _sID = value; }
        }
        public int Ca1
        {
            get { return _ca1; }
            set { _ca1 = value; }
        }
        public int Ca2
        {
            get { return _ca2; }
            set { _ca2 = value; }
        }
        public int Ca3
        {
            get { return _ca3; }
            set { _ca3 = value; }
        }
        public float Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }
        public float TienPhat
        {
            get { return _tienphat; }
            set { _tienphat = value; }
        }
        public float TienThuong
        {
            get { return _tienthuong; }
            set { _tienthuong = value; }
        }
        public int CaTre
        {
            get { return _catre; }
            set { _catre = value; }
        }
        public int SoGioCham
        {
            get { return _sogiotre; }
            set { _sogiotre = value; }
        }
        public int TinhTrangCK
        {
            get { return _tinhtrangck; }
            set { _tinhtrangck = value; }
        }
    }
}
