using MotherProjectv01.Classes;
using System;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class EditPageMehanic : Form
  {
    private MehanicsData mehanicsData;

    public EditPageMehanic()
    {
      InitializeComponent();
    }
    
    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "")
      {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes)
        {
          DataLib.UpdateItemToJSON(new Mehanic
          {
            Id = (int)comboBox1.SelectedValue,
            Name = textBox1.Text,
            OrganisationId = Instrumental.totalOrganisation.Id
          }, (int)comboBox1.SelectedValue);
          Instrumental.wasUpdatedMehanicsOrDispetchers = true;
          this.Close();
        }
      }
      else
      {
        MessageBox.Show("Заполните все поля корректно", "Ошибка");
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void EditPageMehanic_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutMehanics(ref mehanicsData, "Data/dMechanic.json");
      comboBox1.DataSource = mehanicsData.Mehanics;
      comboBox1.ValueMember = "Id";
      comboBox1.DisplayMember = "Name";
      comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    }

    private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
    {

    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedValue != null)
      {
        try
        {
          Mehanic t = Instrumental.getIdElement(mehanicsData, (int)comboBox1.SelectedValue);
          textBox1.Text = t.Name;
        }
        catch
        {

        }
      }
    }
  }
}
