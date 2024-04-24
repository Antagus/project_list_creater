using MotherProjectv01.Classes;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MotherProjectv01
{
  public partial class ImageShow : Form
  {
    static Image image; 
    static Journal journal;
    bool statusRewrite = false;

    public ImageShow(Image i, Journal save, bool statusRewrite)
    {
      InitializeComponent();
      image = i;
      journal = save;
      this.statusRewrite = statusRewrite;
    }
    static int currentPage = 0;

    private static void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
      // Здесь вы можете настроить печать изображений на обеих сторонах страницы
      Graphics graphics = e.Graphics;
      // Загрузка первого изображения
      Image image1 = image;
      // Загрузка второго изображения
      Image image2 = Image.FromFile("Image/list2.png");
      // Отрисовка второго изображения на обратной стороне без ограничения размеров
      graphics.DrawImage(image2, -20, -20, e.PageBounds.Width, e.PageBounds.Height);
      e.HasMorePages = currentPage < 1; // Печатаем две страницы
      currentPage++;
      // Если это вторая страница, рисуем второе изображение
      if (currentPage == 1)
      {
        graphics.DrawImage(image1, -20, -10, e.PageBounds.Width, e.PageBounds.Height);

      }
      image1.Dispose();
      image2.Dispose();
      // Завершение печати
    }

    private void ImageShow_Load(object sender, EventArgs e)
    {
      pictureBox1.Image = image;
      if (statusRewrite) {
        label1.Text = "Данный лист будет перезаписан,\nномер обновлен не будет!!!";
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      // Получение списка доступных принтеров
      PrinterSettings.StringCollection printerList = PrinterSettings.InstalledPrinters;

      if (printerList.Count == 0)
      {
        MessageBox.Show("Не видит принтер!");
        return;
      }
      // Выбор первого доступного принтера
      string selectedPrinter = printerList[0];
      // Создание объекта для печати
      PrintDocument printDocument = new PrintDocument();
      // Установка выбранного принтера
      printDocument.PrinterSettings.PrinterName = selectedPrinter;
      // Установка обработчика события PrintPage для печати
      printDocument.PrintPage += new PrintPageEventHandler(PrintPageHandler);
      printDocument.DefaultPageSettings.Landscape = true;
      printDocument.PrinterSettings.Duplex = Duplex.Default;
      // Начало печати
      printDocument.Print();
      currentPage = 0;
      image.Dispose();


      Instrumental.SaveJournalSave(journal,  statusRewrite);
      Instrumental.wasUpdated = true;
      this.Close();

      
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void button5_Click(object sender, EventArgs e)
    {
      image.Dispose();  
      this.Close(); 
    }
  }
}
