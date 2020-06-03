using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormSupply : Form
    {
        public FormSupply()
        {
            InitializeComponent();
            ShowRieltor();
            ShowClients();
            ShowRealEstate();
            ShowSupplySet();
        }

        void ShowRieltor()
        {
            comboBoxRieltor.Items.Clear();
            foreach (RieltorSet rieltorSet in Program.wtfDb.RieltorSet)
            {
                string[] item = {rieltorSet.ID.ToString() + "." , rieltorSet.FirstName,
                    rieltorSet.MiddleName, rieltorSet.LastName};
                comboBoxRieltor.Items.Add(string.Join(" ", item));
            }
        }

        void ShowClients()
        {
            comboBoxClients.Items.Clear();
            foreach (ClientsSet clientsSet in Program.wtfDb.ClientsSet)
            {
                string[] item = { clientsSet.ID.ToString() + ".", clientsSet.FirstName,
                    clientsSet.MiddleName, clientsSet.LastName };
                comboBoxClients.Items.Add(string.Join(" ", item));
            }
        }

        void ShowRealEstate()
        {
            comboBoxRealEstate.Items.Clear();
            foreach (RealEstateSet realEstateSet in Program.wtfDb.RealEstateSet)
            {
                string[] item = { realEstateSet.ID.ToString() + ".", realEstateSet.Address_City + ",", 
                    realEstateSet.Address_Street + ",", "д. " +realEstateSet.Address_House + ",", 
                    "кв. "+ realEstateSet.Address_Number };
                comboBoxRealEstate.Items.Add(string.Join(" ", item));
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxRieltor.SelectedItem != null && comboBoxClients.SelectedItem != null &&
                comboBoxRealEstate.SelectedItem != null && textBoxPrice.Text != "")
            {
                SupplySet supply = new SupplySet();
                supply.IDRieltor = Convert.ToInt32(comboBoxRieltor.SelectedItem.ToString().Split('.')[0]);
                supply.IDClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                supply.IDRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                supply.Prise = Convert.ToInt64(textBoxPrice.Text);
                Program.wtfDb.SupplySet.Add(supply);
                Program.wtfDb.SaveChanges();
                ShowSupplySet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ShowSupplySet()
        {
            listViewSupplySet.Items.Clear();
            foreach (SupplySet supply in Program.wtfDb.SupplySet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {

                    supply.IDRieltor.ToString(),
                    supply.RieltorSet.LastName+""+supply.RieltorSet.FirstName+""+supply.RieltorSet.MiddleName,
                    supply.IDClient.ToString(),
                    supply.ClientsSet.LastName+""+supply.ClientsSet.FirstName+""+supply.ClientsSet.MiddleName,
                    supply.IDRealEstate.ToString(), 
                    "г. "+supply.RealEstateSet.Address_City+", ул. "+supply.RealEstateSet.Address_Street+", д. "+
                    supply.RealEstateSet.Address_House+", кв. "+supply.RealEstateSet.Address_Number,
                    supply.Prise.ToString()
                });
                item.Tag = supply;
                listViewSupplySet.Items.Add(item);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1)
            {
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                supply.IDRieltor = Convert.ToInt32(comboBoxRieltor.SelectedItem.ToString().Split('.')[0]);
                supply.IDClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                supply.IDRealEstate = Convert.ToInt32(comboBoxRealEstate.SelectedItem.ToString().Split('.')[0]);
                supply.Prise = Convert.ToInt64(textBoxPrice.Text);
                Program.wtfDb.SupplySet.Add(supply);
                Program.wtfDb.SaveChanges();
                ShowSupplySet();

            }
        }

        private void listViewSupplySet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count==1)
            {
                SupplySet supply =listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                comboBoxRieltor.SelectedIndex = comboBoxRieltor.FindString(supply.IDRieltor.ToString());
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(supply.IDClient.ToString());
                comboBoxRealEstate.SelectedIndex = comboBoxRealEstate.FindString(supply.IDRealEstate.ToString());
                textBoxPrice.Text = supply.Prise.ToString();
            }
            else
            {
                comboBoxRieltor.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstate.SelectedItem = null;
                textBoxPrice.Text = "";
            }

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewSupplySet.SelectedItems.Count==1)
                {
                    SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                    Program.wtfDb.SupplySet.Remove(supply);
                    Program.wtfDb.SaveChanges();
                    ShowSupplySet();
                }
                comboBoxRieltor.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstate.SelectedItem = null;
                textBoxPrice.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxRieltor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormSupply_Load(object sender, EventArgs e)
        {

        }
    }
}
