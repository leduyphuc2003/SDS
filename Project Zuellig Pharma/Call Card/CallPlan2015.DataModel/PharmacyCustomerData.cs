namespace CallPlan2015.DataModel
{
    //class dung chung cho 4 call card vi cau truc giong nhau
    public class PharmacyCustomerData
	{
        public string Cust { get; set; }
        public string CustName { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string TermsCode { get; set; }
        public string CustomerLocalGroup1 { get; set; }
        public string SalespersonCode { get; set; }
        public string SMDESC { get; set; }

		public string PrnCode { get; set; }
		public string ProCode { get; set; }
		public string Description { get; set; }
        public double Ave6LastMonthValue { get; set; }
		public double MtdAllSourcesValue { get; set; }
		public double MtdScSourcesValue { get; set; }
        public double Ave6LastMonthQuantity { get; set; }
        public double MtdAllSourcesQuantity { get; set; }
        public double MtdScSourcesQuantity { get; set; }
		public int CheckStock { get; set; }
        public string BestSaleFlag { get; set; }

        public int quotaRemain { get; set; }
	}
}
