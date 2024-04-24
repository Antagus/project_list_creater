using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01.Classes
{
  public class Instrumental
  {
    static public bool wasUpdated = false;
    static public Journal journalSave = null;
    static public Organisation totalOrganisation = null;
    static public bool wasUpdatedMehanicsOrDispetchers = false;

    public class JsonData
    {
      public int Number { get; set; }
    }

    public static void SaveDispetcherId(string jsonFilePath, SaveData data, int newDispetcherId)
    {
      string jsonContent = File.ReadAllText(jsonFilePath);
      data = JsonConvert.DeserializeObject<SaveData>(jsonContent);
      data.DispetcherId = newDispetcherId;
      string updatedJsonContent = JsonConvert.SerializeObject(data, Formatting.Indented);
      File.WriteAllText(jsonFilePath, updatedJsonContent);
    }

    public static void SaveMehanicId (string jsonFilePath, SaveData data, int newMehanicId){
      string jsonContent = File.ReadAllText(jsonFilePath);
      data = JsonConvert.DeserializeObject<SaveData>(jsonContent);
      data.MehanicId = newMehanicId;
      string updatedJsonContent = JsonConvert.SerializeObject(data, Formatting.Indented);
      File.WriteAllText(jsonFilePath, updatedJsonContent);
    }
  
    static public int ReadNumberFromJsonFile(string filePath)
    {
      if (File.Exists(filePath))
      {
        string json = File.ReadAllText(filePath);
        JsonData data = JsonConvert.DeserializeObject<JsonData>(json);
        return data.Number;
      }
      else
      {
        // Обработка ошибки, если файл не существует
        throw new FileNotFoundException("JSON file not found.");
      }
    }

    static public void WriteNumberToJsonFile(string filePath, int newNumber)
    {
      JsonData data = new JsonData { Number = newNumber };
      string json = JsonConvert.SerializeObject(data);
      File.WriteAllText(filePath, json);
    }

    static public void SaveJournalSave(Journal journal, bool statusRewrite)
    {
      string jsonFilePath = "Data/dJournal.json";
      string json = File.ReadAllText(jsonFilePath);
      JournalData journalData = JsonConvert.DeserializeObject<JournalData>(json);
      MessageBox.Show(statusRewrite.ToString());
      if (!statusRewrite) {
        journalData.Records.Add(journal);
        DataLib.UpdateOgranisationNumber(DataLib.GetOrganisationNumber() + 1);
      } else {
        int indexRewrite = journalData.Records.FindIndex(item => item.Number == journal.Number);
        journalData.Records[indexRewrite] = journal;
      }

      string updatedJsonContent = JsonConvert.SerializeObject(journalData, Formatting.Indented);
      File.WriteAllText(jsonFilePath, updatedJsonContent);
    }

  static public Organisation getIdElement (OrganisationData orgData, int Id) {
    foreach (Organisation org in orgData.Organisations) {
      if (org.Id == Id) return org;
    } 
    return null;
  }

    static public PointStart getIdElement(PointStartData orgData, int Id)
    {
      foreach (PointStart org in orgData.PointStarts)
      {
        if (org.Id == Id) return org;
      }
      return null;
    }

    static public Car getIdElement (CarsData Data, int ID) {
      foreach (Car car in Data.Cars) {
        if (car.AutoID == ID) {
          return car;
        }
      }
      return null;
    }

    static public Rider getIdElement (RidersData Data, int ID) {
      foreach (Rider rider in Data.Riders) {
        if (rider.Id == ID) {
          return rider;
        }
      }
      return null;
    }

    static public Mehanic getIdElement(MehanicsData Data, int ID)
    {
      foreach (Mehanic rider in Data.Mehanics)
      {
        if (rider.Id == ID)
        {
          return rider;
        }
      }
      return null;
    }

    static public Route getIdElement (RoutesData Data, int ID) {
      foreach (Route car in Data.Routes)
      {
        if (car.Id == ID)
        {
          return car;
        }
      }
      return null;
    }

    static public Dispetcher getIdElement(DispetcherData Data, int ID)
    {
      foreach (Dispetcher car in Data.Dispetchers)
      {
        if (car.Id == ID)
        {
          return car;
        }
      }
      return null;
    }

    static public Journal getIdElement(JournalData Data, int ID)
    {
      foreach (Journal car in Data.Records)
      {
        if (car.Number == ID)
        {
          return car;
        }
      }
      return null;
    }

    static public void LoadDataAboutSave (ref SaveData data) {
      string path = "Data/dSave.json";
      if (!File.Exists(path))
      {
        MessageBox.Show("Ошибка загрузки данных сохранения", "Ошибка загрузки");
      } else {
        string jsonContent = File.ReadAllText(path);
        data = JsonConvert.DeserializeObject<SaveData>(jsonContent);
      }
    }

    static public void LoadDataAboutPointStart (ref PointStartData pointStarts) {
      if (File.Exists("Data/dPointStart.json"))
      {
        try
        {
          string jsonData = File.ReadAllText("Data/dPointStart.json");
          pointStarts = JsonConvert.DeserializeObject<PointStartData>(jsonData);
          pointStarts.PointStarts = pointStarts.PointStarts.Where(el => el.OrganisationId == totalOrganisation.Id).ToList();
        }
        catch (JsonException e)
        {
          MessageBox.Show(e.Message);
        }
      }
      else
      {
        MessageBox.Show("Приложение сдохло, файл с пунктами отправки не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    static public void LoadDataAboutCars(ref CarsData carsData, string dataCarsStoragePath)
    {
      if (File.Exists(dataCarsStoragePath))
      {
        try
        {
          string jsonData = File.ReadAllText(dataCarsStoragePath);
          carsData = JsonConvert.DeserializeObject<CarsData>(jsonData);
          carsData.Cars = carsData.Cars.Where(id => id.OrganisationID == totalOrganisation.Id).ToList();
        }
        catch (JsonException e)
        {
          MessageBox.Show(e.Message);
        }
      }
      else
      {
        MessageBox.Show("Приложение сдохло, файл с автомобилями не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
    static public void LoadDataAboutRiders(ref RidersData ridersData, string dataRidersStoragePath)
    {
      if (File.Exists(dataRidersStoragePath))
      {
        try
        {
          string jsonData = File.ReadAllText(dataRidersStoragePath);
          ridersData = JsonConvert.DeserializeObject<RidersData>(jsonData);
          ridersData.Riders = ridersData.Riders.Where(el => el.OrganisationId == totalOrganisation.Id).ToList();
        }
        catch (JsonException e)
        {
          MessageBox.Show(e.Message);
        }
      }
      else
      {
        MessageBox.Show("Приложение сдохло, файл с водителями не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    static public void LoadDataAboutOrganisation(ref OrganisationData orgData, string dataRidersStoragePath)
    {
      try
      {
        if (File.Exists(dataRidersStoragePath))
        {
          string jsonData = File.ReadAllText(dataRidersStoragePath);
          orgData = JsonConvert.DeserializeObject<OrganisationData>(jsonData);

          if (orgData == null)
          {
            orgData = new OrganisationData(); // Инициализация объекта, если десериализация не удалась
          }
        }
        else
        {
          MessageBox.Show("Приложение сдохло, файл с организациями не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (JsonException e)
      {
        MessageBox.Show(e.Message);
        orgData = new OrganisationData(); // Инициализация объекта в случае исключения
      }
    }

    static public void LoadDataAboutRoutes(ref RoutesData routesData, string dataRidersStoragePath)
    {
      if (File.Exists(dataRidersStoragePath))
      {
        try
        {
          string jsonData = File.ReadAllText(dataRidersStoragePath);
          routesData = JsonConvert.DeserializeObject<RoutesData>(jsonData);
          routesData.Routes = routesData.Routes.Where(org => org.OrganisationId == totalOrganisation.Id).ToList();
        }
        catch (JsonException e)
        {
          MessageBox.Show(e.Message);
        }
      }
      else
      {
        MessageBox.Show("Приложение сдохло, файл с маршрутами не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    static public void LoadDataAboutMehanics(ref MehanicsData mehanicsData, string dataMehincsStoragePath) {
      if (File.Exists(dataMehincsStoragePath))
      {
        try
        {
          string jsonData = File.ReadAllText(dataMehincsStoragePath);
          mehanicsData = JsonConvert.DeserializeObject<MehanicsData>(jsonData);
          mehanicsData.Mehanics = mehanicsData.Mehanics.Where(el => el.OrganisationId == totalOrganisation.Id).ToList();
        }
        catch (JsonException e)
        {
          MessageBox.Show(e.Message);
        }
      }
      else
      {
        MessageBox.Show("Приложение сдохло, файл с механиками не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    } 

    static public void LoadDataAboutDispetcher (ref DispetcherData dispetcherData, string dataDispetcherStoragePath) {
      if (File.Exists(dataDispetcherStoragePath))
      {
        try
        {
          string jsonData = File.ReadAllText(dataDispetcherStoragePath);
          dispetcherData = JsonConvert.DeserializeObject<DispetcherData>(jsonData);
          dispetcherData.Dispetchers = dispetcherData.Dispetchers.Where(el => el.OrganisationId == totalOrganisation.Id).ToList();
        }
        catch (JsonException e)
        {
          MessageBox.Show(e.Message);
        }
      }
      else
      {
        MessageBox.Show("Приложение сдохло, файл с диспетчерами не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    static public void LoadDataAboutJournal (ref JournalData data) {
        if (File.Exists("Data/dJournal.json"))
        {
          try
          {
            string jsonData = File.ReadAllText("Data/dJournal.json");
            data = JsonConvert.DeserializeObject<JournalData>(jsonData);
          data.Records = data.Records.Where(el => el.OrganisationId == totalOrganisation.Id).ToList();
          }
          catch (JsonException e)
          {
            MessageBox.Show(e.Message);
          }
        }
        else
        {
          MessageBox.Show("Приложение сдохло, файл с журналом не открывается", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }

  }
}
