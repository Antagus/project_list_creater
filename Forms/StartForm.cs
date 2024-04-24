using MotherProjectv01.Classes;
using System;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class StartForm : Form
  {
    OrganisationData orgData; 
     
    public StartForm()
    {
      InitializeComponent();
    }

    private void CloseApplicationSecure () {

    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      AddOrganisationPage form = new AddOrganisationPage();
      form.ShowDialog();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (comboBox1.SelectedIndex != -1) {
        Form1 form = new Form1();
        form.Show();
        this.Hide();
      } else {
        MessageBox.Show ("Не выбрана организация", "Ошибка органиазации", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void StartForm_Load(object sender, EventArgs e)
    {
      CloseApplicationSecure();

      Instrumental.LoadDataAboutOrganisation(ref orgData, "Data/dOrganisation.json");
      comboBox1.DataSource = orgData.Organisations;

      comboBox1.ValueMember = "Id"; 
      comboBox1.DisplayMember = "NameCompany";
      comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    }

    private void StartForm_Activated(object sender, EventArgs e)
    {
      if (Instrumental.wasUpdated)
      {
        Instrumental.LoadDataAboutOrganisation(ref orgData, "Data/dOrganisation.json");
        comboBox1.DataSource = orgData.Organisations;
        Instrumental.wasUpdated = false; 
      }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedIndex != -1) {
        try {
          Instrumental.totalOrganisation = Instrumental.getIdElement(orgData, (int)comboBox1.SelectedValue);
        } catch {
          
        }
      }
    }
  }
}
