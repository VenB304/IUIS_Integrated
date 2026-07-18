namespace IUIS.Modules.Team8.Models
{
    public class TuitionAssessment
    {
        public decimal TuitionPerUnit { get; set; }
        public decimal MiscFees { get; set; }
        public decimal LabFees { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAssessment { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }

        public void CalculateTotal(int totalUnits)
        {
            TotalAssessment = (TuitionPerUnit * totalUnits) + MiscFees + LabFees - Discount;
            Balance = TotalAssessment - AmountPaid;
        }
    }
}
