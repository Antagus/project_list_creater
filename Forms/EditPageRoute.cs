using MotherProjectv01.Classes;
using System;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class EditPageRoute : Form
  {
    private RoutesData rD; 

    public EditPageRoute()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void EditPageRoute_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutRoutes(ref rD, "Data/dRoutes.json");

      comboBox1.DataSource = rD.Routes;

      comboBox1.ValueMember = "Id";
      comboBox1.DisplayMember = "NameRoute";

      comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "" && textBox2.Text != "")
      {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes)
        {
          DataLib.UpdateItemToJSON(new Route
          {
            Id = (int)comboBox1.SelectedValue,
            NameRoute = textBox1.Text,
            TimeRoute = textBox2.Text,
            OrganisationId = Instrumental.totalOrganisation.Id
          }, (int)comboBox1.SelectedValue);
          Instrumental.wasUpdated = true;
        }
      }
      else {
        MessageBox.Show("Заполните все поля корректно", "Ошибка");
      }
    }

    private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedValue != null) {
        try {
          Route t = Instrumental.getIdElement(rD, (int)comboBox1.SelectedValue);
          textBox1.Text = t.NameRoute;
          textBox2.Text = t.TimeRoute;
        } catch {
          
        }
      }
    }
  }
}
