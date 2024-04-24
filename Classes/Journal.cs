namespace MotherProjectv01.Classes
{
  public class Journal
  {
    public int Number { get; set; }
    public string DateCreated { get; set; }
    public string NameRider { get; set; }
    public string Routes { get; set; }
    public string MarkCar { get; set; }
    public string NumberCar { get; set; }
    public string NameMehanic { get; set; }
    public string NameDispetcher { get; set; }
    public int idRider { get; set; }
    public int idCar { get; set; }
    public int idRoute { get; set; }
    public int idDispetcher { get;set; }
    public int idMehanic { get; set; }     
    public int OrganisationId { get; set; }
  }
}
