using MotherProjectv01.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// СДЕЛАЛ Гусев Анатолий Михайлович

namespace MotherProjectv01
{
  public partial class AddOrganisationPage : Form
  {
    OrganisationData org = new OrganisationData();
    Organisation selectedOrganisation; 

    int startId; 

    public AddOrganisationPage()
    {
      InitializeComponent();
      Instrumental.LoadDataAboutOrganisation(ref org, "Data/dOrganisation.json");
      startId = org.Organisations.Count > 0 ? org.Organisations.Max(car => car.Id) + 1 : 1;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btn4_editOrganisation (object sender, EventArgs e) {
      if (textBox8.Text != "" && textBox1.Text != "" && 
          textBox3.Text != "" && textBox4.Text != "" && 
          textBox7.Text != "" && textBox2.Text != "" &&
          textBox9.Text != "" && textBox5.Text != "")
      {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes)
        {
          DataLib.UpdateItemToJSON(new Organisation
          {
            Id = startId,
            Name = textBox1.Text + " " + textBox5.Text + ", ИНН " + textBox3.Text + ", ОГРН " + textBox4.Text + ", ТЕЛ. " + textBox2.Text,
            NameCompany = textBox1.Text,
            Item = textBox6.Text,
            PointStart = textBox7.Text,
            OKPO = textBox8.Text,
            OKOPF = textBox9.Text,

            NumberPhone = textBox2.Text,
            INN = textBox3.Text,
            OGRN = textBox4.Text,
            Address = textBox5.Text,
          },
          selectedOrganisation.Id);
          Instrumental.wasUpdated = true;
          AddOrganisationPage_Load(this, EventArgs.Empty);
        }
      } 
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text.Length > 3 && textBox2.Text.Length > 10 && textBox3.Text.Length > 3 && textBox4.Text.Length > 5 
      && textBox5.Text.Length > 5) {


        Organisation org = new Organisation
        {
          Id = startId,
          Name = textBox1.Text + " " + textBox5.Text + ", ИНН " + textBox3.Text + ", ОГРН " + textBox4.Text + ", ТЕЛ. " + textBox2.Text,
          NameCompany = textBox1.Text,
          Item = textBox6.Text,
          PointStart = textBox7.Text,
          OKPO = textBox8.Text,
          OKOPF = textBox9.Text,

          NumberPhone = textBox2.Text,
          INN = textBox3.Text, 
          OGRN = textBox4.Text,
          Address = textBox5.Text,
        };

        DataLib.AddItemToJSON(org);
        startId++;
        Instrumental.wasUpdated = true;
        AddOrganisationPage_Load(sender, e);
        ClearAllTextBox();
      }
      else
      {
        MessageBox.Show("Проверьте корректность заполнения всех полей", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void btn_Click(object sender, EventArgs e, Organisation organisation)
    {
      labelNameOperation.Text = "Редактирование организации";

      selectedOrganisation = organisation;

      textBox8.Text = selectedOrganisation.OKPO;
      textBox9.Text = selectedOrganisation.OKOPF;
      textBox1.Text = selectedOrganisation.NameCompany;
      
      textBox2.Text = selectedOrganisation.NumberPhone;
      textBox3.Text = selectedOrganisation.INN;
      textBox4.Text = selectedOrganisation.OGRN;
      textBox5.Text = selectedOrganisation.Address;
      textBox6.Text = selectedOrganisation.Item;
      textBox7.Text = selectedOrganisation.PointStart;

      button4.Visible = false;
    }

    private void AddOrganisationPage_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutOrganisation(ref org, "Data/dOrganisation.json");

      flowLayoutPanel1.Controls.Clear();

      for (int i = 0; i != org.Organisations.Count; i++)
      {
        int index = i;

        System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new System.Drawing.Font("Inter", 9);
        btn.Size = new System.Drawing.Size(flowLayoutPanel1.Width - 30, 39);
        btn.FlatAppearance.BorderSize = 0;

        btn.Text = org.Organisations[index].Name;

        btn.Click += (s, args) => {
          btn_Click(s, args, org.Organisations[index]);
        };

        flowLayoutPanel1.Controls.Add(btn);
      }
    }

    private void ClearAllTextBox()
    {
      textBox1.Text = "";
      textBox2.Text = "";
      textBox3.Text = "";
      textBox4.Text = "";
      textBox5.Text = "";
      textBox6.Text = "";
      textBox7.Text = "";
      textBox8.Text = "";
      textBox9.Text = "";
    }

    private void button3_Click(object sender, EventArgs e)
    {
      labelNameOperation.Text = "Добавить организации";
      ClearAllTextBox();
    }

    private void deleteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      DataLib.DeleteRecord(selectedOrganisation);
      ClearAllTextBox();
      AddOrganisationPage_Load(sender, e);
    }

    private void button2_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
