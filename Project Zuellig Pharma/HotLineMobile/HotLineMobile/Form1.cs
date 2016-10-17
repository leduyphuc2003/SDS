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

            //NhanVien Hai = new NhanVien {ten = "Hải",soNgayTrucHotline = 0,NgayTrucList = new List<DateTime>(), checkCN = false};
            //NhanVien Phuc = new NhanVien {ten = "Phúc", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            //NhanVien Phuong = new NhanVien {ten = "Phương", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            //NhanVien Nhat = new NhanVien {ten ="Nhật", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            //NhanVien Hoan = new NhanVien {ten = "Hoàn", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            //NhanVien Tu = new NhanVien {ten = "Tu", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            //NhanVien Binh = new NhanVien {ten = "Bình", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };

            //a.Add(Hoan);
            //a.Add(Tu);
            //a.Add(Nhat);
            //a.Add(Hai);
            //a.Add(Binh);
            //a.Add(Phuong);
            //a.Add(Phuc);

            NhanVien A = new NhanVien { ten = "A", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien B = new NhanVien { ten = "B", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien C = new NhanVien { ten = "C", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien D = new NhanVien { ten = "D", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien E = new NhanVien { ten = "E", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };
            NhanVien F = new NhanVien { ten = "F", soNgayTrucHotline = 0, NgayTrucList = new List<DateTime>(), checkCN = false };

            a.Add(A);
            a.Add(B);
            a.Add(C);
            a.Add(D);
            a.Add(E);
            a.Add(F);


            //get start date va end date
            DateTime startDateTime = dateTimePicker1.Value;
            DateTime endDateTime = dateTimePicker2.Value;

            //get all date between start date va end date
            List<DateTime> allDates = new List<DateTime>();
            allDates= GetDate.GetAllDatesAndInitializeTickets(startDateTime,endDateTime);

            #region 

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

            #endregion 


            //lặp get all T7 CN T2 
            for (int i = 0; i < allDates.Count; i++)
            {
                if ((allDates[i].DayOfWeek == DayOfWeek.Saturday) &&(allDates[i].Year != 2000))
                {
                    if (thuTuTrucCN == a.Count)
                    {
                        thuTuTrucCN = 0;
                    }

                    try
                    {
                        a[thuTuTrucCN].NgayTrucList.Add(allDates[i]);
                        a[thuTuTrucCN].soNgayTrucHotline++;
                        //allDates.Remove(allDates[i]);
                        allDates[i] = new DateTime(2000, 01, 01);
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

                    }
                    catch
                    {
                    }

                    a[thuTuTrucCN].checkCN = true;
                    thuTuTrucCN++;
                }
            }


            List<NhanVien> minDayNhanViensList = new List<NhanVien>();
            //split list allDates gồm những ngày thường ra từng tháng 
            List<List<DateTime>> result = allDates.Where(x => x.Year!=2000).GroupBy(p => p.Month)
            .Select(g => g.ToList()).Distinct()
            .ToList();
            //lặp trên 12 tháng trong result 
            foreach (List<DateTime> i in result)
            {
                foreach (DateTime ii in i)
                {
                    if (ii.Year != 2000)
                    {
                        //lưu all nhân viên có min ngay truc vào list
                        //GetDate.getMinDateTrucHotline2(a, minDayNhanViensList);
                        GetDate.getMinDateTrucHotlineByMonth(a, minDayNhanViensList, ii);
                        NhanVien minNhanVien = new NhanVien();

                        //Random rnd = new Random();
                        //int sttNhanVien = rnd.Next(0, minDayNhanViensList.Count-1);
                        //minNhanVien = minDayNhanViensList[sttNhanVien];
                        minNhanVien = minDayNhanViensList.First();
                        minDayNhanViensList.Clear();

                        minNhanVien.soNgayTrucHotline++;
                        minNhanVien.NgayTrucList.Add(ii);

                        a.Where(d => d.ten == minNhanVien.ten).First().soNgayTrucHotline = minNhanVien.soNgayTrucHotline;
                        //a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(i);

                        bool alreadyExist = a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Contains(ii);
                        if (alreadyExist == false)
                        {
                            a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(ii);
                        }
                    }
                }
            }

            #region xử lí cho ngày thường lặp trên allDates

            //xử lí cho ngày thường lặp trên allDates
            //List<NhanVien> minDayNhanViensList = new List<NhanVien>();
            //foreach (DateTime i in allDates)
            //{
            //    if (i.Year != 2000)
            //    {
            //        //lưu all nhân viên có min ngay truc vào list
            //        GetDate.getMinDateTrucHotline2(a, minDayNhanViensList);
            //        NhanVien minNhanVien = new NhanVien();

            //        //Random rnd = new Random();
            //        //int sttNhanVien = rnd.Next(0, minDayNhanViensList.Count-1);
            //        //minNhanVien = minDayNhanViensList[sttNhanVien];
            //        minNhanVien = minDayNhanViensList.First();
            //        minDayNhanViensList.Clear();

            //        minNhanVien.soNgayTrucHotline++;
            //        minNhanVien.NgayTrucList.Add(i);

            //        a.Where(d => d.ten == minNhanVien.ten).First().soNgayTrucHotline = minNhanVien.soNgayTrucHotline;
            //        //a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(i);

            //        bool alreadyExist = a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Contains(i);
            //        if (alreadyExist == false)
            //        {
            //            a.Where(d => d.ten == minNhanVien.ten).First().NgayTrucList.Add(i);
            //        }
            //    }
            //}

            #endregion



            //lưu kết quả xuất ra file 
            List<String> kq = new List<string>(a.Count);
            for (int i = 0; i < a.Count; i++)
            {
                String s = a[i].ten + "\n";

                List<DateTime> list = a[i].NgayTrucList.OrderBy(x => x.Date).ToList();
                foreach (DateTime ii in list)
                {
                    s = s + ii.ToString("dd/MM/yyyy") + "\n";
                }

                //foreach (DateTime ii in a[i].NgayTrucList)
                //{
                //    s = s + ii.ToString("dd/MM/yyyy") + "\n";
                //}
                kq.Add(s);
                //s = "";
            }

            String ketqua = "";
            foreach (String i in kq)
            {
                ketqua += i;
            }


            //---------------------------------------------------
            //lưu all kết quả nhân viên gồm tên và ngày trực vào list2
            List<NhanVienNgayTruc> nhanVienNgayTrucs = new List<NhanVienNgayTruc>();
            foreach (NhanVien i in a)
            {
                foreach (DateTime ii in i.NgayTrucList)
                {
                    nhanVienNgayTrucs.Add(new NhanVienNgayTruc
                    {
                       ten = i.ten,
                       ngayTruc = ii
                    });
                }
            }
            //xắp xếp theo ngày tăng dần
            List<NhanVienNgayTruc> nhanVienNgayTrucs2 = nhanVienNgayTrucs.OrderBy(o => o.ngayTruc).ToList();
            //var lowerBoundMonth8 = 8;
            //var query = from p in nhanVienNgayTrucs2
            //            where(p.ngayTruc.Month == lowerBoundMonth8)  
            //            select p;
            //string ss = "";
            //foreach (NhanVienNgayTruc i in query)
            //{
            //    ss = ss + i.ngayTruc.ToLongDateString() + "--->" + i.ten.ToString() + " " + "\n";
            //}

            String t1= "January \n", t2= "February \n", t3= "March \n", t4= "April \n", t5= "May \n", t6= "June \n", t7= "July \n", t8= "August \n", t9= "September \n", t10= "October \n", t11= "November \n", t12= "December \n";
            for (var i = 1; i < 13; i++)
            {
                //var lowerBoundMonth8 = 8;
                var query = from p in nhanVienNgayTrucs2
                            where (p.ngayTruc.Month == i)
                            select p;
                switch (i)
                {
                    case 1:
                    {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t1 = t1 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t1 += "================================================ \n";
                            break;
                    }
                    case 2:
                    {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t2 = t2 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t2 += "================================================ \n";
                            break;
                    }
                    case 3:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t3 = t3 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t3 += "================================================ \n";
                            break;
                        }
                    case 4:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t4 = t4 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t4 += "================================================ \n";
                            break;
                        }
                    case 5:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t5 = t5 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t5 += "================================================ \n";
                            break;
                        }
                    case 6:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t6 = t6 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t6 += "================================================ \n";
                            break;
                        }
                    case 7:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t7 = t7 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t7 += "================================================ \n";
                            break;
                        }
                    case 8:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t8 = t8 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t8 += "================================================ \n";
                            break;
                        }
                    case 9:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t9 = t9 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t9 += "================================================ \n";
                            break;
                        }
                    case 10:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t10 = t10 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t10 += "================================================ \n";
                            break;
                        }
                    case 11:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t11 = t11 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t11 += "================================================ \n";
                            break;
                        }
                    case 12:
                        {
                            foreach (NhanVienNgayTruc ii in query)
                            {
                                t12 = t12 + ii.ngayTruc.ToLongDateString() + "--->" + ii.ten.ToString() + " " + "\n";
                            }
                            t12 += "================================================ \n";
                            break;
                        }
                }   
            }
            //--------------------------------------------------------------


            //xuat report
            using (TextWriter writer = File.CreateText("D:\\hotline.txt"))
            {
                //
                // Write one line.
                //
                writer.WriteLine("Lịch Trực hotline");
                //
                // Write two strings.
                //
                //writer.Write(ketqua);
                writer.Write(t1);
                writer.Write(t2);
                writer.Write(t3);
                writer.Write(t4);
                writer.Write(t5);
                writer.Write(t6);
                writer.Write(t7);
                writer.Write(t8);
                writer.Write(t9);
                writer.Write(t10);
                writer.Write(t11);
                writer.Write(t12);
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
