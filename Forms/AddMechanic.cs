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
  public partial class AddMechanic : Form
  {
    MehanicsData dispetcherData;
    int startId;

    public AddMechanic(ref MehanicsData dp)
    {
      InitializeComponent();
      dispetcherData = dp;
      startId = dispetcherData.Mehanics.Count > 0 ? dispetcherData.Mehanics.Max(rider => rider.Id) + 1 : 1;
    }


    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "")
      {
        // Создание нового Rider
        Mehanic newRider = new Mehanic
        {
          Id = startId,
          Name = textBox1.Text,
          OrganisationId = Instrumental.totalOrganisation.Id
        };
        DataLib.AddItemToJSON(newRider);
        startId++;
        textBox1.Text = "";
        MessageBox.Show("Запись добавлена", "Успех");

        Instrumental.wasUpdatedMehanicsOrDispetchers = true;
      }
      else
      {
        MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
