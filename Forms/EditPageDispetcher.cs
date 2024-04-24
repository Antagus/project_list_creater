using MotherProjectv01.Classes;
using System;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class EditPageDispetcher : Form
  {
    private DispetcherData dD; 
    public EditPageDispetcher()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (textBox1.Text != "")
      {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes)
        {
          DataLib.UpdateItemToJSON(new Dispetcher
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

    private void EditPageDispetcher_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutDispetcher (ref dD, "Data/dDispetchers.json");

      comboBox1.DataSource = dD.Dispetchers;

      comboBox1.ValueMember = "Id";
      comboBox1.DisplayMember = "Name";

      comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedValue != null)
      {
        try
        {
          Dispetcher t = Instrumental.getIdElement(dD, (int)comboBox1.SelectedValue);
          textBox1.Text = t.Name;
        }
        catch
        {

        }
      }
    }
  }
}
