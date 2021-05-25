using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Res_ManagementSystem.DTO
{
    public class StaffDTO
    {
       
        private string _staffID;
        private string _username;
        private string _password;
        private string _dplayname;
        private string _type;
        private float _salary;

        // Phương thức khởi tạo mặc định
        public StaffDTO()
        {
           
            _staffID = "";
            _username = "";
            _password = "";
            _dplayname = "";
            _type = "";
            _salary = 0;
        }
        // phương thức khởi tạo có tham số
        public StaffDTO(string staffid,string user,string passw,string dplayname,string type,float salary)
        {
            
            _staffID = staffid;
            _username = user;
            _password = passw;
            _dplayname = dplayname;
            _type = type;
            _salary = salary;
        }
        // phương thức khởi tạo sao chép
        public StaffDTO(StaffDTO stfDTO)
        {
           
            _staffID = stfDTO._staffID;
            _username = stfDTO._username;
            _password = stfDTO._password;
            _dplayname = stfDTO._dplayname;
            _type = stfDTO._type;
            _salary = stfDTO._salary;
        }

        // Properties
        public string StaffID
        {
            get { return _staffID; }
            set { _staffID = value; }
        }
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        public string PassWord
        {
            get { return _password; }
            set { _password = value; }
        } 
        public string DisplayName
        {
            get { return _dplayname; }
            set { _dplayname = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public float Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }


    }
}
