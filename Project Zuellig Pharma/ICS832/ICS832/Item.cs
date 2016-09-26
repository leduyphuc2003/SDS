using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS832
{
    public class Item
    {
        public string ITEM;
        public string WHS;
        public string LOCN;
        public string ISL;
        public int IAQA;

        public static void Mass_Update_Allocate_Scheduled_Picked_To_0(string item)
        {
            List<String> listSQL = new List<string>();
            //string s1 = string.Format("UPDATE LB SET LBSOA = 0 WHERE ITEM = '{0}' AND LBSOA<> 0",item );
            
            listSQL.Add(string.Format("UPDATE LB SET LBSOA = 0 WHERE ITEM = '{0}' AND LBSOA<> 0", item));
            listSQL.Add(string.Format("UPDATE IL SET ILSOA = 0 WHERE ITEM = '{0}' AND ILSOA<> 0", item));
            listSQL.Add(string.Format("UPDATE ISL SET ISLSOA = 0 WHERE ITEM = '{0}' AND ISLSOA<> 0", item));
            listSQL.Add(string.Format("UPDATE IW SET IWSOA = 0 WHERE ITEM = '{0}' AND IWSOA<> 0", item));
            listSQL.Add(string.Format("UPDATE IM SET IMSOA = 0 WHERE ITEM = '{0}' AND IMSOA<> 0", item));

            listSQL.Add(string.Format("UPDATE LB SET LBSPK = 0 WHERE ITEM = '{0}' AND LBSPK<> 0", item));
            listSQL.Add(string.Format("UPDATE IL SET ILSPK = 0 WHERE ITEM = '{0}' AND ILSPK<> 0", item));
            listSQL.Add(string.Format("UPDATE ISL SET ISLSPK = 0 WHERE ITEM = '{0}' AND ISLSPK<> 0", item));
            listSQL.Add(string.Format("UPDATE IW SET IWSPK = 0 WHERE ITEM = '{0}' AND IWSPK<> 0", item));
            listSQL.Add(string.Format("UPDATE IM SET IMSPK = 0 WHERE ITEM = '{0}' AND IMSPK<> 0", item));

            listSQL.Add(string.Format("UPDATE IW SET IWSSA = 0  WHERE ITEM = '{0}' AND IWSSA<> 0", item));
            listSQL.Add(string.Format("UPDATE IM SET IMSSA = 0  WHERE ITEM = '{0}' AND IMSSA<> 0", item));

            As400ConnectionUtility var = new As400ConnectionUtility();

            foreach (String i in listSQL)
            {
                var.ExecuteQuery(i);
            }
        }

        public static void Mass_Update_0_File_LB(string item)
        {
            List<Item> lsItems = new List<Item>();
            string sql = string.Format("select ITEM, LOCN, ISL#, WHS, LBSOA, LBSPK from LB where item='{0}' and (LBSOA<>0 or LBSPK<>0) and LBDEL='' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();

            DataTable tb = var.GetDataByQuery(sql);

            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    lsItems.Add(new Item
                    {
                        ITEM = dr["ITEM"].ToString().Trim(),
                        WHS = dr["WHS"].ToString().Trim(),
                        LOCN = dr["LOCN"].ToString().Trim(),
                        ISL = dr["ISL#"].ToString().Trim()
                    });
                }

                foreach (Item i in lsItems)
                {
                    string update_allocate_picked_0 = string.Format("UPDATE LB SET LBSOA = 0,LBSPK=0 WHERE ITEM='{0}' AND LOCN='{1}' AND WHS='{2}' AND ISL#='{3}'", i.ITEM, i.LOCN, i.WHS, i.ISL);
                    var.ExecuteQuery(update_allocate_picked_0);
                }
            }
            catch
            {
            }
        }

        public static void Mass_Update_0_File_IL(string item)
        {
            List<Item> lsItems = new List<Item>();
            string sql = string.Format("select item,locn,whs,ilsoa,ilspk from IL where item='{0}' and (ILSOA<>0 or ILSPK<>0) and ILDEL='' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();

            DataTable tb = var.GetDataByQuery(sql);

            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    lsItems.Add(new Item
                    {
                        ITEM = dr["ITEM"].ToString().Trim(),
                        WHS = dr["WHS"].ToString().Trim(),
                        LOCN = dr["LOCN"].ToString().Trim()
                    });
                }

                foreach (Item i in lsItems)
                {
                    string update_allocate_picked_0 = string.Format("UPDATE IL SET ILSOA = 0,ILSPK=0 WHERE ITEM='{0}' AND LOCN='{1}' AND WHS='{2}'", i.ITEM, i.LOCN, i.WHS);
                    var.ExecuteQuery(update_allocate_picked_0);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void Mass_Update_0_File_ISL(string item)
        {
            List<Item> lsItems = new List<Item>();
            string sql = string.Format("select item,ISL#,ISLsoa,ISLspk from ISL where item='{0}' and (ISLSOA<>0 or ISLSPK<>0) and ISLDEL='' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();

            DataTable tb = var.GetDataByQuery(sql);

            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    lsItems.Add(new Item
                    {
                        ITEM = dr["ITEM"].ToString().Trim(),
                        ISL = dr["ISL#"].ToString().Trim()
                    });
                }

                foreach (Item i in lsItems)
                {
                    string update_allocate_picked_0 = string.Format("UPDATE ISL SET ISLSOA = 0,ISLSPK=0 WHERE ITEM='{0}' AND ISL#='{1}' ", i.ITEM, i.ISL);
                    var.ExecuteQuery(update_allocate_picked_0);
                }
            }
            catch 
            {
            }
        }

        public static void Mass_Update_0_File_IW(string item)
        {
            List<Item> lsItems = new List<Item>();
            string sql = string.Format("select item,WHS,iwsoa,iwspk from IW where item='{0}' and (iwsoa<>0 or iwspk<>0) and IWDEL='' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();

            DataTable tb = var.GetDataByQuery(sql);

            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    lsItems.Add(new Item
                    {
                        ITEM = dr["ITEM"].ToString().Trim(),
                        WHS = dr["WHS"].ToString().Trim()
                    });
                }

                foreach (Item i in lsItems)
                {
                    string update_allocate_picked_0 = string.Format("UPDATE IW SET iwsoa = 0,iwspk=0 WHERE ITEM='{0}' AND WHS='{1}' ", i.ITEM, i.WHS);
                    var.ExecuteQuery(update_allocate_picked_0);
                }
            }
            catch 
            {
            }
        }

        public static void Mass_Update_0_File_IM(string item)
        {
            List<Item> lsItems = new List<Item>();
            string sql = string.Format("select item,imsoa,imspk from IM where item='{0}' and (imsoa<>0 or imspk<>0) and IMDEL='' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();

            DataTable tb = var.GetDataByQuery(sql);

            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    lsItems.Add(new Item
                    {
                        ITEM = dr["ITEM"].ToString().Trim()
                    });
                }

                foreach (Item i in lsItems)
                {
                    string update_allocate_picked_0 = string.Format("UPDATE IM SET imsoa = 0,imspk=0 WHERE ITEM='{0}'", i.ITEM);
                    var.ExecuteQuery(update_allocate_picked_0);
                }
            }
            catch 
            {
            }
        }

        public static void Mass_Update_0_scheduled_File_IW(string item)
        {
            List<Item> lsItems = new List<Item>();
            string sql = string.Format("select ITEM, IWSSA, WHS from IW where item='{0}' and IWSSA<>0 and IWDEL='' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();

            DataTable tb = var.GetDataByQuery(sql);

            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    lsItems.Add(new Item
                    {
                        ITEM = dr["ITEM"].ToString().Trim(),
                        WHS = dr["WHS"].ToString().Trim()
                    });
                }

                foreach (Item i in lsItems)
                {
                    string update_scheduled_0 = string.Format("UPDATE IW SET IWSSA=0 WHERE ITEM='{0}' AND WHS='{1}'", i.ITEM, i.WHS);
                    var.ExecuteQuery(update_scheduled_0);
                }
            }
            catch
            {
            }

        }

        public static void Mass_Update_0_scheduled_File_IM(string item)
        {
            string update_scheduled_0 = string.Format("UPDATE IM SET IMSSA=0 WHERE ITEM='{0}' ", item);
            As400ConnectionUtility var = new As400ConnectionUtility();
            var.ExecuteQuery(update_scheduled_0);
        }

        public static void ShowSumAllocate(string a, ref int lbsoa, ref int ilsoa, ref int islsoa, ref int iwsoa, ref int imsoa)
        {
            string sumLB = string.Format("select sum(LBSOA) from LB where ITEM='{0}'", a);
            string sumIL = string.Format("select sum(ILSOA) from IL where ITEM='{0}'", a);
            string sumISL = string.Format("select sum(ISLSOA) from ISL where ITEM='{0}'", a);
            string sumIW = string.Format("select sum(IWSOA) from IW where ITEM='{0}'", a);
            string sumIM = string.Format("select sum(IMSOA) from IM where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();

            var tb1 = var.GetDataByQuery(sumLB).Rows[0];
            string x1 = tb1[0].ToString();
            lbsoa = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIL).Rows[0];
            x1 = tb1[0].ToString();
            ilsoa = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumISL).Rows[0];
            x1 = tb1[0].ToString();
            islsoa = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIW).Rows[0];
            x1 = tb1[0].ToString();
            iwsoa = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIM).Rows[0];
            x1 = tb1[0].ToString();
            imsoa = Convert.ToInt32(x1);


            //lbsoa = Int32.Parse(var.GetDataByQuery(sumLB).ToString());
            //ilsoa = Int32.Parse(var.GetDataByQuery(sumIL).ToString());
            //islsoa = Int32.Parse(var.GetDataByQuery(sumISL).ToString());
            //iwsoa = Int32.Parse(var.GetDataByQuery(sumIW).ToString());
            //imsoa = Int32.Parse(var.GetDataByQuery(sumIM).ToString());
        }

        public static void ShowSumPicked(string a, ref int lbspk, ref int ilspk, ref int islspk, ref int iwsoa, ref int imsoa)
        {
            string sumLB = string.Format("select sum(LBSPK) from LB where ITEM='{0}'", a);
            string sumIL = string.Format("select sum(ILSPK) from IL where ITEM='{0}'", a);
            string sumISL = string.Format("select sum(ISLSPK) from ISL where ITEM='{0}'", a);
            string sumIW = string.Format("select sum(IWSPK) from IW where ITEM='{0}'", a);
            string sumIM = string.Format("select sum(IMSPK) from IM where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();

            var tb1 = var.GetDataByQuery(sumLB).Rows[0];
            string x1 = tb1[0].ToString();
            lbspk = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIL).Rows[0];
            x1 = tb1[0].ToString();
            ilspk = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumISL).Rows[0];
            x1 = tb1[0].ToString();
            islspk = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIW).Rows[0];
            x1 = tb1[0].ToString();
            iwsoa = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIM).Rows[0];
            x1 = tb1[0].ToString();
            imsoa = Convert.ToInt32(x1);

            //lbspk = Int32.Parse(var.GetDataByQuery(sumLB).ToString());
            //ilspk = Int32.Parse(var.GetDataByQuery(sumIL).ToString());
            //islspk = Int32.Parse(var.GetDataByQuery(sumISL).ToString());
            //iwsoa = Int32.Parse(var.GetDataByQuery(sumIW).ToString());
            //imsoa = Int32.Parse(var.GetDataByQuery(sumIM).ToString());
        }

        public static void ShowScheduled(string a, ref int iwssa, ref int imssa)
        {
            string sumIM = string.Format("select sum (IMSSA) from IM where ITEM='{0}'", a);
            string sumIW = string.Format("select sum (IWSSA) from IW where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();

            var tb1 = var.GetDataByQuery(sumIM).Rows[0];
            string x1 = tb1[0].ToString();
            imssa = Convert.ToInt32(x1);

            tb1 = var.GetDataByQuery(sumIW).Rows[0];
            x1 = tb1[0].ToString();
            iwssa = Convert.ToInt32(x1);

            //iwssa = Int32.Parse(var.GetDataByQuery(sumIW).ToString());
            //imssa = Int32.Parse(var.GetDataByQuery(sumIM).ToString());
        }

        //sum va check allocate 5 bang: trả về true nếu 5 bảng bằng nhau, còn lại là false 
        public static bool checkSumAllocate(string a)
        {
            string sumLB = string.Format("select sum(LBSOA) from LB where ITEM='{0}'", a);
            string sumIL = string.Format("select sum(ILSOA) from IL where ITEM='{0}'", a);
            string sumISL = string.Format("select sum(ISLSOA) from ISL where ITEM='{0}'", a);
            string sumIW = string.Format("select sum(IWSOA) from IW where ITEM='{0}'", a);
            string sumIM = string.Format("select sum(IMSOA) from IM where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();
            int sumLBint = Int32.Parse(var.GetDataByQuery(sumLB).ToString());
            int sumILint = Int32.Parse(var.GetDataByQuery(sumIL).ToString());
            int sumISLint = Int32.Parse(var.GetDataByQuery(sumISL).ToString());
            int sumIWint = Int32.Parse(var.GetDataByQuery(sumIW).ToString());
            int sumIMint = Int32.Parse(var.GetDataByQuery(sumIM).ToString());

            if ((sumLBint == sumILint) &&
               (sumLBint == sumISLint) &&
               (sumLBint == sumIWint) &&
               (sumLBint == sumIMint))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //sum va check picked 5 bang 
        public static bool checkSumPicked(string a)
        {
            string sumLB = string.Format("select sum(LBSPK) from LB where ITEM='{0}'", a);
            string sumIL = string.Format("select sum(ILSPK) from IL where ITEM='{0}'", a);
            string sumISL = string.Format("select sum(ISLSPK) from ISL where ITEM='{0}'", a);
            string sumIW = string.Format("select sum(IWSPK) from IW where ITEM='{0}'", a);
            string sumIM = string.Format("select sum(IMSPK) from IM where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();
            int sumLBint = Int32.Parse(var.GetDataByQuery(sumLB).ToString());
            int sumILint = Int32.Parse(var.GetDataByQuery(sumIL).ToString());
            int sumISLint = Int32.Parse(var.GetDataByQuery(sumISL).ToString());
            int sumIWint = Int32.Parse(var.GetDataByQuery(sumIW).ToString());
            int sumIMint = Int32.Parse(var.GetDataByQuery(sumIM).ToString());

            if ((sumLBint == sumILint) &&
               (sumLBint == sumISLint) &&
               (sumLBint == sumIWint) &&
               (sumLBint == sumIMint))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //sum va check Scheduled 2 bang 
        public static bool checkSumScheduled(string a)
        {
            string sumIM = string.Format("select sum (IMSSA) from IM where ITEM='{0}'", a);
            string sumIW = string.Format("select sum (IWSSA) from IW where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();

            int sumIWint = Int32.Parse(var.GetDataByQuery(sumIW).ToString());
            int sumIMint = Int32.Parse(var.GetDataByQuery(sumIM).ToString());

            if (sumIWint == sumIMint)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //hàm check_Allocate_3vs2(): 
        //tham số string a
        //trả về true nếu 3 bảng LB IL ISL =0 và IW !=0; ngược lại thì false
        public static bool check_Allocate_3vs2(string a)
        {
            string sumLB = string.Format("select sum(LBSOA) as LBSOA from LB where ITEM='{0}'", a);
            string sumIL = string.Format("select sum(ILSOA) as ILSOA from IL where ITEM='{0}'", a);
            string sumISL = string.Format("select sum(ISLSOA) as ISLSOA from ISL where ITEM='{0}'", a);
            string sumIW = string.Format("select sum(IWSOA) as IWSOA from IW where ITEM='{0}'", a);
            //string sumIM = string.Format("select sum(IMSOA) from IM where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();

            DataRow cellValue1 = var.GetDataByQuery(sumLB).Rows[0];
            string x1 = cellValue1[0].ToString();
            int sumLBint = Convert.ToInt32(x1);

            DataRow cellValue2 = var.GetDataByQuery(sumIL).Rows[0];
            string x2 = cellValue2[0].ToString();
            int sumILint = Convert.ToInt32(x2);

            DataRow cellValue3 = var.GetDataByQuery(sumISL).Rows[0];
            string x3 = cellValue3[0].ToString();
            int sumISLint = Convert.ToInt32(x3);

            DataRow cellValue4 = var.GetDataByQuery(sumIW).Rows[0];
            string x4 = cellValue4[0].ToString();
            int sumIWint = Convert.ToInt32(x4);


            //int sumLBint = Int32.Parse(var.GetDataByQuery(sumLB).Rows.ToString());
            //int sumILint = Int32.Parse(var.GetDataByQuery(sumIL).Rows.ToString());
            //int sumISLint = Int32.Parse(var.GetDataByQuery(sumISL).Rows.ToString());
            //int sumIWint = Int32.Parse(var.GetDataByQuery(sumIW).Rows.ToString());
            //int sumIMint = Int32.Parse(var.GetDataByQuery(sumIM).ToString());

            if ((sumLBint == 0) &&
               (sumILint == 0) &&
               (sumISLint == 0) && (sumIWint != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool check_Picked_3vs2(string a)
        {
            string sumLB = string.Format("select sum(LBSPK) from LB where ITEM='{0}'", a);
            string sumIL = string.Format("select sum(ILSPK) from IL where ITEM='{0}'", a);
            string sumISL = string.Format("select sum(ISLSPK) from ISL where ITEM='{0}'", a);
            string sumIW = string.Format("select sum(IWSPK) from IW where ITEM='{0}'", a);

            As400ConnectionUtility var = new As400ConnectionUtility();
            int sumLBint = Int32.Parse(var.GetDataByQuery(sumLB).ToString());
            int sumILint = Int32.Parse(var.GetDataByQuery(sumIL).ToString());
            int sumISLint = Int32.Parse(var.GetDataByQuery(sumISL).ToString());
            int sumIWint = Int32.Parse(var.GetDataByQuery(sumIW).ToString());

            if ((sumLBint == 0) &&
               (sumILint == 0) &&
               (sumISLint == 0) && (sumIWint != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public class ItemLB
    {
        public string ITEM;
        public string WHS;
        public string LOCN;
        public string ISL;
        public int LBSOA;

        
    }

    public class ItemScheduled
    {
        public string ITEM;
        public string WHS;
        public int IWSSA;
        public int IMSSA;
    }

    public class ItemPicked
    {
        public string ITEM;
        public string WHS;
        public string LOCN;
        public string ISL;
        public int LBSPK;

        
    }

}
