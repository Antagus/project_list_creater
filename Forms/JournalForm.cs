using MotherProjectv01.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class JournalForm : Form
  {
    private JournalData Data;
    private List<Journal> jr;

    public JournalForm()
    {
      InitializeComponent();
    }

    private void JournalForm_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutJournal(ref Data);
      jr = this.Data.Records.OrderByDescending(journal => journal.Number).ToList();
      jr = jr.Where(el => el.OrganisationId == Instrumental.totalOrganisation.Id).ToList();
      dataGridView1.DataSource = jr;
      dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
      dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      dataGridView1.CellDoubleClick += dataGridView_CellClick;

    }

    private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        dataGridView1.ClearSelection(); // Сначала сбросьте выделение всех строк
        // Выделите всю строку, в которой находится кликнутая ячейка
        dataGridView1.Rows[e.RowIndex].Selected = true;

        if (dataGridView1.SelectedRows.Count > 0)
        {
          DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
          // Укажите название столбца, из которого вы хотите получить значение
          int columnIndex = dataGridView1.Columns["number"].Index;
          int selectedNumberJournal = (int)selectedRow.Cells[columnIndex].Value;
          DialogResult dt = MessageBox.Show("Желаете скопировать данный лист", "Скопировать лист", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
          if (dt == DialogResult.Yes)
          {
            Instrumental.journalSave = Instrumental.getIdElement(Data, selectedNumberJournal);
            this.Close();
          } 
        }
      }
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      try {
        int search = Convert.ToInt32(textBox1.Text);
        dataGridView1.DataSource = this.jr.Where(rec => rec.Number == search).ToList();
      } 
      catch
      {
        dataGridView1.DataSource = this.jr.Where(rec => rec.NameRider.Contains(textBox1.Text)).ToList();
      }
    }

    private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right) { 
        dataGridView1.ClearSelection(); // Сначала сбросьте выделение всех строк
        dataGridView1.Rows[e.RowIndex].Selected = true;
        if (dataGridView1.SelectedRows.Count > 0)
        {
          DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
          // Укажите название столбца, из которого вы хотите получить значение
          int columnIndex = dataGridView1.Columns["number"].Index;
          DialogResult dt = MessageBox.Show("Удалить данную запись?", "Удалить лист", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

          if (dt == DialogResult.Yes)
          {
            Data.Records = Data.Records.Where(item => item.Number != (int)selectedRow.Cells[columnIndex].Value).ToList();
            string jsonFilePath = "Data/dJournal.json";
            string updatedJsonContent = JsonConvert.SerializeObject(Data, Formatting.Indented);
            File.WriteAllText(jsonFilePath, updatedJsonContent);
            JournalForm_Load(this, EventArgs.Empty);
          }
        }
      }
    }
  }
}
