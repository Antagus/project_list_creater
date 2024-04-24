using MotherProjectv01.Classes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class AddDispetcher : Form
  {
    DispetcherData dispetcherData;
    int startId; 

    public AddDispetcher(ref DispetcherData dp)
    {
      InitializeComponent();
      dispetcherData = dp;
      startId = dispetcherData.Dispetchers.Count > 0 ? dispetcherData.Dispetchers.Max(rider => rider.Id) + 1 : 1;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "")
      {
        // Создание нового Rider
        Dispetcher newRider = new Dispetcher
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

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
