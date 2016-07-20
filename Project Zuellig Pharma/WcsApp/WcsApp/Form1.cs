using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WcsApp
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer aTimer;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            aTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //-----------------------------------------------------------
            //var as400 = new As400ConnectionUtility();
            //string slect = string.Format("select ORDR, PSN, CTMCDE,PSHCTN,PSHSTS from psh where ttype = 'PS' and PSHDEL = '' and PSHDAT = 20160719");

            //DataTable dataAs400 = as400.GetDataByQuery(slect);

            ////select data from AS400
            //List<As400Table> retval = new List<As400Table>();
            //foreach (DataRow dr in dataAs400.Rows)
            //{
            //    retval.Add(new As400Table
            //    {
            //        ORDR = Convert.ToInt32(dr["ORDR"].ToString()),
            //        PSN = Convert.ToInt32(dr["PSN"].ToString()),
            //        CTMCDE = dr["CTMCDE"].ToString(),
            //        PSHCTN = Convert.ToInt32(dr["PSHCTN"]),
            //        CHK = dr["PSHSTS"].ToString()
            //    });
            //}

            //var sqlInsert = new SQLConnectionUtility();

            ////select data table tam
            //string temp = string.Format("select * from temp");
            //DataTable tempTable = sqlInsert.GetData(temp);
            //List<As400Table> temp2 = new List<As400Table>();
            //foreach (DataRow dr in tempTable.Rows)
            //{
            //    temp2.Add(new As400Table
            //    {
            //        ORDR = Convert.ToInt32(dr["ORDR"].ToString()),
            //        PSN = Convert.ToInt32(dr["PSN"].ToString()),
            //        CTMCDE = dr["CTMCDE"].ToString(),
            //        PSHCTN = Convert.ToInt32(dr["PSHCTN"]),
            //        CHK = dr["CHK"].ToString()
            //    });
            //}

            ////select data trong retval va khong co trong temp2
            ////var result = retval.Where(p => !temp2.Any(p2 => p2.PSN == p.PSN));

            //var result = from item1 in retval
            //             where !(temp2.Any(item2 => item2.PSN == item1.PSN))
            //             select item1;

            //List<As400Table> selectAs400Tables = result.Distinct().ToList();
            //var anyDuplicate = selectAs400Tables.GroupBy(x => x.PSN).Where(g => g.Count() > 1).Select(x => x.Key);
            ////insert vao temp va wcs
            //foreach (var row in selectAs400Tables)
            //{
            //    string insertTemp = string.Format("insert into temp(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}')", row.ORDR, row.PSN, row.CTMCDE, row.PSHCTN, row.CHK);
            //    string insertWCS = string.Format("insert into WCS(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}')", row.ORDR, row.PSN, row.CTMCDE, row.PSHCTN, row.CHK);
            //    sqlInsert.UpdateData(insertTemp);
            //    sqlInsert.UpdateData(insertWCS);

            //}
            //-----------------------------------------------------------

            var as400 = new As400ConnectionUtility();
            string slect = string.Format("select ORDR, PSN, CTMCDE,PSHCTN,PSHSTS from psh where ttype = 'PS' and whs='WM1' and PSHDEL = ''  and PSHCTD = {0}", DateTime.Now.ToString("yyyyMMdd"));
            //string slect = string.Format("select ORDR, PSN, CTMCDE,PSHCTN,PSHSTS from TSTLIBD.psh where ttype = 'PS' and whs='WT2' and PSHDEL = ''  and PSHCTD = {0}", DateTime.Now.ToString("yyyyMMdd"));

            DataTable dataAs400 = as400.GetDataByQuery(slect);


            //=====================================================================
            //++shuffle list
            List<As400Table> lstAs400Tables = new List<As400Table>();
            foreach (DataRow dr in dataAs400.Rows)
            {
                lstAs400Tables.Add(new As400Table
                {
                    ORDR = Convert.ToInt32(dr["ORDR"].ToString()),
                    PSN = Convert.ToInt32(dr["PSN"].ToString()),
                    CTMCDE = dr["CTMCDE"].ToString(),
                    PSHCTN = Convert.ToInt32(dr["PSHCTN"]),
                    CHK = dr["PSHSTS"].ToString()
                });
            }

            var tmp = lstAs400Tables.OrderBy(a => Guid.NewGuid());
            ListExtensions.Shuffle(tmp.ToList());
            List<As400Table> lstAs400Tables2 = tmp.ToList();
            //--shuffle list

            //++ chia doi list
            List<List<As400Table>> lstPlit = ListExtensions.ChunkBy(lstAs400Tables2);

            //set Y va N vao moi list
            foreach (var a in lstPlit[0])
            {
                a.CHK = "Y";
            }
            foreach (var a in lstPlit[1])
            {
                a.CHK = "N";
            }

            //ghep lai 2 list vao lstPlit[0]
            lstPlit[0].AddRange(lstPlit[1]);
            List<As400Table> lstAs400s2 = lstPlit[0].ToList();

            var sqlInsert = new SQLConnectionUtility();
            StringBuilder insertTemp = new StringBuilder();
            //StringBuilder insertWCS = new StringBuilder();

            //insert vao temp va wcs
            foreach (As400Table dr in lstAs400s2)
            {
                //insertTemp.AppendLine(string.Format("if not exists (select PSN from temp where ORDR = '{0}' and PSN = '{1}') begin insert into temp(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}') end", dr["ORDR"], dr["PSN"], dr["CTMCDE"], dr["PSHCTN"], dr["PSHSTS"]));
                //insertWCS.AppendLine(string.Format("if not exists (select PSN from temp where ORDR = '{0}' and PSN = '{1}') begin insert into WCS(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}') end", dr["ORDR"], dr["PSN"], dr["CTMCDE"], dr["PSHCTN"], dr["PSHSTS"]));

                insertTemp.AppendLine(string.Format("if not exists (select PSN from temp where ORDR = '{0}' and PSN = '{1}') begin insert into temp(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}'); insert into wcs(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}') " +
                                      "end", dr.ORDR, dr.PSN, dr.CTMCDE, dr.PSHCTN, dr.CHK));
            }
            sqlInsert.UpdateData(insertTemp.ToString());

            //======================================================================

            //var sqlInsert = new SQLConnectionUtility();
            //StringBuilder insertTemp = new StringBuilder();
            ////StringBuilder insertWCS = new StringBuilder();

            ////insert vao temp va wcs
            //foreach (DataRow dr in dataAs400.Rows)
            //{
            //    //insertTemp.AppendLine(string.Format("if not exists (select PSN from temp where ORDR = '{0}' and PSN = '{1}') begin insert into temp(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}') end", dr["ORDR"], dr["PSN"], dr["CTMCDE"], dr["PSHCTN"], dr["PSHSTS"]));
            //    //insertWCS.AppendLine(string.Format("if not exists (select PSN from temp where ORDR = '{0}' and PSN = '{1}') begin insert into WCS(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}') end", dr["ORDR"], dr["PSN"], dr["CTMCDE"], dr["PSHCTN"], dr["PSHSTS"]));

            //    insertTemp.AppendLine(string.Format("if not exists (select PSN from temp where ORDR = '{0}' and PSN = '{1}') begin insert into temp(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}'); insert into wcs(ORDR, PSN, CTMCDE,PSHCTN,CHK) values ({0}, {1}, '{2}',{3},'{4}') " +
            //                          "end", dr["ORDR"], dr["PSN"], dr["CTMCDE"], dr["PSHCTN"], dr["PSHSTS"]));
            //}
            //sqlInsert.UpdateData(insertTemp.ToString());
            ////sqlInsert.UpdateData(insertWCS.ToString());
        }
        


    }
}
