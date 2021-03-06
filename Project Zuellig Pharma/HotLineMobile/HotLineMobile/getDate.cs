﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLineMobile
{
    public static class GetDate
    {
        public static List<DateTime> GetAllDatesAndInitializeTickets(DateTime startingDate, DateTime endingDate)
        {
            List<DateTime> allDates = new List<DateTime>();

            //int starting = startingDate.Day;
            //int ending = endingDate.Day;

            //for (int i = starting; i <= ending; i++)
            //{
            //    allDates.Add(new DateTime(startingDate.Year, startingDate.Month, i));
            //}

            for (DateTime date = startingDate; date <= endingDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates;
        }


        //get nhan vien co min ngay truc hotline 
        public static List<NhanVien> getMinDateTrucHotline(List<NhanVien> nvList)
        {
            List<NhanVien> a = new List<NhanVien>(); 

            //lấy ngày trực min 
            int d =0;
            foreach (var i in nvList)
            {
                if (d >= i.soNgayTrucHotline)
                {
                    d = i.soNgayTrucHotline;
                }
            }

            //lấy nhan vien có ngày trực min
            foreach (var i in nvList)
            {
                if (d == i.soNgayTrucHotline)
                {
                    a.Add(i);
                }   
            }
            
            return a;
        }


        public static void getMinDateTrucHotline2(List<NhanVien> nvList, List<NhanVien> a)
        {
            //lấy ngày trực min 
            int minDay = nvList.Min(car => car.soNgayTrucHotline);
            
            //lấy nhan vien có ngày trực min
            foreach (var i in nvList)
            {
                if (minDay == i.soNgayTrucHotline)
                {
                    a.Add(i);
                }
            }
        }

        public static void getMinDateTrucHotlineByMonth(List<NhanVien> nvList, List<NhanVien> aaa, DateTime aTime)
        {
            int month = aTime.Month;
            int ngayTrucCuaMotNhanVienTheoThang=0;
            List<int> ngayTrucList = new List<int>();
            List<NhanVien> temp = new List<NhanVien>();
            foreach (NhanVien i in nvList)
            {
                int count = 0;
                for (int j = 0; j < i.NgayTrucList.Count; j++)
                {
                    //ngayTrucCuaMotNhanVienTheoThang = (from n in i.NgayTrucList
                    //    where (i.NgayTrucList[j].Month == month)
                    //    select n).Count();
                    if (i.NgayTrucList[j].Month == month)
                    {
                        count++;
                    }
                }
                ngayTrucList.Add(count);

                temp.Add(new NhanVien
                {
                    ten = i.ten,
                    soNgayTrucHotline = count
                });
            }
            int minDay = ngayTrucList.Min();

            //lấy all nhan vien có min ngày trực trong từng tháng vào list a
            foreach (NhanVien i in temp)
            {
                if (minDay == i.soNgayTrucHotline)
                {
                    //aaa.Add(i);
                    aaa.Add(new NhanVien
                    {
                        ten = i.ten,
                        soNgayTrucHotline = i.soNgayTrucHotline,
                    });
                }
            }
        }


        public static void WriteTsv<T>(this List<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

    }
}
