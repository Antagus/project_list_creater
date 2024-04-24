using MotherProjectv01.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class EditPageCar : Form
  {

    private CarsData cD;
    private int startId;
    private Car selectedCar; 

    public EditPageCar()
    {
      InitializeComponent();
    }


    private void btn_Click (object sender, EventArgs e, Car car) {
      labelNameOperation.Text = "Редактирование автомобиля";

      nameBox.Text = car.MarkAuto;
      textBox1.Text = car.NumberAuto;
      selectedCar = car;
      button4.Visible = false;
    }

    private void EditPageCar_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutCars(ref cD, "Data/dCars.json");

      startId = cD.Cars.Count > 0 ? (cD.Cars.Max(rider => rider.AutoID) + 1) : 1;
      button4.Visible = true;


      flowLayoutPanel1.Controls.Clear();

      for (int i = 0; i != cD.Cars.Count; i++)
      {
        int index = i;

        Button btn = new Button();
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new Font("Inter", 9);
        btn.Size = new Size(flowLayoutPanel1.Width - 30, 39);
        btn.FlatAppearance.BorderSize = 0;

        btn.Text = cD.Cars[index].MarkAuto + " : " + cD.Cars[index].NumberAuto;

        btn.Click += (s, args) =>
        {
          btn_Click(s, args, cD.Cars[index]);
        };

        flowLayoutPanel1.Controls.Add(btn);
      }

    }

    private void ClearAllTextBox () {
      nameBox.Text = "";
      textBox1.Text = "";
    }


    private void button3_Click(object sender, EventArgs e)
    {
      labelNameOperation.Text = "Добавление автомобиля";
      ClearAllTextBox();
      button4.Visible = true;
    }

    private void deleteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {

      DialogResult dt = MessageBox.Show("Вы точно хотите удалить данные?", "Удалить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

      if (dt == DialogResult.Yes) {
        DataLib.DeleteRecord(selectedCar);
        ClearAllTextBox();
        EditPageCar_Load(sender, e);
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "" && nameBox.Text != "")
      {
        Car car = new Car
        {
          AutoID = startId,
          MarkAuto = nameBox.Text,
          NumberAuto = textBox1.Text,
          OrganisationID = Instrumental.totalOrganisation.Id
        };
        DataLib.AddItemToJSON(car);
        startId++;
        Instrumental.wasUpdated = true;
        ClearAllTextBox();
        EditPageCar_Load(this, EventArgs.Empty);
      }
      else
      {
        MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "" && nameBox.Text != "") {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes) {
          DataLib.UpdateItemToJSON(new Car
          {
            AutoID = selectedCar.AutoID,
            MarkAuto = nameBox.Text,
            NumberAuto = textBox1.Text,
            OrganisationID = selectedCar.OrganisationID

          }, selectedCar.AutoID);
          Instrumental.wasUpdated = true;
          ClearAllTextBox(); 
          
          EditPageCar_Load(this, EventArgs.Empty);
        }
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
