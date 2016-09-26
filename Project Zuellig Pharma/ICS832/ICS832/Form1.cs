using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICS832
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string item = textBox1.Text;
            string ketQuaUpdate = "";

            #region select from IA 
            var as400 = new As400ConnectionUtility();
            //string selectIA = string.Format(
            //    "SELECT ITEM, ISL#, LOCN, WHS ,SUM(IAQA) IAQA FROM ia WHERE ttype in ('OR', 'PS','BP','RP') and iadel='' and IAQA <> 0 and item = '{0}' group by ITEM, ISL#, LOCN, WHS ",
            //    item);

            //kho WM1
            //string selectIA = string.Format(
            //    "SELECT ITEM, ISL#, LOCN, WHS ,SUM(IAQA) IAQA FROM ASLF0183 WHERE ttype in ('OR', 'PS','BP','RP') and iadel='' and IAQA <> 0 and item = '{0}' group by ITEM, ISL#, LOCN, WHS ",
            //    item);

            //kho WT2
            string selectIA = string.Format(
                "SELECT ITEM, ISL#, LOCN, WHS ,SUM(IAQA) IAQA FROM ASLF0183 WHERE ttype in ('OR', 'PS','BP','RP','BI') and iadel='' and IAQA <> 0 and item = '{0}' group by ITEM, ISL#, LOCN, WHS ",
                item);

            DataTable dataAs400 = as400.GetDataByQuery(selectIA);

            List<Item> items = new List<Item>();

            foreach (DataRow dr in dataAs400.Rows)
            {
                items.Add(new Item
                {
                    ITEM = dr["ITEM"].ToString().Trim(),
                    WHS = dr["WHS"].ToString().Trim(),
                    LOCN = dr["LOCN"].ToString().Trim(),
                    ISL = dr["ISL#"].ToString().Trim(),
                    IAQA = Int32.Parse(dr["IAQA"].ToString())
                });
            }

            string selectIA2 = string.Format("select a.ITEM,a.WHS,a.LOCN,a.ISL#,a.IAQA from ASLF0183 a, AJD b where a.item='{0}' and a.ttype ='LT' and a.ttype =b.ttype and a.batch=b.batch and AJDDEL='' and  IADEL='' and AJDSTS <> '2' and a.bhseq=b.bhseq", item);

            DataTable dataAs400LT = as400.GetDataByQuery(selectIA2);

            foreach (DataRow dr in dataAs400LT.Rows)
            {
                items.Add(new Item
                {
                    ITEM = dr["ITEM"].ToString().Trim(),
                    WHS = dr["WHS"].ToString().Trim(),
                    LOCN = dr["LOCN"].ToString().Trim(),
                    ISL = dr["ISL#"].ToString().Trim(),
                    IAQA = Int32.Parse(dr["IAQA"].ToString())
                });
            } 
            #endregion


            Item.Mass_Update_Allocate_Scheduled_Picked_To_0(item);
            //Item.Mass_Update_0_File_LB(item);
            //Item.Mass_Update_0_File_IL(item);
            //Item.Mass_Update_0_File_ISL(item);
            //Item.Mass_Update_0_File_IW(item);
            //Item.Mass_Update_0_File_IM(item);
            //Item.Mass_Update_0_scheduled_File_IW(item);
            //Item.Mass_Update_0_scheduled_File_IM(item);

            #region tam thoi comment

            ////select allocate <> 0 trong LB của item và all whS

            //#region mass update allocate <> 0 cua item đó về 0

            //string selectAllocateKhac0 =
            //    string.Format(
            //        "select ITEM,WHS,LOCN,ISL#,LBSOA FROM LB WHERE ITEM = '{0}' AND LBSOA<>0 ", item);

            ////tạo datatable lưu kết quả truy vấn 
            //DataTable dataAs400AllocateKhac0 = as400.GetDataByQuery(selectAllocateKhac0);
            ////chuyển datatable thành list<ItemLB>
            //List<ItemLB> itemLBListAllocateKhac0 = new List<ItemLB>();
            //foreach (DataRow dr in dataAs400AllocateKhac0.Rows)
            //{
            //    itemLBListAllocateKhac0.Add(new ItemLB
            //    {
            //        ITEM = dr["ITEM"].ToString(),
            //        WHS = dr["WHS"].ToString(),
            //        LOCN = dr["LOCN"].ToString(),
            //        ISL = dr["ISL#"].ToString(),
            //        LBSOA = Int32.Parse(dr["LBSOA"].ToString())
            //    });
            //}

            ////mass update allocate <> 0 cua item đó về 0
            //foreach (ItemLB a in itemLBListAllocateKhac0)
            //{
            //    int tempLBSOA = -a.LBSOA;

            //    string updateLB2 =
            //        string.Format(
            //            "UPDATE LB SET LBSOA = LBSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'",
            //            tempLBSOA, a.ITEM, a.LOCN, a.ISL, a.WHS);

            //    string updateIL2 =
            //        string.Format(
            //            "UPDATE IL SET ILSOA = ILSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'",
            //            tempLBSOA, a.ITEM, a.LOCN, a.WHS);

            //    string updateISL2 =
            //        string.Format("UPDATE ISL SET ISLSOA = ISLSOA + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
            //            tempLBSOA, a.ITEM, a.ISL);

            //    string updateIW2 =
            //        string.Format("UPDATE IW SET IWSOA = IWSOA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
            //            tempLBSOA, a.ITEM, a.WHS);

            //    string updateIM2 = string.Format("UPDATE IM SET IMSOA = IMSOA + {0} WHERE ITEM = '{1}'", tempLBSOA,
            //        a.ITEM);

            //    as400.ExecuteQuery(updateLB2);
            //    as400.ExecuteQuery(updateIL2);
            //    as400.ExecuteQuery(updateISL2);
            //    as400.ExecuteQuery(updateIW2);
            //    as400.ExecuteQuery(updateIM2);
            //}
            ////if (Item.check_Allocate_3vs2(item))
            ////{
            ////    string Allocate_IW_Khac0 =string.Format("select ITEM,WHS,IWSOA FROM IW WHERE ITEM = '{0}' AND IWSOA<>0 ", item);

            ////    //tạo datatable lưu kết quả truy vấn 
            ////    DataTable dtTable= as400.GetDataByQuery(Allocate_IW_Khac0);
            ////    //chuyển datatable thành list<ItemLB>
            ////    List<ItemLB> listItemLbs = new List<ItemLB>();
            ////    if (dtTable != null)
            ////        foreach (DataRow dr in dtTable.Rows)
            ////        {
            ////            listItemLbs.Add(new ItemLB
            ////            {
            ////                ITEM = dr["ITEM"].ToString(),
            ////                WHS = dr["WHS"].ToString(),
            ////                LBSOA = Int32.Parse(dr["LBSOA"].ToString())
            ////            });
            ////        }
            ////}

            //#endregion

            ////select LBSPK<>0 trong LB của của item và all whS

            //#region mass update PICKED <>0 của item đó về 0

            //string selectPickedKhac0 = string.Format(
            //    "select ITEM,WHS,LOCN,ISL#,LBSPK FROM LB WHERE ITEM = '{0}' AND LBSPK<>0",
            //    item);

            //DataTable dataAs400PickedKhac0 = as400.GetDataByQuery(selectPickedKhac0);
            //List<ItemPicked> itemPickedListKhac0 = new List<ItemPicked>();
            //foreach (DataRow dr in dataAs400PickedKhac0.Rows)
            //{

            //    itemPickedListKhac0.Add(new ItemPicked
            //    {
            //        ITEM = dr["ITEM"].ToString().Trim(),
            //        WHS = dr["WHS"].ToString().Trim(),
            //        LOCN = dr["LOCN"].ToString().Trim(),
            //        ISL = dr["ISL#"].ToString().Trim(),
            //        LBSPK = Int32.Parse(dr["LBSPK"].ToString())
            //    });
            //}

            ////mass update PICKED <>0 của item đó về 0
            //foreach (ItemPicked a in itemPickedListKhac0)
            //{
            //    int tempLBSPK = -a.LBSPK;

            //    string updateLB2 =
            //        string.Format(
            //            "UPDATE LB SET LBSPK = LBSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'",
            //            tempLBSPK, a.ITEM, a.LOCN, a.ISL, a.WHS);

            //    string updateIL2 =
            //        string.Format(
            //            "UPDATE IL SET ILSPK = ILSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'",
            //            tempLBSPK, a.ITEM, a.LOCN, a.WHS);

            //    string updateISL2 =
            //        string.Format("UPDATE ISL SET ISLSPK = ISLSPK + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
            //            tempLBSPK, a.ITEM, a.ISL);

            //    string updateIW2 =
            //        string.Format("UPDATE IW SET IWSPK = IWSPK + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
            //            tempLBSPK, a.ITEM, a.WHS);

            //    string updateIM2 = string.Format("UPDATE IM SET IMSPK = IMSPK + {0} WHERE ITEM = '{1}'", tempLBSPK,
            //        a.ITEM);

            //    as400.ExecuteQuery(updateLB2);
            //    as400.ExecuteQuery(updateIL2);
            //    as400.ExecuteQuery(updateISL2);
            //    as400.ExecuteQuery(updateIW2);
            //    as400.ExecuteQuery(updateIM2);
            //}

            //#endregion

            ////select scheduled <> 0 trong IW của item và all whS

            //#region mas update scheduled<>0 của item đó trong file IW về 0

            //string selectScheduledKhac0 =
            //    string.Format("select ITEM,WHS,IWSSA FROM IW WHERE ITEM = '{0}' AND IWSSA<>0 ", item);

            //DataTable dataAs400ScheduledITemKhac0 = as400.GetDataByQuery(selectScheduledKhac0);
            //List<ItemScheduled> itemScheduledListKhac0 = new List<ItemScheduled>();

            //foreach (DataRow dr in dataAs400ScheduledITemKhac0.Rows)
            //{

            //    itemScheduledListKhac0.Add(new ItemScheduled
            //    {
            //        ITEM = dr["ITEM"].ToString().Trim(),
            //        WHS = dr["WHS"].ToString().Trim(),
            //        IWSSA = Int32.Parse(dr["IWSSA"].ToString())
            //    });
            //}
            ////mas update scheduled<>0 của item đó trong file IW về 0
            //foreach (ItemScheduled a in itemScheduledListKhac0)
            //{
            //    int tempIWSSA = -a.IWSSA;

            //    string updateIW = string.Format(
            //        "UPDATE IW SET IWSSA=IWSSA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'", tempIWSSA, a.ITEM, a.WHS);
            //    as400.ExecuteQuery(updateIW);

            //}
            //string updateIMVe0 = string.Format("UPDATE IM SET IMSSA = 0 WHERE ITEM = '{0}'", item);
            //as400.ExecuteQuery(updateIMVe0);

            //#endregion

            #endregion

            //update theo IA lặp trên all line của list item:
            #region update theo IA lặp trên all line của list item
            foreach (Item i in items)
            {
                //nếu locn='Scheduled': update IWSSA cho IW item,whs của IA.
                if (i.LOCN == "Scheduled")
                {
                    //string selectScheduled =
                    //    string.Format("select ITEM,WHS,IWSSA FROM IW WHERE ITEM = '{0}' AND WHS='{1}' ", i.ITEM, i.WHS);
                    //DataTable dataAs400ScheduledITem = as400.GetDataByQuery(selectScheduled);
                    //ItemScheduled itemScheduled = new ItemScheduled();
                    //foreach (DataRow dr in dataAs400ScheduledITem.Rows)
                    //{
                    //    ItemScheduled tempItem = new ItemScheduled
                    //    {
                    //        ITEM = dr["ITEM"].ToString().Trim(),
                    //        WHS = dr["WHS"].ToString().Trim(),
                    //        IWSSA = Int32.Parse(dr["IWSSA"].ToString())
                    //    };
                    //    itemScheduled = tempItem;
                    //}

                    string updateIW = string.Format(
                        "UPDATE IW SET IWSSA=IWSSA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'", i.IAQA,
                        i.ITEM, i.WHS);
                    string updateIM = string.Format("UPDATE IM SET IMSSA=IMSSA + {0} WHERE ITEM = '{1}'",
                        i.IAQA, i.ITEM);

                    as400.ExecuteQuery(updateIW);
                    as400.ExecuteQuery(updateIM);

                    ketQuaUpdate += "Update Scheduled thanh cong. ";

                }
                //update PICKED 
                else if (i.LOCN == "PICKED")
                {
                    string updateLBPICKED =
                            string.Format(
                                "UPDATE LB SET LBSPK = LBSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'",
                                i.IAQA, i.ITEM, i.LOCN, i.ISL, i.WHS);

                    string updateILPICKED =
                        string.Format(
                            "UPDATE IL SET ILSPK = ILSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'",
                            i.IAQA, i.ITEM, i.LOCN, i.WHS);

                    string updateISLPICKED =
                        string.Format("UPDATE ISL SET ISLSPK = ISLSPK + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
                            i.IAQA, i.ITEM, i.ISL);

                    string updateIWPICKED =
                        string.Format("UPDATE IW SET IWSPK = IWSPK + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
                            i.IAQA, i.ITEM, i.WHS);

                    string updateIMPICKED = string.Format("UPDATE IM SET IMSPK = IMSPK + {0} WHERE ITEM = '{1}'", i.IAQA,
                        i.ITEM);

                    as400.ExecuteQuery(updateLBPICKED);
                    as400.ExecuteQuery(updateILPICKED);
                    as400.ExecuteQuery(updateISLPICKED);
                    as400.ExecuteQuery(updateIWPICKED);
                    as400.ExecuteQuery(updateIMPICKED);

                    ketQuaUpdate += "Update PICKED thanh cong. ";
                }
                else
                {
                    string updateLBAllocate =
                            string.Format(
                                "UPDATE LB SET LBSOA = LBSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'",
                                i.IAQA, i.ITEM, i.LOCN, i.ISL, i.WHS);

                    string updateILAllocate =
                        string.Format(
                            "UPDATE IL SET ILSOA = ILSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'",
                            i.IAQA, i.ITEM, i.LOCN, i.WHS);

                    string updateISLAllocate =
                        string.Format("UPDATE ISL SET ISLSOA = ISLSOA + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
                            i.IAQA, i.ITEM, i.ISL);

                    string updateIWAllocate =
                        string.Format("UPDATE IW SET IWSOA = IWSOA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
                            i.IAQA, i.ITEM, i.WHS);

                    string updateIMAllocate = string.Format("UPDATE IM SET IMSOA = IMSOA + {0} WHERE ITEM = '{1}'", i.IAQA, i.ITEM);

                    as400.ExecuteQuery(updateLBAllocate);
                    as400.ExecuteQuery(updateILAllocate);
                    as400.ExecuteQuery(updateISLAllocate);
                    as400.ExecuteQuery(updateIWAllocate);
                    as400.ExecuteQuery(updateIMAllocate);

                    ketQuaUpdate += "Update Allocate thanh cong. ";
                }
            } 
            #endregion
            //lblKetQua.Text = item.ToString() + " " + ketQuaUpdate;

            lblKetQua.Text = item.ToString() + " đã fix xong !!! ";

            int sumAllocate=0, sumScheduled=0, sumPicked =0;

            foreach (Item item1 in items)
            {
                if (item1.LOCN == "Scheduled")
                {
                    sumScheduled = sumScheduled + item1.IAQA;
                }
                else if (item1.LOCN == "PICKED")
                {
                    sumPicked = sumPicked + item1.IAQA;
                }
                else
                {
                    sumAllocate = sumAllocate + item1.IAQA;
                }
            }

            lblAllocateForIA.Text = sumAllocate.ToString();
            lblPickedForIA.Text = sumPicked.ToString();
            lblScheduledForIA.Text = sumScheduled.ToString();

            #region MyRegion
            //if (Item.checkSumAllocate(item))
            //{
            //    lblAllocate.Text = "Allocate đã đúng";
            //}
            //else
            //{
            //    lblAllocate.Text = "Allocate 5 bảng không bằng nhau. Fix tay tiếp nhé";
            //}

            //if (Item.checkSumPicked(item))
            //    lblPicked.Text = "picked đã đúng";
            //else
            //{
            //    lblPicked.Text = "picked 5 bảng không bằng nhau. Fix tay tiếp nhé";
            //}

            //if (Item.checkSumScheduled(item))
            //    lblScheduled.Text = "scheduled đã đúng";
            //else
            //{
            //    lblScheduled.Text = "scheduled 2 bảng không bằng nhau. Fix tay tiếp nhé";
            //} 
            #endregion

            #region Show Sum allocate 5 bảng
            int lbAllocate = 0, ilAllocate = 0, islAllocate = 0, iwAllocate = 0, imAllocate = 0;
            Item.ShowSumAllocate(item, ref lbAllocate, ref ilAllocate, ref islAllocate, ref iwAllocate, ref imAllocate);
            lbLBSOA.Text = lbAllocate.ToString();
            lbILSOA.Text = ilAllocate.ToString();
            lbISLSOA.Text = islAllocate.ToString();
            lbIWSOA.Text = iwAllocate.ToString();
            lbIMSOA.Text = imAllocate.ToString();

            int lbPicked = 0, ilPicked = 0, islPicked = 0, iwPicked = 0, imPicked = 0;
            Item.ShowSumPicked(item, ref lbPicked, ref ilPicked, ref islPicked, ref iwPicked, ref imPicked);
            lbLBSPK.Text = lbPicked.ToString();
            lbILSPK.Text = ilPicked.ToString();
            lbISLSPK.Text = islPicked.ToString();
            lbIWSPK.Text = iwPicked.ToString();
            lbIMSPK.Text = imPicked.ToString();

            int iwssa = 0, imssa = 0;
            Item.ShowScheduled(item, ref iwssa, ref imssa);
            lbIWSSA.Text = iwssa.ToString();
            lblIMSSA.Text = imssa.ToString(); 
            #endregion

            #region bản cũ

            //=========================================================================
            //update cho tat ca locn slot cho mot item 
            //foreach (Item i in items)
            //{
            //    //update Scheduled
            //    if (i.LOCN == "Scheduled")
            //    {
            //        string selectScheduled =
            //            string.Format("select ITEM,WHS,IWSSA FROM IW WHERE ITEM = '{0}' AND WHS='{1}' ", i.ITEM, i.WHS);

            //        DataTable dataAs400ScheduledITem = as400.GetDataByQuery(selectScheduled);

            //        ItemScheduled itemScheduled = new ItemScheduled();

            //        foreach (DataRow dr in dataAs400ScheduledITem.Rows)
            //        {
            //            ItemScheduled tempItem = new ItemScheduled
            //            {
            //                ITEM = dr["ITEM"].ToString().Trim(),
            //                WHS = dr["WHS"].ToString().Trim(),
            //                IWSSA = Int32.Parse(dr["IWSSA"].ToString())
            //            };
            //            itemScheduled = tempItem;
            //        }

            //        //check IWSSA va IA theo line trong IA
            //        if (i.IAQA != itemScheduled.IWSSA)
            //        {
            //            int varianceScheduled = i.IAQA - itemScheduled.IWSSA;

            //            string updateIW = string.Format(
            //                "UPDATE IW SET IWSSA=IWSSA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'", varianceScheduled,
            //                i.ITEM, i.WHS);
            //            string updateIM = string.Format("UPDATE IM SET IMSSA=IMSSA + {0} WHERE ITEM = '{1}'",
            //                varianceScheduled, i.ITEM);

            //            as400.ExecuteQuery(updateIW);
            //            as400.ExecuteQuery(updateIM);

            //            ketQuaUpdate += "Update Scheduled thanh cong. ";

            //        }

            //        //check sum sum (IMSSA) va sum (IWSSA), neu khac thi update IMSSA = IWSSA 
            //        string strCheckSumIM = string.Format("select ITEM,sum (IMSSA) as IMSSA from IM where item ='{0}' GROUP BY ITEM", i.ITEM);
            //        string strCheckSumIW = string.Format("select ITEM,WHS,sum (IWSSA) as IWSSA from IW where item ='{0}' and whs='{1}' GROUP BY ITEM,WHS", i.ITEM,i.WHS);

            //        DataTable dataAs400SumIwssa = as400.GetDataByQuery(strCheckSumIW);
            //        DataTable dataAs400SumImssa = as400.GetDataByQuery(strCheckSumIM);

            //        ItemScheduled iw = new ItemScheduled();
            //        ItemScheduled im = new ItemScheduled();

            //        foreach (DataRow dr in dataAs400SumIwssa.Rows)
            //        {
            //            ItemScheduled tempItem = new ItemScheduled
            //            {
            //                ITEM = dr["ITEM"].ToString().Trim(),
            //                WHS = dr["WHS"].ToString().Trim(),
            //                IWSSA = Int32.Parse(dr["IWSSA"].ToString())
            //            };
            //            iw = tempItem;
            //        }

            //        foreach (DataRow dr in dataAs400SumImssa.Rows)
            //        {
            //            ItemScheduled tempItem = new ItemScheduled
            //            {
            //                ITEM = dr["ITEM"].ToString().Trim(),
            //                IMSSA = Int32.Parse(dr["IMSSA"].ToString())
            //            };
            //            im = tempItem;
            //        }

            //        if (iw.IWSSA != im.IMSSA)
            //        {
            //            string updateIwToIm = string.Format("update IM set IMSSA = {0} where ITEM={1}", iw.IWSSA, i.ITEM);
            //            as400.ExecuteQuery(updateIwToIm);
            //        }

            //    }

            //    //update PICKED 
            //    else if (i.LOCN == "PICKED")
            //    {
            //        string selectPicked = string.Format(
            //            "select ITEM,WHS,LOCN,ISL#,LBSPK FROM LB WHERE ITEM = '{0}' AND WHS='{1}' AND LOCN= '{2}' AND ISL#='{3}' ",
            //            i.ITEM, i.WHS, i.LOCN, i.ISL);

            //        DataTable dataAs400PickedITem = as400.GetDataByQuery(selectPicked);

            //        ItemPicked itemPicked = new ItemPicked();

            //        foreach (DataRow dr in dataAs400PickedITem.Rows)
            //        {
            //            ItemPicked tempItem = new ItemPicked
            //            {
            //                ITEM = dr["ITEM"].ToString().Trim(),
            //                WHS = dr["WHS"].ToString().Trim(),
            //                LOCN = dr["LOCN"].ToString().Trim(),
            //                ISL = dr["ISL#"].ToString().Trim(),
            //                LBSPK = Int32.Parse(dr["LBSPK"].ToString())
            //            };
            //            itemPicked = tempItem;
            //        }

            //        //update PICKED 5 bang
            //        if (i.IAQA != itemPicked.LBSPK)
            //        {
            //            int variance = i.IAQA - itemPicked.LBSPK;

            //            string updateLB =
            //                string.Format(
            //                    "UPDATE LB SET LBSPK = LBSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'",
            //                    variance, i.ITEM, i.LOCN, i.ISL, i.WHS);

            //            string updateIL =
            //                string.Format(
            //                    "UPDATE IL SET ILSPK = ILSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'",
            //                    variance, i.ITEM, i.LOCN, i.WHS);

            //            string updateISL =
            //                string.Format("UPDATE ISL SET ISLSPK = ISLSPK + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
            //                    variance, i.ITEM, i.ISL);

            //            string updateIW =
            //                string.Format("UPDATE IW SET IWSPK = IWSPK + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
            //                    variance, i.ITEM, i.WHS);

            //            string updateIM = string.Format("UPDATE IM SET IMSPK = IMSPK + {0} WHERE ITEM = '{1}'", variance,
            //                i.ITEM);

            //            as400.ExecuteQuery(updateLB);
            //            as400.ExecuteQuery(updateIL);
            //            as400.ExecuteQuery(updateISL);
            //            as400.ExecuteQuery(updateIW);
            //            as400.ExecuteQuery(updateIM);

            //            ketQuaUpdate += "Update PICKED thanh cong. ";
            //        }
            //    }
            //    else
            //    {
            //        string selectAllocate =
            //            string.Format(
            //                "select ITEM,WHS,LOCN,ISL#,LBSOA FROM LB WHERE ITEM = '{0}' AND WHS='{1}' AND LOCN= '{2}' AND ISL#='{3}' ",
            //                i.ITEM, i.WHS, i.LOCN, i.ISL);

            //        DataTable dataAs400LbITem = as400.GetDataByQuery(selectAllocate);

            //        ItemLB itemLB = new ItemLB();

            //        foreach (DataRow dr in dataAs400LbITem.Rows)
            //        {
            //            ItemLB tempItem = new ItemLB
            //            {
            //                ITEM = dr["ITEM"].ToString(),
            //                WHS = dr["WHS"].ToString(),
            //                LOCN = dr["LOCN"].ToString(),
            //                ISL = dr["ISL#"].ToString(),
            //                LBSOA = Int32.Parse(dr["LBSOA"].ToString())
            //            };
            //            itemLB = tempItem;
            //        }

            //        //update allocate
            //        if (i.IAQA != itemLB.LBSOA)
            //        {
            //            int variance = i.IAQA - itemLB.LBSOA;

            //            string updateLB =
            //                string.Format(
            //                    "UPDATE LB SET LBSOA = LBSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'",
            //                    variance, itemLB.ITEM, itemLB.LOCN, itemLB.ISL, itemLB.WHS);

            //            string updateIL =
            //                string.Format(
            //                    "UPDATE IL SET ILSOA = ILSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'",
            //                    variance, itemLB.ITEM, itemLB.LOCN, itemLB.WHS);

            //            string updateISL =
            //                string.Format("UPDATE ISL SET ISLSOA = ISLSOA + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
            //                    variance, itemLB.ITEM, itemLB.ISL);

            //            string updateIW =
            //                string.Format("UPDATE IW SET IWSOA = IWSOA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
            //                    variance, itemLB.ITEM, itemLB.WHS);

            //            string updateIM = string.Format("UPDATE IM SET IMSOA = IMSOA + {0} WHERE ITEM = '{1}'", variance,
            //                itemLB.ITEM);

            //            as400.ExecuteQuery(updateLB);
            //            as400.ExecuteQuery(updateIL);
            //            as400.ExecuteQuery(updateISL);
            //            as400.ExecuteQuery(updateIW);
            //            as400.ExecuteQuery(updateIM);

            //            ketQuaUpdate += "Update Allocate thanh cong. ";
            //        }

            //    }
            //    lblKetQua.Text = "Xu li xong. " + ketQuaUpdate;

            //}


            ////update ALLOCATE line khong co trong IA = 0  -------------------------------------------------
            //string selectAllocate2 =
            //            string.Format(
            //                "select ITEM,WHS,LOCN,ISL#,LBSOA FROM LB WHERE ITEM = '{0}' AND LBSOA<>0 ",item);

            //DataTable dataAs400LbITem2 = as400.GetDataByQuery(selectAllocate2);
            //List<ItemLB> itemLBList = new List<ItemLB>();
            //foreach (DataRow dr in dataAs400LbITem2.Rows)
            //{
            //    itemLBList.Add(new ItemLB
            //    {
            //        ITEM = dr["ITEM"].ToString(),
            //        WHS = dr["WHS"].ToString(),
            //        LOCN = dr["LOCN"].ToString(),
            //        ISL = dr["ISL#"].ToString(),
            //        LBSOA = Int32.Parse(dr["LBSOA"].ToString())
            //    });
            //}
            //bool alreadyExistAllocate = false;
            //foreach (ItemLB a in itemLBList)
            //{
            //    foreach (Item aa in items)
            //    {
            //        if ((a.ITEM == aa.ITEM) && (a.WHS == aa.WHS) && (a.LOCN == aa.LOCN) && (a.ISL == aa.ISL))
            //        {
            //            alreadyExistAllocate = true;
            //        }
            //    }
            //    if (alreadyExistAllocate == false)
            //    {
            //        int tempLBSOA = -a.LBSOA;

            //        string updateLB2 = string.Format("UPDATE LB SET LBSOA = LBSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'", tempLBSOA, a.ITEM, a.LOCN, a.ISL, a.WHS);

            //        string updateIL2 =
            //            string.Format("UPDATE IL SET ILSOA = ILSOA + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'", tempLBSOA, a.ITEM, a.LOCN, a.WHS);

            //        string updateISL2 =
            //            string.Format("UPDATE ISL SET ISLSOA = ISLSOA + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
            //                tempLBSOA, a.ITEM, a.ISL);

            //        string updateIW2 =
            //            string.Format("UPDATE IW SET IWSOA = IWSOA + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
            //                tempLBSOA, a.ITEM, a.WHS);

            //        string updateIM2 = string.Format("UPDATE IM SET IMSOA = IMSOA + {0} WHERE ITEM = '{1}'", tempLBSOA, a.ITEM);

            //        as400.ExecuteQuery(updateLB2);
            //        as400.ExecuteQuery(updateIL2);
            //        as400.ExecuteQuery(updateISL2);
            //        as400.ExecuteQuery(updateIW2);
            //        as400.ExecuteQuery(updateIM2);
            //    }
            //}



            ////update PICKED line khong co trong IA = 0 ------------------------------------------------
            //    string selectPicked2 = string.Format(
            //            "select ITEM,WHS,LOCN,ISL#,LBSPK FROM LB WHERE ITEM = '{0}' AND LBSPK<>0",
            //            item);

            //DataTable dataAs400PickedITem2 = as400.GetDataByQuery(selectPicked2);
            //List<ItemPicked> itemPickedList = new List<ItemPicked>();
            //foreach (DataRow dr in dataAs400PickedITem2.Rows)
            //{

            //    itemPickedList.Add(new ItemPicked
            //    {
            //        ITEM = dr["ITEM"].ToString().Trim(),
            //        WHS = dr["WHS"].ToString().Trim(),
            //        LOCN = dr["LOCN"].ToString().Trim(),
            //        ISL = dr["ISL#"].ToString().Trim(),
            //        LBSPK = Int32.Parse(dr["LBSPK"].ToString())
            //    });
            //}

            //bool alreadyExist = false;
            //foreach (ItemPicked a in itemPickedList)
            //{
            //    foreach (Item aa in items)
            //    {
            //        if ((a.ITEM == aa.ITEM) && (a.WHS == aa.WHS) && (a.LOCN == aa.LOCN) && (a.ISL == aa.ISL))
            //        {
            //            alreadyExist = true;
            //        }
            //    }

            //    if (alreadyExist == false)
            //    {
            //        int tempLBSPK = -a.LBSPK;

            //        string updateLB2 = string.Format("UPDATE LB SET LBSPK = LBSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND ISL#='{3}' AND WHS = '{4}'", tempLBSPK, a.ITEM, a.LOCN, a.ISL, a.WHS);

            //        string updateIL2 =
            //            string.Format("UPDATE IL SET ILSPK = ILSPK + {0} WHERE ITEM = '{1}' AND LOCN = '{2}' AND WHS = '{3}'", tempLBSPK, a.ITEM, a.LOCN, a.WHS);

            //        string updateISL2 =
            //            string.Format("UPDATE ISL SET ISLSPK = ISLSPK + {0} WHERE ITEM = '{1}' AND ISL#='{2}'",
            //                tempLBSPK, a.ITEM, a.ISL);

            //        string updateIW2 =
            //            string.Format("UPDATE IW SET IWSPK = IWSPK + {0} WHERE ITEM = '{1}' AND WHS = '{2}'",
            //                tempLBSPK, a.ITEM, a.WHS);

            //        string updateIM2 = string.Format("UPDATE IM SET IMSPK = IMSPK + {0} WHERE ITEM = '{1}'", tempLBSPK, a.ITEM);

            //        as400.ExecuteQuery(updateLB2);
            //        as400.ExecuteQuery(updateIL2);
            //        as400.ExecuteQuery(updateISL2);
            //        as400.ExecuteQuery(updateIW2);
            //        as400.ExecuteQuery(updateIM2);
            //    }
            //}

            #endregion

        }
    }
}
