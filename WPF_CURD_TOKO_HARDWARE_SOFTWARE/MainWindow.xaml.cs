using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WPF_CURD_TOKO_HARDWARE_SOFTWARE.Trigger;
using WPF_CURD_TOKO_HARDWARE_SOFTWARE.View;
using WPF_CURD_TOKO_HARDWARE_SOFTWARE.View_Model;


namespace WPF_CURD_TOKO_HARDWARE_SOFTWARE
{
    
    public partial class MainWindow : Window
    {
        //======== database conn =======//
        Final_Task_WPFEntities4 db = new Final_Task_WPFEntities4();
              
        //====== method combo box ======
        void combo_Barang_supplier()
        {
            List<Supplier> list_ = db.Supplier.Where(x => x.IsDelete == false).ToList();
            Barang_NMKategori_comboBox.ItemsSource = list_;
        }
        void combo_Pembelian_supplier()
        {
            List<Supplier> list_ = db.Supplier.Where(x => x.IsDelete == false).ToList();
            Pembelian_comboBox_nmaSupp.ItemsSource = list_;
        }
        void combo_Pembelian_NamaBarang()
        {
            List<Barang> list_ = db.Barang.Where(x => x.IsDelete == false).ToList();
            Pembelian_comboBox_NamaBarang.ItemsSource = list_;
        }
        void combo_Penjualann_NamaSupplier()
        {

            List<Supplier> list_ = db.Supplier.Where(x => x.IsDelete == false).ToList();
            Penjualan_Supplier_comboBox.ItemsSource = list_;
        }
        void combo_Penjualan_NamaBarang()
        {
            List<Barang> list_ = db.Barang.Where(x => x.IsDelete == false).ToList();
            Penjualan_NamaBarang_comboBox_.ItemsSource = list_;
        }
                
        //========== show list datagrid only false ========
        public void delete_Barang()
        {
            try
            {
                var data = db.Barang.Where(x => x.IsDelete == false).ToList();
                Barang_dataGrid.ItemsSource = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void delete_Supplier()
        {
            try
            {
                var data = db.Supplier.Where(y => y.IsDelete == false).ToList();
                Supplier_dataGrid1.ItemsSource = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       

        public MainWindow()
        {
            InitializeComponent();
            //==============
            delete_Barang();
            delete_Supplier();
            //==============
            combo_Penjualann_NamaSupplier();
        }

       
        //========================================
        //=========== supplier ===================
        //========================================
        private void Supplier_button_Save_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                SupplierVM SPVM = new SupplierVM();
                SPVM.NamaSupplier = Supplier_textBox.Text;

                Supplier SP = new Supplier();
                SP.NamaSupplier = SPVM.NamaSupplier;
                SP.CreateDate = DateTime.Now;

                db.Entry(SP).State = EntityState.Added;
                db.SaveChanges();
                MessageBox.Show("Input Berhasil");
                delete_Supplier();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Supplier_button_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SupplierVM SPVM = new SupplierVM();
                SPVM.NamaSupplier = Supplier_textBox.Text;

                int delS = int.Parse(Supplier_id_textBox.Text);

                var delete_ = db.Supplier.Find(delS);               
                delete_.NamaSupplier = SPVM.NamaSupplier;
                delete_.UpdateDate = DateTime.Now;

                db.Entry(delete_).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Update Berhasil");
                delete_Supplier();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Supplier_button_IsDelete_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int inpt_id = int.Parse(Supplier_id_textBox.Text);

                var delete = db.Supplier.Find(inpt_id);
                delete.IsDelete = true;
                delete.DeleteDate = DateTime.Now;
                db.Entry(delete).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Delete Sukses");
                delete_Supplier();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }       
        private void Supplier_dataGrid1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                object control = Supplier_dataGrid1.SelectedItem;
                string _id_ = (Supplier_dataGrid1.SelectedCells[0].Column.GetCellContent(control) as TextBlock ).Text;
                string _nama_ = (Supplier_dataGrid1.SelectedCells[1].Column.GetCellContent(control) as TextBlock).Text;

                Supplier_id_textBox.Text = _id_;
                Supplier_textBox.Text = _nama_;
                delete_Supplier();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        //============================================================
        //========================== Barang ==========================
        //============================================================
        BarangVM BVM = new BarangVM();
        Barang Ba = new Barang();
        private void NMKategori_Barang_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combo_Barang_supplier();     
        }
        private void Barang_NMBarang_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Barang_button_Save_Click_1(object sender, RoutedEventArgs e)
        {
            
            try
            {
                BarangVM BVM = new BarangVM();
                BVM.NamaBarang = Barang_NMBarang_textBox.Text;
                BVM.HargaSatuan = int.Parse(Barang_Stok_textBox1.Text);
                BVM.Stock = int.Parse(Barang_HrgSatuan_textBox2.Text);

                Barang Ba = new Barang();
                Ba.IdSupplier = Convert.ToInt32(Barang_NMKategori_comboBox.SelectedValue);
                Ba.IdAdmin = int.Parse(IdadmintextBox1.Text);
                Ba.NamaBarang = BVM.NamaBarang;
                Ba.Stok = BVM.Stock;
                Ba.HargaSatuan = BVM.HargaSatuan;
                Ba.CreateDate = DateTime.Now;

                db.Entry(Ba).State = EntityState.Added;

                db.SaveChanges();
                MessageBox.Show("Input Berhasil");
                delete_Barang();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Barang_dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BarangVM BVM = new BarangVM();
            Barang Ba = new Barang();
            try
            {
                var b1 = Barang_dataGrid.SelectedItem;
                
                string id_ = (Barang_dataGrid.SelectedCells[0].Column.GetCellContent(b1) as TextBlock).Text;
                string supplier_ = (Barang_dataGrid.SelectedCells[1].Column.GetCellContent(b1) as TextBlock).Text;
                string nama_ = (Barang_dataGrid.SelectedCells[3].Column.GetCellContent(b1) as TextBlock).Text;
                string stok_ = (Barang_dataGrid.SelectedCells[4].Column.GetCellContent(b1) as TextBlock).Text;
                string harga_ = (Barang_dataGrid.SelectedCells[5].Column.GetCellContent(b1) as TextBlock).Text;
                
                ID_Barang_textBox1.Text = id_;
                int idsup = int.Parse(supplier_);
                Barang_NMKategori_comboBox.SelectedValue = idsup;
                Barang_NMBarang_textBox.Text = nama_;
                Barang_Stok_textBox1.Text = stok_;
                Barang_HrgSatuan_textBox2.Text = harga_;

                delete_Barang();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }               
        private void Barang_button_Update_Click(object sender, RoutedEventArgs e)
        {                            
            BVM.NamaBarang = Barang_NMBarang_textBox.Text;
            BVM.Stock = int.Parse(Barang_Stok_textBox1.Text);
            BVM.HargaSatuan = int.Parse(Barang_HrgSatuan_textBox2.Text);

            int id_ = Convert.ToInt32(ID_Barang_textBox1.Text);
            var update_ = db.Barang.Find(id_);

            update_.IdAdmin = int.Parse(IdadmintextBox1.Text);
            update_.HargaSatuan = BVM.HargaSatuan;
            update_.Stok = BVM.Stock;
            update_.NamaBarang = BVM.NamaBarang;
            update_.UpdateDate = DateTime.Now;
           
            try
            {
                db.Entry(update_).State = EntityState.Modified;
                                
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Update sukses");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private void Barang_button_IsDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int inpt_id = int.Parse(ID_Barang_textBox1.Text);

                var delete = db.Barang.Find(inpt_id);
                delete.IsDelete = true;
                delete.DeleteDate = DateTime.Now;
                db.Entry(delete).State = EntityState.Modified;

                delete_Barang();
                db.SaveChanges();
                MessageBox.Show("Delete Sukses");
                delete_Barang();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Barang_NMKategori_comboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            combo_Barang_supplier();
        }
        private void Barang_Stok_textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Barang_HrgSatuan_textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        //===================== Penjualan ==============================  
        int no_idbarang ;
        int nilai;
        List<ListAddCart> ListPenjualan = new List<ListAddCart>();
        List<ListAddCart> list123 = new List<ListAddCart>();
        private void Penjualan_JumlahBarang_textBox__TextChanged_2(object sender, TextChangedEventArgs e)
        {

            var x1 = db.Barang.Where(x => x.IdBarang == no_idbarang).Select(x => x.Stok).ToList();
            int jumlhstok = Convert.ToInt32(x1[0]);
            var input_barang = Penjualan_JumlahBarang_textBox_.Text;
           

            if (Int32.TryParse(input_barang, out nilai))
                {
                if (jumlhstok < nilai)
                {
                    Penjualan_JumlahBarang_textBox_.Text = Convert.ToString(jumlhstok);
                }
                else if (nilai < 0)
                {
                    Penjualan_JumlahBarang_textBox_.Text = Convert.ToString("0");
                }
            }
        }
        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }     
        private void Penjualan_kategori_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int fetch_id_supplier = Convert.ToInt32(Penjualan_Supplier_comboBox.SelectedValue);
            List<Barang> list_ = db.Barang.Where(x => x.IsDelete == false).Where(x => x.IdSupplier == fetch_id_supplier).ToList();
            Penjualan_NamaBarang_comboBox_.ItemsSource = list_;
        }
        private void Penjualan_Supplier_comboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            combo_Penjualann_NamaSupplier();
        }
        private void Penjualan_comboBox_NamaBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Penjualan_NamaBarang_comboBox_.SelectedValue != null )
            {
                no_idbarang = Convert.ToInt32(Penjualan_NamaBarang_comboBox_.SelectedValue);
                var Ba = db.Barang.Where(x => x.IdBarang == no_idbarang).Select(a => a.HargaSatuan).ToList();
                HargaSatuantextBlock.Text = Convert.ToString(Ba[0]);
            }
        }
        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int total = int.Parse(totalharga_textBox1.Text);
            int bayar = int.Parse(textBox2.Text);

            int sum = bayar - total;
            textBox3.Text = Convert.ToString(sum);

        }
        private void Penjualan_ADDtoCart_button_Click(object sender, RoutedEventArgs e) // add to cart from list to daagrid
        {
            try
            {

                int idbarangs = Convert.ToInt32(Penjualan_NamaBarang_comboBox_.SelectedValue);
                string namabarang = Penjualan_NamaBarang_comboBox_.Text;
                int jumlahbarang = int.Parse(Penjualan_JumlahBarang_textBox_.Text);
                int harga = int.Parse(HargaSatuantextBlock.Text);
                int jumlahtotalharga = jumlahbarang * harga;
                //int id_ = ListAddCart.
                

                

                var listdata = new ListAddCart { id = idbarangs, namabarang = namabarang, jumlah = jumlahbarang, harga = harga, jumlahtotal = jumlahtotalharga };
                dataGrid.Items.Add(listdata);
                //when same data on list
                


                //summary total harga
                decimal sum = jumlahtotalharga;

                for (int x = 0; x < dataGrid.Items.Count - 1; x++)
                {
                    sum += (decimal.Parse((dataGrid.Columns[4].GetCellContent(dataGrid.Items[x]) as TextBlock).Text));
                }
                totalharga_textBox1.Text = Convert.ToString(sum);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }

            
        }
        private void Penjualan_JumlahBarang_textBox__PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);         

        }      
        private void Penjualan_button_Save_Click(object sender, RoutedEventArgs e)
        {
            int idpenjualan_ = 0;
            //insert table penjualan

            Penjualan pen = new Penjualan();
            pen.CreateDate = DateTime.Now;
            db.Entry(pen).State = EntityState.Added;
            db.SaveChanges();
            idpenjualan_ = pen.IdPenjualan;
            //--------------------------------------------

            //insert table item
            foreach (var listnya in dataGrid.Items) // ambil satu baris tiap row
            {
                ItemPenjualan IP = new ItemPenjualan();

                var ambildariperulangan = listnya as ListAddCart; 
                
                IP.IdAdmin = 1711;
                IP.IdPenjualan = idpenjualan_;
                IP.IdBarang = db.Barang.Where(x => x.NamaBarang == ambildariperulangan.namabarang).Select(x => x.IdBarang).FirstOrDefault();
                IP.TotalBarang = ambildariperulangan.jumlah;
                IP.CreateDate = DateTime.Now;
                db.Entry(IP).State = EntityState.Added;
                db.SaveChanges();                
            }


            foreach (var b1 in dataGrid.Items)
            {
                var listPenjualan = b1 as ListAddCart;

                int idbarang = db.Barang.Where(x => x.NamaBarang == listPenjualan.namabarang).Select(x=>x.IdBarang).FirstOrDefault();
                var ba = db.Barang.Find(idbarang);


                ba.Stok = ba.Stok - listPenjualan.jumlah;
                db.Entry(ba).State = EntityState.Modified;
                db.SaveChanges();

            }
            ListPenjualan.Clear();
            dataGrid.Items.Clear();
            MessageBox.Show("Terimakasih");
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //================================================================
        //======================= Pembelian ==============================
        //================================================================
        private void Pembelian_comboBox_nmaSupp_MouseEnter(object sender, MouseEventArgs e)
        {
            combo_Pembelian_supplier();
        }
        private void Pembelian_comboBox_nmaSupp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int fetch_c_sup = Convert.ToInt32(Pembelian_comboBox_nmaSupp.SelectedValue);
            List<Barang> list_ = db.Barang.Where(x => x.IsDelete == false).Where(x => x.IdSupplier == fetch_c_sup).ToList();
            Pembelian_comboBox_NamaBarang.ItemsSource = list_;
        }
        private void Pembelian_comboBox_NamaBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combo_Pembelian_NamaBarang();
        }
        private void Pembelian_textBox_qty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void textBox_totharga_pem__PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void AddToCart_button_Click(object sender, RoutedEventArgs e)
        {
            int kategori = Convert.ToInt32(Pembelian_comboBox_nmaSupp.SelectedValue);
            string namabarang = Pembelian_comboBox_NamaBarang.Text;
            int qty = int.Parse(Pembelian_textBox_qty.Text);

            var addlist = new ListAddCartPembelian { idkategori = kategori, namabarang = namabarang, jumlahbarang = qty };
            Pembelian_dataGrid.Items.Add(addlist);            
       }
        private void Pembelian_button_Save_Click(object sender, RoutedEventArgs e)
        {
            //insert pembelian 
            Pembelian pem = new Pembelian();
            pem.CreateDate = DateTime.Now;
            db.Entry(pem).State = EntityState.Added;
            db.SaveChanges();

            //-----------------------------------------
            int idpembelian = pem.IdPembelian;

            //insert item pembelian
            foreach (var list_ in Pembelian_dataGrid.Items)
            {
                ItemPembelian IE = new ItemPembelian();

                var ambildatagrid = list_ as ListAddCartPembelian;

                IE.IdPembelian = idpembelian;
                IE.IdBarang = db.Barang.Where(x => x.NamaBarang == ambildatagrid.namabarang).Select(x => x.IdBarang).FirstOrDefault();
                IE.IdAdmin = 1711;
                IE.QTY = int.Parse(Pembelian_textBox_qty.Text);
                IE.CreateDate = DateTime.Now;
                db.Entry(IE).State = EntityState.Added;
                db.SaveChanges();
            }

            var listpembelian = new List<ListAddCartPembelian>();

            //update to stok

            foreach (var updatestok in Pembelian_dataGrid.Items)
            {
                Barang barang_ = new Barang();

                var pembelian = updatestok as ListAddCartPembelian;

                int idbaranglist = db.Barang.Where(x => x.NamaBarang == pembelian.namabarang).Select(i => i.IdBarang).FirstOrDefault();
                var cariid = db.Barang.Find(idbaranglist);

                int stoklist = pembelian.jumlahbarang;
                cariid.Stok = cariid.Stok + stoklist;

                db.Entry(cariid).State = EntityState.Modified;
                db.SaveChanges();

            }            
            listpembelian.Clear();
            Pembelian_dataGrid.Items.Clear();

            //-----------------------------

            MessageBox.Show("Terimakasih");
            
        }
        

        //##########################################################################################
            private void Log_Out_Click(object sender, RoutedEventArgs e)
            {
                LoginPage login = new LoginPage();
                this.Close();
                login.Show();
            }

        }
    }



//addcart.Add(new ListAddCart { id = idbarangs, namabarang = namabarang, jumlah = jumlahbarang, harga = harga, jumlahtotal = jumlahbarang * harga });
//dataGrid.ItemsSource = addcart.ToList();
//var addcart = new List<addtocart>();
//addcart.Add(new addtocart { id = 1, jumlah = 5000, namabarang = "Moba", harga = 9999 });
//addcart.Add(new addtocart { id = 1, jumlah = 5000, namabarang = "Moba", harga = 9999 });
//dataGrid.ItemsSource = addcart;







//+++++++++++++++++++++++++++ cara penjualan ke 2 ++

//int idbarangs = Convert.ToInt32(Penjualan_NamaBarang_comboBox_.SelectedValue);
//    string namabarang = Penjualan_NamaBarang_comboBox_.Text;
//    int jumlahbarang = int.Parse(Penjualan_JumlahBarang_textBox_.Text);
//    int harga = int.Parse(HargaSatuantextBlock.Text);
//    int jumlahtotalharga = jumlahbarang * harga;

//    list123.Add(new ListAddCart()
//    {
//        id = idbarangs,
//        namabarang = namabarang,
//        jumlah = jumlahbarang,
//        harga = harga,
//        jumlahtotal = jumlahtotalharga
//    });
//    dataGrid.ItemsSource = list123.ToList();

//------------------------------------------------------------------------------

//Debug.WriteLine("id kategori : " + Ba.IdKategori);
//Debug.WriteLine("id Barang : " + Ba.IdBarang);
//Debug.WriteLine("id Harga : " + Ba.HargaSatuan);
//Debug.WriteLine("id kategori : " + Ba.IdKategori);
//Debug.WriteLine("id admin : " + Ba.IdAdmin);
//Debug.WriteLine("id stok : " + Ba.Stok);
//Debug.WriteLine("id tanggal : " + Ba.CreateDate);

