using MotherProjectv01.Classes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class EditPage : Form
  {
    RidersData ridersData;
    CarsData carsData;
    Rider SelectedRider;

    int startId;

    public EditPage(Form1 f, ref RidersData rd, CarsData carsData)
    {
      InitializeComponent();

      ridersData = rd;
      this.carsData = carsData;
      startId = ridersData.Riders.Count > 0 ? (ridersData.Riders.Max(rider => rider.Id) + 1) : 1; 
    }

    private void EditRiderData()
    {
      if (nameBox.Text != "" && textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && carComboBox2.SelectedIndex >= 0)
      {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes)
        {
          DataLib.UpdateItemToJSON(new Rider
          {
            Id = SelectedRider.Id,
            Name = nameBox.Text,
            Snils = textBox1.Text,
            Document = textBox2.Text,
            Class = "",
            AutoID = (int)carComboBox2.SelectedValue,
            DateGet = textBox3.Text,
            TabelNumber = textBox4.Text,
            OrganisationId = Instrumental.totalOrganisation.Id
          },
          SelectedRider.Id);
          Instrumental.wasUpdated = true;

          ClearAllTextBox();
          EditPage_Load(this, EventArgs.Empty);
        }
      } else {
        MessageBox.Show ("Поля заполнены не корректно", "Некорректно заполнены поля для редактирования", MessageBoxButtons.OK, MessageBoxIcon.Error);  
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      EditRiderData();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn_Click(object sender, EventArgs e, Rider rider)
    {
      labelNameOperation.Text = "Редактирование водителя";
      button4.Visible = false;
      SelectedRider = rider;

      nameBox.Text = rider.Name;
      textBox1.Text = rider.Snils;
      textBox2.Text = rider.Document;
      textBox3.Text = rider.DateGet;
      textBox4.Text = rider.TabelNumber;

      carComboBox2.SelectedValue = rider.AutoID;

    }


    private void btn3_CreateNewRider(object sender, EventArgs e)
    {
      if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" &&
          nameBox.Text != "" && carComboBox2.SelectedIndex >= 0)
      {
        Rider newRider = new Rider
        {
          Id = startId, 
          Name = nameBox.Text,
          Snils = textBox1.Text,
          Document = textBox2.Text,
          Class = "",
          AutoID = (int)carComboBox2.SelectedValue,
          DateGet = textBox3.Text,
          TabelNumber = textBox4.Text,
          OrganisationId = Instrumental.totalOrganisation.Id

        };
        DataLib.AddItemToJSON(newRider);
        startId++;

        ClearAllTextBox();
        Instrumental.wasUpdated = true;
        EditPage_Load(this, EventArgs.Empty);

      }
      else
      {
        MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void EditPage_Load(object sender, EventArgs e)
    {

      Instrumental.LoadDataAboutRiders(ref ridersData, "Data/dRiders.json");
      button4.Visible = true;

      button4.Click += btn3_CreateNewRider;

      carComboBox2.DataSource = carsData.Cars;
      carComboBox2.ValueMember = "AutoID";
      carComboBox2.DisplayMember = "NumberAuto";

      flowLayoutPanel1.Controls.Clear();

      for (int i = 0; i != ridersData.Riders.Count; i++)
      {
        int index = i;

        Button btn = new Button();
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new System.Drawing.Font("Inter", 9);
        btn.Size = new System.Drawing.Size(flowLayoutPanel1.Width - 30, 39);
        btn.FlatAppearance.BorderSize = 0;

        btn.Text = ridersData.Riders[i].Name;

        btn.Click += (s, args) => {
          btn_Click(s, args, ridersData.Riders[index]);
        };

        flowLayoutPanel1.Controls.Add(btn);
      }
    }

    private void ClearAllTextBox () {
      nameBox.Text = "";
      textBox1.Text = "";
      textBox2.Text = "";
      textBox3.Text = "";
      textBox4.Text = "";
    }

    private void button3_Click(object sender, EventArgs e)
    {
      labelNameOperation.Text = "Добавление водителя";
      button4.Visible = true;
      ClearAllTextBox();
    }

    private void deleteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      DataLib.DeleteRecord(SelectedRider);
      ClearAllTextBox();
      EditPage_Load(sender, e);
    }

    private void button4_Click(object sender, EventArgs e)
    {

    }
  }
}
