using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace WPF_CURD_TOKO_HARDWARE_SOFTWARE
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        //windows time

       DispatcherTimer timer = new DispatcherTimer();
        

        Final_Task_WPFEntities4 db = new Final_Task_WPFEntities4();
        MainWindow m = new MainWindow();
        public LoginPage()
        {
            InitializeComponent();
                
        }


        void timer_tick(object sender, System.EventArgs e)
        {
            Loginbutton.IsEnabled = true;
            timer.Stop();
        }
        public int i=1;
        private void Loginbutton_Click(object sender, RoutedEventArgs e)
        {
            
            
            //============================          
              
            string user = Login_textBox.Text;
            string pass = Login_passwordBox.Password;
            var loginAdmin = db.UserAdmin.Where(x => x.Username == user).Where(x => x.Password == pass).Where(x => x.Status == "admin").FirstOrDefault();
            var loginUser = db.UserAdmin.Where(c => c.Username == user).Where(c => c.Password == pass).Where(c => c.Status == "user").FirstOrDefault();
            UserAdmin adminuser = new UserAdmin();
               
            if ( i <= 3)
            {
                    if (loginAdmin != null)
                    {
                        m.Nama_Login.Text = loginAdmin.Status;
                        this.Close();
                        m.Show();
                    }

                    else if (loginUser != null)
                    {
                        //untuk hidden tab
                        m.TabBarang.Visibility = Visibility.Hidden;
                        m.TabPembelian.Visibility = Visibility.Hidden;
                        m.TabSupplier.Visibility = Visibility.Hidden;
                        //membuat nama login pada menu utama sama dengan status
                        m.Nama_Login.Text = loginUser.Status;
                        //tab ini akan tutup
                        this.Close();
                        //m= main tampil.
                        m.Show();
                    }
                    else
                    { MessageBox.Show("Username/Password salah"); }
            }
            else
            {
                //batas waktu
                timer.Interval = TimeSpan.FromSeconds(10);
                timer.Tick += timer_tick;
                timer.Start();
                MessageBox.Show("Anda Harus menunggu selama 10 detik");
                Loginbutton.IsEnabled = false;
                i = 1;
            }
            i = i + 1;

        }    
    }
}
