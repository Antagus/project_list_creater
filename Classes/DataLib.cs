using System.Data.Common;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MotherProjectv01.Classes
{
  public class DataLib
  {
    static string dataCarsPath = "Data/dCars.json"; 
    static string dataRidersPath = "Data/dRiders.json"; 
    static string dataRoutesPath = "Data/dRoutes.json";
    static string dataMehanicsPath = "Data/dMechanic.json";
    static string dataDispetchersPath = "Data/dDispetchers.json";
    static string dataOrganisationPath = "Data/dOrganisation.json";
    static string dataPointStartPath = "Data/dPointStart.json"; 

    static public int GetOrganisationNumber () {
      if (File.Exists(dataOrganisationPath)) {
        string json = File.ReadAllText(dataOrganisationPath);
        OrganisationData data = JsonConvert.DeserializeObject<OrganisationData>(json);
        return data.Organisations.Find(el => el.Id == Instrumental.totalOrganisation.Id).Number;
      }
      return -1;
    }

    static public void DeleteRecord(PointStart elDelete)
    {
      string jsonContent = File.ReadAllText(dataPointStartPath);
      PointStartData rootObject = JsonConvert.DeserializeObject<PointStartData>(jsonContent);
      if (elDelete != null) rootObject.PointStarts = rootObject.PointStarts.Where(el => el.Id != elDelete.Id).ToList();
      string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
      File.WriteAllText(dataPointStartPath, updateJsonContent);
    }


    static public void DeleteRecord (Rider elDelete) {
      string jsonContent = File.ReadAllText(dataRidersPath);
      RidersData rootObject = JsonConvert.DeserializeObject<RidersData>(jsonContent);
      if (elDelete != null) rootObject.Riders = rootObject.Riders.Where(el => el.Id != elDelete.Id).ToList();
      
      string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
      
      File.WriteAllText(dataRidersPath, updateJsonContent);
    }

    static public void DeleteRecord(Organisation elDelete)
    {
      string jsonContent = File.ReadAllText(dataOrganisationPath);
      OrganisationData rootObject = JsonConvert.DeserializeObject<OrganisationData>(jsonContent);
      if (elDelete != null)  rootObject.Organisations = rootObject.Organisations.Where(el => el.Id != elDelete.Id).ToList();

      string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);

      File.WriteAllText(dataOrganisationPath, updateJsonContent);
    }

    static public void DeleteRecord(Car elDelete)
    {
      string jsonContent = File.ReadAllText(dataCarsPath);
      CarsData rootObject = JsonConvert.DeserializeObject<CarsData>(jsonContent);
      if (elDelete != null) rootObject.Cars = rootObject.Cars.Where(el => el.AutoID != elDelete.AutoID).ToList();

      string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);

      File.WriteAllText(dataCarsPath, updateJsonContent);
    }

    static public void UpdateOgranisationNumber(int newNumber)
    {
      OrganisationData orgData = new OrganisationData(); 
      Instrumental.LoadDataAboutOrganisation(ref orgData, dataOrganisationPath);
      Organisation organisation = orgData.Organisations.Find(o => o.Id == Instrumental.totalOrganisation.Id);
      if (organisation != null)
      {
        organisation.Number = newNumber;
        string updatedJson = JsonConvert.SerializeObject(orgData, Formatting.Indented);
        File.WriteAllText(dataOrganisationPath, updatedJson);
      }
    }

    static public void AddItemToJSON (Organisation org) {
      string jsonContent = File.ReadAllText(dataOrganisationPath);
      OrganisationData rootObject = JsonConvert.DeserializeObject<OrganisationData>(jsonContent);
      rootObject.Organisations.Add(org);
      File.WriteAllText(
        dataOrganisationPath,
        JsonConvert.SerializeObject(
          rootObject, Formatting.Indented
        )
      );
    }

    static public void AddItemToJSON(PointStart org)
    {
      string jsonContent = File.ReadAllText(dataPointStartPath);
      PointStartData rootObject = JsonConvert.DeserializeObject<PointStartData>(jsonContent);
      rootObject.PointStarts.Add(org);
      File.WriteAllText(
        dataPointStartPath,
        JsonConvert.SerializeObject(
          rootObject, Formatting.Indented
        )
      );
    }

    static public void AddItemToJSON (Rider rider) {
      string jsonContent = File.ReadAllText (dataRidersPath); 
      RidersData rootObject = JsonConvert.DeserializeObject <RidersData> (jsonContent); 
      rootObject.Riders.Add (rider); 
      File.WriteAllText (
        dataRidersPath,
        JsonConvert.SerializeObject (
          rootObject, Formatting.Indented
        ) 
      );
    }
    
    static public void AddItemToJSON (Car car) {
      string jsonContent = File.ReadAllText (dataCarsPath); 
      CarsData rootObject = JsonConvert.DeserializeObject <CarsData> (jsonContent); 
      rootObject.Cars.Add (car); 
      File.WriteAllText (
        dataCarsPath,
        JsonConvert.SerializeObject (
          rootObject, Formatting.Indented
        ) 
      );
    }
    
    static public void AddItemToJSON (Route route) {
      string jsonContent = File.ReadAllText (dataRoutesPath); 
      RoutesData rootObject = JsonConvert.DeserializeObject <RoutesData> (jsonContent); 
      rootObject.Routes.Add (route); 
      File.WriteAllText (
        dataRoutesPath,
        JsonConvert.SerializeObject (
          rootObject, Formatting.Indented
        ) 
      );
    }
    
    static public void AddItemToJSON (Mehanic meh) {
      string jsonContent = File.ReadAllText (dataMehanicsPath); 
      MehanicsData rootObject = JsonConvert.DeserializeObject <MehanicsData> (jsonContent); 
      rootObject.Mehanics.Add (meh); 
      File.WriteAllText (
        dataMehanicsPath,
        JsonConvert.SerializeObject (
          rootObject, Formatting.Indented
        ) 
      );
    }
    
    static public void AddItemToJSON (Dispetcher meh) {
      string jsonContent = File.ReadAllText (dataDispetchersPath); 
      DispetcherData rootObject = JsonConvert.DeserializeObject <DispetcherData> (jsonContent); 
      rootObject.Dispetchers.Add (meh); 
      File.WriteAllText (
        dataDispetchersPath,
        JsonConvert.SerializeObject (
          rootObject, Formatting.Indented
        ) 
      );
    }

    static public void UpdateItemToJSON (Mehanic newData, int idItem) {
      string jsonContent = File.ReadAllText(dataMehanicsPath);
      MehanicsData rootObject = JsonConvert.DeserializeObject<MehanicsData>(jsonContent);

      int searchId = rootObject.Mehanics.FindIndex(meh => meh.Id == idItem && meh.OrganisationId == Instrumental.totalOrganisation.Id);
      if (searchId >= 0)
      {
        rootObject.Mehanics[searchId] = newData;
        string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        File.WriteAllText(dataMehanicsPath, updateJsonContent);
      }
    }

    static public void UpdateItemToJSON(PointStart newData, int idItem)
    {
      string jsonContent = File.ReadAllText(dataPointStartPath);
      PointStartData rootObject = JsonConvert.DeserializeObject<PointStartData>(jsonContent);

      int searchId = rootObject.PointStarts.FindIndex(meh => meh.Id == idItem && meh.OrganisationId == Instrumental.totalOrganisation.Id);
      if (searchId >= 0)
      {
        rootObject.PointStarts[searchId] = newData;
        string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        File.WriteAllText(dataPointStartPath, updateJsonContent);
      }
    }

    static public void UpdateItemToJSON (Organisation newData, int idItem) {
      string jsonContent = File.ReadAllText(dataOrganisationPath); 
      OrganisationData rootObject = JsonConvert.DeserializeObject<OrganisationData>(jsonContent);
      int searchId = rootObject.Organisations.FindIndex (meh => meh.Id == idItem);
      if (searchId >= 0) rootObject.Organisations[searchId] = newData;
      string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
      File.WriteAllText(dataOrganisationPath, updateJsonContent);
    }

    static public void UpdateItemToJSON(Rider newData, int idItem)
    {
      string jsonContent = File.ReadAllText(dataRidersPath);
      RidersData rootObject = JsonConvert.DeserializeObject<RidersData>(jsonContent);

      int searchId = rootObject.Riders.FindIndex(meh => meh.Id == idItem && meh.OrganisationId == Instrumental.totalOrganisation.Id);
      if (searchId >= 0)
      {
        rootObject.Riders[searchId] = newData;
        string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        File.WriteAllText(dataRidersPath, updateJsonContent);
      }
    }

    static public void UpdateItemToJSON (Car newData, int idItem) {
      string jsonContent = File.ReadAllText(dataCarsPath);
      CarsData rootObject = JsonConvert.DeserializeObject<CarsData>(jsonContent);

      int searchId = rootObject.Cars.FindIndex(meh => meh.AutoID == idItem && meh.OrganisationID == Instrumental.totalOrganisation.Id);
      if (searchId >= 0)
      {
        rootObject.Cars[searchId] = newData;
        MessageBox.Show(searchId.ToString());
        string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        File.WriteAllText(dataCarsPath, updateJsonContent);
      }
    }

    static public void UpdateItemToJSON (Dispetcher newData, int idItem) {
      string jsonContent = File.ReadAllText(dataDispetchersPath);
      DispetcherData rootObject = JsonConvert.DeserializeObject<DispetcherData>(jsonContent);

      int searchId = rootObject.Dispetchers.FindIndex(meh => meh.Id == idItem && meh.OrganisationId == Instrumental.totalOrganisation.Id);
      if (searchId >= 0)
      {
        rootObject.Dispetchers[searchId] = newData;
        string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        File.WriteAllText(dataDispetchersPath, updateJsonContent);
      }
    }

    static public void UpdateItemToJSON (Route newData, int idItem) {
      string jsonContent = File.ReadAllText(dataRoutesPath);
      RoutesData rootObject = JsonConvert.DeserializeObject<RoutesData>(jsonContent);

      int searchId = rootObject.Routes.FindIndex(meh => meh.Id == idItem && meh.OrganisationId == Instrumental.totalOrganisation.Id);
      if (searchId >= 0)
      {
        rootObject.Routes[searchId] = newData;
        string updateJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        File.WriteAllText(dataRoutesPath, updateJsonContent);
      }
    }
  }
}
