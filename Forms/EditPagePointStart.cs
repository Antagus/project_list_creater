using MotherProjectv01.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class EditPagePointStart : Form
  {

    PointStart selectedPointStart;
    PointStartData cD; 
    int startId; 

    public EditPagePointStart()
    {
      InitializeComponent();
    }

    private void btn_Click (object sender, EventArgs e, PointStart el) {
      labelNameOperation.Text = "Редактирование пункта отправки";
      selectedPointStart = el;
      nameBox.Text = el.Name; 
      button4.Visible = false;
    }

    private void EditPagePointStart_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutPointStart(ref cD);

      button4.Visible = true;

      startId = cD.PointStarts.Count > 0 ? (cD.PointStarts.Max(rider => rider.Id) + 1) : 1;
      button4.Visible = true;


      flowLayoutPanel1.Controls.Clear();

      for (int i = 0; i != cD.PointStarts.Count; i++)
      {
        int index = i;

        Button btn = new Button();
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.FlatStyle = FlatStyle.Flat;
        btn.Font = new Font("Inter", 9);
        btn.Size = new Size(flowLayoutPanel1.Width - 30, 39);
        btn.FlatAppearance.BorderSize = 0;

        btn.Text = cD.PointStarts[index].Name;

        btn.Click += (s, args) =>
        {
          btn_Click(s, args, cD.PointStarts[index]);
        };

        flowLayoutPanel1.Controls.Add(btn);
      }
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (nameBox.Text != "")
      {
        PointStart newRider = new PointStart
        {
          Id = startId,
          Name = nameBox.Text,
          OrganisationId = Instrumental.totalOrganisation.Id
        };
        DataLib.AddItemToJSON(newRider);
        startId++;

        nameBox.Text = "";
        Instrumental.wasUpdated = true;
        EditPagePointStart_Load(this, EventArgs.Empty);
      }
      else
      {
        MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      nameBox.Text = ""; 
      button4.Visible = true;
    }

    private void deleteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      DialogResult dt = MessageBox.Show("Вы точно хотите удалить данные?", "Удалить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

      if (dt == DialogResult.Yes) {
        DataLib.DeleteRecord(selectedPointStart); 
        nameBox.Text = ""; 
        EditPagePointStart_Load(sender, e);
        button4.Visible = true;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (nameBox.Text != "") {
        DialogResult dt = MessageBox.Show("Вы точно хотите изменить данные?", "Изменить данные?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (dt == DialogResult.Yes) {
          DataLib.UpdateItemToJSON(new PointStart
          {
            Id = selectedPointStart.Id,
            Name = nameBox.Text,
            OrganisationId = selectedPointStart.OrganisationId

          }, selectedPointStart.Id);
          Instrumental.wasUpdated = true;
          nameBox.Text = ""; 
          
          EditPagePointStart_Load(this, EventArgs.Empty);
        }
      }
    }
  }
}
