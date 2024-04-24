using MotherProjectv01.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class RouteAdd : Form
  {
    RoutesData routesData;
    int startId; 
    public RouteAdd(ref RoutesData rD)
    {
      routesData = rD;
      InitializeComponent();
      startId = routesData.Routes.Count > 0 ? routesData.Routes.Max(route => route.Id) + 1 : 1;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      string jsonFilePath = "Data/dRoutes.json";
      // Чтение JSON файла и преобразование его в объект
      string jsonContent = File.ReadAllText(jsonFilePath);
      RoutesData rootObject = JsonConvert.DeserializeObject<RoutesData>(jsonContent);

      if (textBox1.Text != "" && textBox2.Text != "")
      {
        Route newRoute = new Route{ Id = startId, NameRoute = textBox1.Text, TimeRoute = textBox2.Text, OrganisationId = Instrumental.totalOrganisation.Id };
        startId++;

        rootObject.Routes.Add(newRoute);
        string updatedJsonContent = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
        
        File.WriteAllText(jsonFilePath, updatedJsonContent);

        MessageBox.Show("Запись была добавлена", "Успешный успех");
        textBox1.Text = "";
        textBox2.Text = "";
        Instrumental.wasUpdated = true;

      }
      else
      {
        MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
