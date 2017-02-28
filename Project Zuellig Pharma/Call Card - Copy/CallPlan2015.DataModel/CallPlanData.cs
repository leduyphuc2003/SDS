namespace CallPlan2015.DataModel
{
	public class CallPlanData
	{
        //add by ph
        //dinh nghia cac cot data se show ra trong bang callplan
        public string CallPlanDate { get; set; }
        public string SalespersonCode { get; set; }
        public string SalespersonsName { get; set; }
        public string PDSalesTeamCode { get; set; }
        public string PDSalesTeamName { get; set; }
        public int LeftWD { get; set; }
        public int PassedWD { get; set; }
        public double TargetMTD { get; set; }
        public double SalesMTD { get; set; }
        public double MonthToGo { get; set; }
        public double TargetToday { get; set; }

		public string CustomerCode { get; set; }
		public string CustomerName { get; set; }
		public string Address { get; set; }
		public string District { get; set; }
		public string Class { get; set; }
		public double Ave6LastMonth { get; set; }
		public double MtdAllSources { get; set; }
		public double MtdScSources { get; set; }
		public double PercentSaleBySc { get; set; }
        public double Zlvl { get; set; }
	}
}
