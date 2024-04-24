namespace MotherProjectv01
{
  partial class ImageShow
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      this.button5 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(1117, 666);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.BackColor = System.Drawing.Color.White;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.button1.Location = new System.Drawing.Point(958, 12);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(147, 32);
      this.button1.TabIndex = 1;
      this.button1.Text = "Печать";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button4
      // 
      this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button4.BackColor = System.Drawing.Color.White;
      this.button4.Location = new System.Drawing.Point(980, 12);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(125, 32);
      this.button4.TabIndex = 7;
      this.button4.Text = "Печать";
      this.button4.UseVisualStyleBackColor = false;
      // 
      // pictureBox4
      // 
      this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox4.Location = new System.Drawing.Point(0, 0);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(1117, 666);
      this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox4.TabIndex = 6;
      this.pictureBox4.TabStop = false;
      // 
      // button5
      // 
      this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.button5.BackColor = System.Drawing.Color.White;
      this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.button5.Location = new System.Drawing.Point(958, 50);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(147, 33);
      this.button5.TabIndex = 8;
      this.button5.Text = "Отмена";
      this.button5.UseVisualStyleBackColor = false;
      this.button5.Click += new System.EventHandler(this.button5_Click);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(907, 107);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(0, 16);
      this.label1.TabIndex = 9;
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ImageShow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1117, 666);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button5);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.pictureBox4);
      this.Name = "ImageShow";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Предпросмотр и печать";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.ImageShow_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Label label1;
  }
}