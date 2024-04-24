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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MotherProjectv01
{
  public partial class EditPageRider : Form
  {
    public EditPageRider()
    {
      InitializeComponent();
    }

    private RidersData rD;
    private CarsData cD;

    private void EditPageRider_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutRiders(ref rD, "Data/dRiders.json");
      Instrumental.LoadDataAboutCars(ref cD, "Data/dCars.json");

      comboBox1.DataSource = rD.Riders;
      comboBox2.DataSource = cD.Cars;
      
      comboBox1.ValueMember = "Id";
      comboBox1.DisplayMember = "Name";
      comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

      comboBox2.ValueMember = "AutoID";
      comboBox2.DisplayMember = "NumberAuto";
      comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

      comboBox1_SelectedValueChanged(comboBox1, EventArgs.Empty);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "" && 
          textBox2.Text != "" && 
          textBox3.Text != "" && 
          comboBox1.SelectedIndex >= 0 &&
          comboBox2.SelectedIndex >= 0)
      {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes) {
          DataLib.UpdateItemToJSON(new Rider
          {
            Id = (int)comboBox1.SelectedValue,
            Snils = textBox1.Text,
            Document = textBox2.Text,
            DateGet = textBox3.Text,
            Name = textBox4.Text,
            TabelNumber = textBox5.Text,
            AutoID = (int)comboBox2.SelectedValue,
            OrganisationId = Instrumental.totalOrganisation.Id
          },
          (int)comboBox1.SelectedValue);
          Instrumental.wasUpdated = true;
        }
      }
      }

      private void button1_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedIndex != -1)
      {
        try
        {
          int s = (int)comboBox1.SelectedValue;

          Rider total = Instrumental.getIdElement(rD, s);

          textBox4.Text = total.Name;
          textBox3.Text = total.DateGet;
          textBox1.Text = total.Snils;
          textBox2.Text = total.Document;
          textBox5.Text = total.TabelNumber;
          comboBox2.SelectedValue = total.AutoID;
        }
        catch (Exception ex)
        {
        }
      }
    }
  }
}
