using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Assignment03;



namespace Assign04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyClient> displayCollection = null;
        AppointList aList = new AppointList();
        ClientInfo info = new ClientInfo();









        List<ClientInfo> i = new List<ClientInfo>();

        //string stringAge;

        //  string stringAge = null;
        int age = 0;

        string stringHeight = null;
        decimal height = 0;

        string stringCard;
        long numCard = 0;

        string time;
        string genderType;
        public ObservableCollection<MyClient> DisplayCollection { get => displayCollection; set => displayCollection = value; }

        public ClientInfo Info { get => info; set => info = value; }


        public MainWindow()
        {


            InitializeComponent();
            DisplayCollection = new ObservableCollection<MyClient>();
            DataContext = this;
            cmb_time1.Items.Add("9 to 10");
            cmb_time1.Items.Add("10 to 11");
            cmb_time1.Items.Add("11 to 12");
            cmb_time1.Items.Add("12 to 1");
            cmb_time1.Items.Add("1 to 2");
            cmb_time1.Items.Add("2 to 3");
            cmb_time1.Items.Add("3 to 4");
            cmb_time1.Items.Add("4 to 5");


            string[] genderType = Enum.GetNames(typeof(client));
            foreach (string gender in genderType)
            {
                cmb_type.Items.Add(gender);
            }
        }
        private void cmb_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            client clientCombo = (client)cmb_type.SelectedIndex;
            genderType = cmb_type.SelectedItem.ToString();
            
        }
        /*  public void Clear()
         {
             txt_age.Text = "";
             txt_height.Text = "";
             txt_cc.Text = "";
             cmb_time1.Text ="" ;
             cmb_type.SelectedIndex =0 ;
             cmb_time1.SelectedIndex = 0;
         }*/
        public void writeFile()
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("myFile.bin", FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(genderType);
                bw.Write(age);
                // MessageBox.Show("age Written");
                bw.Write(height);
                //MessageBox.Show("Height");

                bw.Write(stringCard);
                //MessageBox.Show("card");

                bw.Write(time);
                //MessageBox.Show("time");

                //MessageBox.Show("file Written");

                bw.Close();
                fs.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Cmb_time1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            time = cmb_time1.SelectedIndex.ToString();
        }
        private void Btn_submit_Click(object sender, RoutedEventArgs e)
        {
            bool answer = true;
            int age = int.Parse(txt_age.Text);



            MyClient myClient = new MyClient();
            myClient.Type= cmb_type.SelectedItem.ToString();

            Appoint appoint = new Appoint();
            appoint.ApptDate= cmb_time1.SelectedItem.ToString();

            ClientInfo newClient=null;

            switch (cmb_type.SelectedIndex)
            {
                case 0:
                    newClient = new Gentlemen();
                    break;
                case 1:
                    newClient = new Ladies();
                    break;

                case 2:
                    newClient = new Children();
                    break;
            }
            newClient.Age = age;
            newClient.Height = int.Parse(txt_height.Text);
            newClient.CardNumber = txt_cc.Text;
            appoint.MyData = newClient;
            myClient.InnerClient = appoint;
            displayCollection.Add(myClient);

            txt_age.Text = string.Empty;
            txt_height.Text = string.Empty;





            //age

            /*  stringAge = txt_age.Text;


              if ((int.TryParse(stringAge, out age) && age >= 1 && age <= 100))
              {
                  txt_age.Foreground = Brushes.Black;
                  ans = true;
              }
              else
              {
                  txt_age.Foreground = Brushes.Red;
                  ans = false;
              }


              stringHeight = txt_height.Text;


              if (!(decimal.TryParse(stringHeight, out height) && height >= 30 && height <= 200))
              {
                  txt_height.Foreground = Brushes.Black;
                  ans = true;
              }
              else
              {
                  txt_height.Foreground = Brushes.Red;
                  ans = false;
              }
              stringCard = txt_cc.Text;

              if (!(stringCard.Length > 1) && (stringCard.Length < 17))
              {
                  txt_cc.Foreground = Brushes.Red;
                  ans = false;
              }
              else {
                  txt_cc.Foreground = Brushes.Black;
                  ans = true;
              }
              writeFile();

            //  Clear();*/
        }


        private void Btn_Display_Click(object sender, RoutedEventArgs e)
        {
            displayCollection.Clear();
            ReadFromFile();

            foreach (Appoint item in aList)
            {
                MyClient client = new MyClient();
                client.Type = item.MyData.GetType().ToString().Split('.')[1];
                client.InnerClient = item;
                displayCollection.Add(client);
            }
            Grid.ItemsSource = displayCollection;
            // readFile();
        }


        //public void readFile()
        //{


        //    FileStream fs = null;
        //    try
        //    {
        //        fs = new FileStream("myFile.bin", FileMode.Open, FileAccess.Read);
        //        BinaryReader br = new BinaryReader(fs);
        //        while (fs.Position < fs.Length)
        //        {

        //            ClientInfo clientInfo = new ClientInfo();
        //            string genderType = br.ReadString();
        //            int age = br.ReadInt32();
        //            decimal height = br.ReadDecimal();
        //            string card = br.ReadString();
        //            string time = br.ReadString();

        //            clientInfo.Age = age;
        //            Console.WriteLine("Age:{0}", age);
        //            clientInfo.Height = height;
        //            Console.WriteLine("Height:{0}", height);
        //            clientInfo.CardNumber = card.ToString();
        //            Console.WriteLine("Card Number:{0}", numCard);
        //            clientInfo.Time = time;
        //            Console.WriteLine("Time : {0}", time);
        //            Console.WriteLine("Time : {0}", genderType);

        //            txt_display.Text += clientInfo.ToString() + "\n";

        //        }
        //        br.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    finally
        //    {
        //        if (fs != null)
        //        {
        //            fs.Close();
        //        }
        //    }



        //}
        private void WriteInFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppointList));
            TextWriter txtWriter = new StreamWriter("Info.xml");
            serializer.Serialize(txtWriter, aList);
            txtWriter.Close();
        }
        private void ReadFromFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppointList));
            TextReader txtReader = new StreamReader("Info.xml");
            aList = (AppointList)serializer.Deserialize(txtReader);
            txtReader.Close();
        }

        private void Btn_save(object sender, RoutedEventArgs e)
        {
            foreach (MyClient information in displayCollection)
            {
                
                aList.Add(information.InnerClient);
            }
            WriteInFile();
        }

       

       

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {
            var query = from ClientInfo in DisplayCollection
                        where  ClientInfo.Type.ToLower()== txt_search.Text.ToLower()
                        select ClientInfo;
            Grid.ItemsSource = query;
        }

       




       



    }
}

