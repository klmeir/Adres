namespace Adres.Api.Entities
{   
    public class DocumentationFile : DomainEntity
    {        
        public int AcquisitionId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
