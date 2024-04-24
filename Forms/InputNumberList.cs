using MotherProjectv01.Classes;
using System;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class InputNumberList : Form
  {
    public InputNumberList()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DataLib.UpdateOgranisationNumber(Convert.ToInt32(textBox1.Text));
      Instrumental.wasUpdated = true;
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
