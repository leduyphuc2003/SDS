using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLineMobile
{
    public class NhanVien
    {
        public string ten;
        public int soNgayTrucHotline;
        public List<DateTime> NgayTrucList;
        public bool checkCN;

        public NhanVien()
        {
            ten = "";
            soNgayTrucHotline = 0;
            NgayTrucList = new List<DateTime>();
            checkCN = false;
        }
    }
}
