using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CURD_TOKO_HARDWARE_SOFTWARE.Trigger
{
    
    class TriggerDatabase
    {
        public void databaseEvent_Penjulan()
        {
            // Penjualan pen = new Penjualan();

            DataTable table = new DataTable("Penjualan");

            table.Columns["IdPenjualan"].Unique = true;
            table.PrimaryKey = new DataColumn[] { table.Columns["IdPenjualan"] };
           // table.Columns.Add("CreateDate", DateTime.Now);
        }

    }
}
