namespace Adres.Api.Entities
{   
    public class Acquisition : DomainEntity
    {        
        public decimal Budget { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal TotalValue { get; set; }
        public DateOnly AcquisitionDate { get; set; }
        public string Supplier { get; set; }
        public List<DocumentationFile> Documentation { get; set; }

        public Acquisition()
        {
        }

        // Constructor
        public Acquisition(decimal budget, string unit, string type, int quantity, decimal unitValue, decimal totalValue,
                           DateOnly acquisitionDate, string supplier, List<DocumentationFile> documentation)
        {
            Budget = budget;
            Unit = unit;
            Type = type;
            Quantity = quantity;
            UnitValue = unitValue;
            TotalValue = totalValue;
            AcquisitionDate = acquisitionDate;
            Supplier = supplier;
            Documentation = documentation;
        }

        public Acquisition Update(decimal budget, string unit, string type, int quantity, decimal unitValue, decimal totalValue,
                           DateOnly acquisitionDate, string supplier, List<DocumentationFile> documentation)
        {
            Budget = budget;
            Unit = unit;
            Type = type;
            Quantity = quantity;
            UnitValue = unitValue;
            TotalValue = totalValue;
            AcquisitionDate = acquisitionDate;
            Supplier = supplier;
            Documentation = documentation;
            return this;
        }
    }
}
