using MotherProjectv01.Classes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class CarAdd : Form
  {
    CarsData carsData;
    int startId;
    public CarAdd(ref CarsData c)
    {
      carsData = c;
      InitializeComponent();
      startId = carsData.Cars.Count > 0 ? carsData.Cars.Max(car => car.AutoID) + 1 : 1;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (nameBox.Text != "" && textBox1.Text != "")
      {
        Car newCar = new Car
        {
          AutoID = startId,
          MarkAuto = nameBox.Text,
          NumberAuto = textBox1.Text,
          OrganisationID = Instrumental.totalOrganisation.Id
        };

        DataLib.AddItemToJSON(newCar);
        startId++;
        MessageBox.Show("Запись была добавлена", "Успешный успех");
        nameBox.Text = "";
        textBox1.Text = "";
        Instrumental.wasUpdated = true;

      }
      else
      {
        MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
