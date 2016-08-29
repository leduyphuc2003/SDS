using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotLineMobile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<NhanVien> a = new List<NhanVien>();
            int thuTuTrucCN = 0;
                
            NhanVien Hai = new NhanVien {ten = "Hải",soNgayTrucHotline = 0,NgayTrucList = new List<DateTime>(), checkCN = false};
            NhanVien Phuc = new NhanVien {ten = "Phúc", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien Phuong = new NhanVien {ten = "Phương", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien Nhat = new NhanVien {ten ="Nhật", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien Hoan = new NhanVien {ten = "Hoàn", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien Tu = new NhanVien {ten = "Tu", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien Binh = new NhanVien {ten = "Bình", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };

            a.Add(Hai);
            a.Add(Phuc);
            a.Add(Phuong);
            a.Add(Nhat);
            a.Add(Hoan);
            a.Add(Tu);
            a.Add(Binh);

            //get start date va end date
            DateTime startDateTime = dateTimePicker1.Value;
            DateTime endDateTime = dateTimePicker2.Value;

            //get all date between start date va end date
            List<DateTime> allDates = new List<DateTime>();
            allDates= GetDate.GetAllDatesAndInitializeTickets(startDateTime,endDateTime);

            

            //for (int i = 0; i < allDates.Count-1; i++)
            //{
            //    if (allDates[i].DayOfWeek == DayOfWeek.Saturday)
            //    {
            //        if (thuTuTrucCN == a.Count)
            //        {
            //            thuTuTrucCN = 0;
            //        }

            //        a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
            //        a[thuTuTrucCN].soNgayTrucHotline++;
            //        i++;

            //        a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
            //        a[thuTuTrucCN].soNgayTrucHotline++;
            //        i++;

            //        a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
            //        a[thuTuTrucCN].soNgayTrucHotline++;
            //        a[thuTuTrucCN].checkCN = true;
            //        thuTuTrucCN++;
            //    }
            //    else
            //    {
            //        //minDayNhanViensList = new List<NhanVien>(GetDate.getMinDateTrucHotline(a));
            //        //minDayNhanViensList.AddRange(GetDate.getMinDateTrucHotline(a));

            //        //lưu all nhân viên có min ngay truc vào list
            //        List<NhanVien> minDayNhanViensList = new List<NhanVien>();

            //        GetDate.getMinDateTrucHotline2(a, minDayNhanViensList);
            //        NhanVien minNhanVien = new NhanVien();

            //        minNhanVien = minDayNhanViensList.First();
            //        minDayNhanViensList.Clear();

            //        minNhanVien.soNgayTrucHotline++;
            //        minNhanVien.NgayTrucList.Add(allDates[i]);

            //        a.Where(d => d.ten == minNhanVien.ten).First().soNgayTrucHotline = minNhanVien.soNgayTrucHotline;
            //        a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(allDates[i]);

            //    }
            //}

            //lặp get all T7 CN T2 
            for (int i = 0; i < allDates.Count - 1; i++)
            {
                if ((allDates[i].DayOfWeek == DayOfWeek.Saturday) &&(allDates[i].Year != 2000))
                {
                    if (thuTuTrucCN == a.Count)
                    {
                        thuTuTrucCN = 0;
                    }

                    a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
                    a[thuTuTrucCN].soNgayTrucHotline++;
                    //allDates.Remove(allDates[i]);
                    allDates[i] = new DateTime(2000,01,01);
                    i++;
                    

                    a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
                    a[thuTuTrucCN].soNgayTrucHotline++;
                    //allDates.Remove(allDates[i]);
                    allDates[i] = new DateTime(2000, 01, 01);
                    i++;
                    

                    a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
                    a[thuTuTrucCN].soNgayTrucHotline++;
                    //allDates.Remove(allDates[i]);
                    allDates[i] = new DateTime(2000, 01, 01);

                    a[thuTuTrucCN].checkCN = true;
                    thuTuTrucCN++;
                }
            }


            List<NhanVien> minDayNhanViensList = new List<NhanVien>();
            foreach (DateTime i in allDates)
            {
                if (i.Year != 2000)
                {
                    //lưu all nhân viên có min ngay truc vào list
                    GetDate.getMinDateTrucHotline2(a, minDayNhanViensList);
                    NhanVien minNhanVien = new NhanVien();

                    minNhanVien = minDayNhanViensList.First();
                    minDayNhanViensList.Clear();

                    minNhanVien.soNgayTrucHotline++;
                    minNhanVien.NgayTrucList.Add(i);

                    a.Where(d => d.ten == minNhanVien.ten).First().soNgayTrucHotline = minNhanVien.soNgayTrucHotline;
                    //a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(i);

                    bool alreadyExist = a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Contains(i);
                    if (alreadyExist == false)
                    {
                        a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(i);
                    }
                }


            }

            List<String> kq = new List<string>(a.Count);
            for (int i = 0; i < a.Count; i++)
            {
                String s = a[i].ten + "\n";
                foreach (DateTime ii in a[i].NgayTrucList)
                {
                    s = s + ii.ToString() + "\n";
                }
                kq.Add(s);
                s = "";
            }

            String ketqua = "";
            foreach (String i in kq)
            {
                ketqua += i;
            }



            using (TextWriter writer = File.CreateText("D:\\hotline.txt"))
            {
                //
                // Write one line.
                //
                writer.WriteLine("First line");
                //
                // Write two strings.
                //
                writer.Write(ketqua);
                //writer.Write("B ");
                //
                // Write the default newline.
                //
                writer.Write(writer.NewLine);
            }

            lblKetQua.Text = "Vui lòng check kết quả file hotline.txt trong ổ D: ";
        }
    }
}
