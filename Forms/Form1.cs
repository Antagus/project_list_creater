using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MotherProjectv01.Classes;

namespace MotherProjectv01
{
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    // TODO:
    // 1. Сделать возможность выбрать товар

    private readonly string dataRidersStoragePath = "Data/dRiders.json";
    private readonly string dataCarsStoragePath = "Data/dCars.json";
    private readonly string dataRoutesStoragePath = "Data/dRoutes.json";
    private readonly string dataPointStartStoragePath = "Data/dPointStart.json";

    private string imageListPath1 = "Image/list1.png";
    
    private int selectedIndexRiders = -1;
    private int selectedIndexCars = -1;
    private int selectedIndexRoutes = -1;
    private int selectedPointStart;
    private Car totalCar;
    private Rider totalRider;
    private Route totalRoute;
    private Mehanic totalMehanic;
    private Dispetcher totalDispetcher;
    private PointStart totalPointStart;

    RidersData ridersData;
    CarsData carsData;
    RoutesData routesData;
    MehanicsData mehanicsData;
    DispetcherData dispetcherData;
    JournalData journalData;
    PointStartData pointData; 

    Font font = new Font("Times New Roman", 9);
    Font small = new Font("Times New Roman", 6);

    Font bold = new Font("Times New Roman", 10, FontStyle.Bold);
    Brush brush = new SolidBrush(Color.Black); // Цвет текста

    static private string[] allMonth = {
      "января",
      "февраля", 
      "марта", 
      "апреля",
      "мая", 
      "июня",
      "июля",
      "августа",
      "сентября",
      "октября",
      "ноября",
      "декабря"
    };

    public void DrawTextOnPoint (Graphics graphics, PointF point, string text) {
      graphics.DrawString(text, font, brush, point);
    }

    private string TimeDifferent (string time, int dmin) {
      string[] t = time.Split(':');

      int h = int.Parse(t[0]);
      int minutes = int.Parse(t[1]);

      if (t.Length == 2) {
        if (minutes - dmin < 0)
        {
          minutes = 60 + (minutes - dmin);
          h -= 1;
        } 
        else {
          minutes -= dmin;
        }
      }

      if (minutes < 10)
      {
        return h.ToString() + ":" + "0" + minutes.ToString();
      }
      return h.ToString() + ":" + minutes.ToString();

    }

    static string[] SplitString(string input, int substringLength)
    {
      int length = input.Length;
      int numSubstrings = (int)Math.Ceiling((double)length / substringLength);
      string[] substrings = new string[numSubstrings];

      for (int i = 0; i < numSubstrings; i++)
      {
        int startIndex = i * substringLength;
        int endIndex = Math.Min(startIndex + substringLength, length);
        substrings[i] = input.Substring(startIndex, endIndex - startIndex);
      }

      return substrings;
    }

    public Image DrawImageText (
      string number, 
      string mark, 
      string gosnumber, 
      string rider, 
      string[] dateCreated, 
      string[] dateClosed,
      string tableNumber,
      string snils, 
      string dateGet, 
      string classDocument,
      string numberDocument, 
      string timeRoute,
      string nameRoute,
      string nameMehanic,
      string nameDispetcher
      )
    {
    // Загрузите изображение
    Bitmap image = new Bitmap(imageListPath1);

    // Создайте объект Graphics для рисования на изображении
    using (Graphics graphics = Graphics.FromImage(image))
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        // МАРКА АВТОМОБИЛЯ
        DrawTextOnPoint(graphics, new Point(445, 555), mark);
        // ГОС ЗНАК
        DrawTextOnPoint(graphics, new Point(772, 604), gosnumber);
        // ФИО ВОДИТЕЛЯ
        graphics.DrawString(rider, bold, brush, new Point(295, 647));
        // СНИЛС ВОДИТЕЛЯ
        DrawTextOnPoint(graphics, new Point(101, 692), snils);
        // НОМЕР УДОСТОВЕРЕНИЕ
        DrawTextOnPoint(graphics, new PointF(390, 755), numberDocument);
        // Дата выдачи
        DrawTextOnPoint(graphics, new PointF(948, 755), dateGet);

        DrawTextOnPoint(graphics, new PointF(3115, 235), Instrumental.totalOrganisation.OKPO);
        DrawTextOnPoint(graphics, new PointF(3115, 285), Instrumental.totalOrganisation.OKOPF);

        // Класс документ
        // DrawTextOnPoint(graphics, new PointF(1345, 755), classDocument);

        // С дата (день)
        DrawTextOnPoint(graphics, new PointF(400, 245), dateCreated[0]);
        // С дата (месяц)
        DrawTextOnPoint(graphics, new PointF(650, 240), dateCreated[1]);
        // С дата (год)
        DrawTextOnPoint(graphics, new PointF(1111, 235), dateCreated[2]);

        DrawTextOnPoint(graphics, new PointF(1980, 1700), dateCreated[0] + " " + dateCreated[1] + " 202" + dateCreated[2]);

        DrawTextOnPoint(graphics, new PointF(1390, 1886), dateCreated[0] + " " + dateCreated[1] + " 202" + dateCreated[2]);
        
        if (nameDispetcher != null) {
          string[] nameDispArray = nameDispetcher.Split();
          DrawTextOnPoint(graphics, new PointF(650, 2120), nameDispArray[0] + " " + nameDispArray[1][0] + "." + nameDispArray[2][0] + ".");
        }
        

        // По дата (день)
        DrawTextOnPoint(graphics, new PointF(1280, 240), dateClosed[0]);
        // По дата (месяц)
        DrawTextOnPoint(graphics, new PointF(1570, 240), dateClosed[1]);
        // По дата (год)
        DrawTextOnPoint(graphics, new PointF(1995, 240), dateClosed[2]);

        // дата верхняя
        // date (day) 
        DrawTextOnPoint(graphics, new PointF(920, 175), dateCreated[0]);
        // date month 
        DrawTextOnPoint(graphics, new PointF(1180, 175), dateCreated[1]);
        // date year 
        DrawTextOnPoint(graphics, new PointF(1600, 175), dateCreated[2]);

        // number ts
        DrawTextOnPoint(graphics, new PointF(1613, 100), textBox1.Text);

        // table ts 
        DrawTextOnPoint(graphics, new PointF(1595, 640), tableNumber);

        // выгрузка 
        DrawTextOnPoint(graphics, new PointF(120, 1455), Instrumental.totalOrganisation.NameCompany);

        // Маршрут время
        DrawTextOnPoint(graphics, new Point(865, 1450), timeRoute);
        DrawTextOnPoint(graphics, new PointF(2293, 1700), TimeDifferent(totalRoute.TimeRoute, 10));

        DrawTextOnPoint(graphics, new PointF(1802, 1885), TimeDifferent(totalRoute.TimeRoute, 5));

        graphics.DrawString(Instrumental.totalOrganisation.Name, new Font("Times New Roman", 9, FontStyle.Bold), brush, new PointF(350, 302));

        string[] initialsNameMeh = nameMehanic.Split();
        DrawTextOnPoint(graphics, new PointF(2260, 1785), initialsNameMeh[0] + " " + initialsNameMeh[1][0] + ". " + initialsNameMeh[2][0] + ".");
        DrawTextOnPoint(graphics, new PointF(2175, 1941), initialsNameMeh[0] + " " + initialsNameMeh[1][0] + ". " + initialsNameMeh[2][0] + ".");
        DrawTextOnPoint(graphics, new PointF(2050, 2265), initialsNameMeh[0] + " " + initialsNameMeh[1][0] + ". " + initialsNameMeh[2][0] + ".");
        graphics.DrawString(initialsNameMeh[0] + " " + initialsNameMeh[1][0] + ". " + initialsNameMeh[2][0] + ".", small, brush, new PointF(3095, 642));
        graphics.DrawString(initialsNameMeh[0] + " " + initialsNameMeh[1][0] + ". " + initialsNameMeh[2][0] + ".", small, brush, new PointF(3095, 583));
        // Рагрузка
        string[] tempString = nameRoute.Split(',');

        for (int i=0; i != tempString.Length; i++) {
            DrawTextOnPoint(graphics, new Point(1630, 1450 + i * 52), tempString[i] + (i + 1 == tempString.Length ? "" : ","));
        }

        DrawTextOnPoint(graphics, new PointF(1180, 1455), totalPointStart.Name);
        DrawTextOnPoint(graphics, new PointF(2246, 1455), Instrumental.totalOrganisation.Item);
      }
      return image;
  }
    private void button1_Click(object sender, EventArgs e)
    {
      string[] dateCreated = { dateTimePicker1.Value.Day.ToString(),
        allMonth[dateTimePicker1.Value.Month - 1],
        dateTimePicker1.Value.Year.ToString()[3].ToString()
      };
      string[] dateClosed = {
        dateTimePicker2.Value.Day.ToString(),
        allMonth[dateTimePicker2.Value.Month - 1],
        dateTimePicker2.Value.Year.ToString()[3].ToString()
      };

        if (totalRoute != null && totalCar != null && totalRider !=  null && totalRoute != null && totalMehanic != null && totalDispetcher != null) {
          Image image = DrawImageText(
            textBox1.Text,
            totalCar.MarkAuto,
            totalCar.NumberAuto,
            totalRider.Name,
            dateCreated,
            dateClosed,
            totalRider.TabelNumber,
            totalRider.Snils,
            totalRider.DateGet,
            totalRider.Class,
            totalRider.Document,
            totalRoute.TimeRoute,
            totalRoute.NameRoute,
            totalMehanic.Name,
            totalDispetcher.Name
          );

          Journal journal = new Journal
          {
            Number = Convert.ToInt32(textBox1.Text),
            DateCreated = dateCreated[0] + " " + dateCreated[1] + " 202" + dateCreated[2],
            NameRider = totalRider.Name,
            Routes = totalRoute.NameRoute,
            MarkCar = totalCar.MarkAuto,
            NumberCar = totalCar.NumberAuto,
            NameMehanic = totalMehanic.Name,
            NameDispetcher = totalDispetcher.Name,
            idRider = totalRider.Id,
            idCar = totalCar.AutoID,
            idRoute = totalRoute.Id,
            idDispetcher = totalDispetcher.Id,
            idMehanic = totalMehanic.Id,
            OrganisationId = Instrumental.totalOrganisation.Id
          };

        bool statusRewrite = false;
        Journal searchJournal = journalData.Records.Find(item => item.Number == journal.Number);

        if (searchJournal != null)
        {
          DialogResult dt = MessageBox.Show("Лист с данным номером, уже существует. Вы хотите перезаписать данный лист?", "Перезапись", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
          if (dt == DialogResult.Yes)
          {
            statusRewrite = true;
            journal.Number = searchJournal.Number;
            ImageShow form = new ImageShow(image, journal, statusRewrite);
            form.ShowDialog();
            image.Dispose();
          } else {
            MessageBox.Show("Путевой лист должен иметь уникальный порядковый номер!", "Ошибка данный лист уже существует", MessageBoxButtons.OK, MessageBoxIcon.Error);
            image.Dispose();
          }
        }
        else
        {
          statusRewrite = false;
          ImageShow form = new ImageShow(image, journal, statusRewrite);
          form.ShowDialog();
          image.Dispose();
        }

      }
      else {
          MessageBox.Show("Ошибка заполнения данных, проверьте все поля и заполните их", "Ошибка заполнения данных");
        }
    }

    // ЗАГРУЗКА ФОРМЫ
    public void Form1_Load(object sender, EventArgs e)
    {
      Instrumental.LoadDataAboutCars(ref carsData, dataCarsStoragePath);
      Instrumental.LoadDataAboutRiders(ref ridersData, dataRidersStoragePath);
      Instrumental.LoadDataAboutRoutes(ref routesData, dataRoutesStoragePath);
      Instrumental.LoadDataAboutMehanics(ref mehanicsData, "Data/dMechanic.json");
      Instrumental.LoadDataAboutDispetcher(ref dispetcherData, "Data/dDispetchers.json");
      Instrumental.LoadDataAboutJournal(ref journalData);
      Instrumental.LoadDataAboutPointStart(ref pointData);
      //Instrumental.LoadDataAboutSave(ref saveData);

      textBox1.Text = DataLib.GetOrganisationNumber().ToString();

      comboBox1.DataSource = ridersData.Riders;
      comboBox1.ValueMember = "Id";
      comboBox1.DisplayMember = "Name";
      carComboBox2.DataSource = carsData.Cars;
      carComboBox2.ValueMember = "AutoID";
      carComboBox2.DisplayMember = "NumberAuto";
      routesComboBox.DataSource = routesData.Routes;
      routesComboBox.ValueMember = "Id";
      routesComboBox.DisplayMember = "NameRoute";
      comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

      carComboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
      carComboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      
      routesComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      routesComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

      pointStartComboBox.DataSource = pointData.PointStarts;
      pointStartComboBox.ValueMember = "Id";
      pointStartComboBox.DisplayMember = "Name";
      pointStartComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      pointStartComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

      comboBox2.DataSource = mehanicsData.Mehanics;
      comboBox2.ValueMember = "Id";
      comboBox2.DisplayMember = "Name";
      comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      
      comboBox3.DataSource = dispetcherData.Dispetchers;
      comboBox3.ValueMember = "Id";
      comboBox3.DisplayMember = "Name";
      comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
      comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

      comboBox1_SelectedValueChanged(comboBox1, EventArgs.Empty);
      carComboBox2_SelectedValueChanged(carComboBox2, EventArgs.Empty);
      routesComboBox_SelectedValueChanged(routesComboBox, EventArgs.Empty);
      comboBox2_SelectedIndexChanged(comboBox2, EventArgs.Empty);
      comboBox3_SelectedIndexChanged(comboBox3, EventArgs.Empty);
      pointStartComboBox_SelectedIndexChanged (pointStartComboBox, EventArgs.Empty);
    }

    private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
    {
      if (comboBox1.SelectedIndex != -1) {
        try
        {
          selectedIndexRiders = (int)comboBox1.SelectedValue;
          totalRider = Instrumental.getIdElement(ridersData, selectedIndexRiders);
          totalCar = Instrumental.getIdElement(carsData, totalRider.AutoID);
          if (totalCar != null) carComboBox2.SelectedValue = totalCar.AutoID;
          snilsLabel.Text = totalRider.Snils;
          documentLabel.Text = totalRider.Document;
          dateGetLabel.Text = totalRider.DateGet;
        } catch (Exception ex) {
          selectedIndexRiders = -1;
        }
      }

    }

    private void carComboBox2_SelectedValueChanged(object sender, EventArgs e)
    {
      if (carComboBox2.SelectedIndex != -1) {
         try {
          selectedIndexCars = (int)carComboBox2.SelectedValue;
          totalCar = Instrumental.getIdElement(carsData, selectedIndexCars);
         } catch {
          selectedIndexCars = -1;
         }
      }
    }

    private void routesComboBox_SelectedValueChanged(object sender, EventArgs e)
    {
      if (routesComboBox.SelectedIndex != -1) {
        try {
          selectedIndexRoutes = (int)routesComboBox.SelectedValue;

          totalRoute = Instrumental.getIdElement(routesData, selectedIndexRoutes);
          timeRouteLabel.Text = totalRoute.TimeRoute;
          label17.Text = totalRoute.NameRoute;
        } catch {
          selectedIndexRoutes= -1;
        }
      }
    }

    private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void Form1_Activated(object sender, EventArgs e)
    {

      if (Instrumental.wasUpdatedMehanicsOrDispetchers) {
        Instrumental.LoadDataAboutMehanics(ref mehanicsData, "Data/dMechanic.json");
        Instrumental.LoadDataAboutDispetcher(ref dispetcherData, "Data/dDispetchers.json");
        
        comboBox2.DataSource = mehanicsData.Mehanics;
        comboBox3.DataSource = dispetcherData.Dispetchers;

        Instrumental.wasUpdatedMehanicsOrDispetchers = false;
      }

      if (Instrumental.wasUpdated) {
        Instrumental.LoadDataAboutRiders(ref ridersData, dataRidersStoragePath);
        Instrumental.LoadDataAboutCars(ref carsData, dataCarsStoragePath);
        Instrumental.LoadDataAboutRoutes(ref routesData, dataRoutesStoragePath);
        Instrumental.LoadDataAboutPointStart(ref pointData);

        textBox1.Text = DataLib.GetOrganisationNumber().ToString();

        comboBox1.DataSource = ridersData.Riders;
        carComboBox2.DataSource = carsData.Cars;
        routesComboBox.DataSource = routesData.Routes;
        pointStartComboBox.DataSource = pointData.PointStarts;

        Instrumental.LoadDataAboutJournal(ref journalData);

        comboBox1_SelectedValueChanged(comboBox1, EventArgs.Empty);
        carComboBox2_SelectedValueChanged(carComboBox2, EventArgs.Empty);
        routesComboBox_SelectedValueChanged(routesComboBox, EventArgs.Empty);
        comboBox2_SelectedIndexChanged(comboBox2, EventArgs.Empty);
        comboBox3_SelectedIndexChanged(comboBox3, EventArgs.Empty);
        pointStartComboBox_SelectedIndexChanged(pointStartComboBox, EventArgs.Empty);

        Instrumental.wasUpdated = false;
      }
      
      if (Instrumental.journalSave != null) {
        textBox1.Text = DataLib.GetOrganisationNumber().ToString();

        comboBox1.SelectedValue = Instrumental.journalSave.idRider;
        carComboBox2.SelectedValue = Instrumental.journalSave.idCar;
        routesComboBox.SelectedValue = Instrumental.journalSave.idRoute;
        comboBox2.SelectedValue = Instrumental.journalSave.idMehanic;
        comboBox3.SelectedValue = Instrumental.journalSave.idDispetcher;

        dateTimePicker1.Value = DateTime.Now;
        dateTimePicker2.Value = DateTime.Now;
        Instrumental.journalSave = null; 
      }
    }

    private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      CarAdd form = new CarAdd(ref carsData);
      form.Show();
    }

    private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
    {
      RouteAdd form = new RouteAdd(ref routesData);
      form.Show();
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox2.SelectedIndex != -1)
      {
        try
        {
          int idTotalMeh = (int)comboBox2.SelectedValue;
          totalMehanic = mehanicsData.Mehanics.Find(meh => meh.Id == idTotalMeh);
          //Instrumental.SaveMehanicId("Data/dSave.json", saveData, idTotalMeh);
        }
        catch
        {
          
        }
      }
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (comboBox3.SelectedIndex != -1) {
        try {
          int idTotalMeh = (int)comboBox3.SelectedValue;
          totalDispetcher = dispetcherData.Dispetchers.Find(meh => meh.Id == idTotalMeh);
          //Instrumental.SaveDispetcherId("Data/dSave.json", saveData, idTotalMeh);

        } catch {
        }
      }
    }

    private void добавитьМеханикиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddMechanic form = new AddMechanic(ref mehanicsData);
      form.Show();

    }

    private void добавитьДиспетчераToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddDispetcher form = new AddDispetcher(ref dispetcherData);
      form.Show();
    }

    private void журналToolStripMenuItem_Click(object sender, EventArgs e)
    {
      JournalForm form = new JournalForm();
      form.Show();
    }

    private void задатьНомерЛистаToolStripMenuItem_Click(object sender, EventArgs e)
    {
      InputNumberList form = new InputNumberList();
      form.ShowDialog();
    }

    private void редактироватьToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }

    private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditPageRider form = new EditPageRider(); 
      form.Show();
    }

    private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void редактированиеМехникиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditPageMehanic form = new EditPageMehanic();
      form.Show();
    }

    private void редактироватьToolStripMenuItem2_Click(object sender, EventArgs e)
    {
      EditPageRoute form = new EditPageRoute(); 
      form.Show();
    }

    private void редактированиеДиспетчераToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditPageDispetcher form = new EditPageDispetcher(); 
      form.Show();
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }

    private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
    {
      if (dateTimePicker2.Value > dateTimePicker1.Value) {
        dateTimePicker1.Value = dateTimePicker2.Value;
      }
    }


    private void автомобилиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditPageCar form = new EditPageCar();
      form.Show();
    }

    private void водителиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditPage editPage = new EditPage(this, ref ridersData, carsData);
      editPage.Show();
    }

    private void пунктОтправкиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      EditPagePointStart form = new EditPagePointStart(); 
      form.Show();
    }

    private void pointStartComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (pointStartComboBox.SelectedIndex != -1)
      {
        try
        {
          selectedPointStart = (int)pointStartComboBox.SelectedValue;
          totalPointStart = Instrumental.getIdElement(pointData, selectedPointStart);
        }
        catch
        {
          selectedPointStart = -1;
        }
      }
    }
  }
}
