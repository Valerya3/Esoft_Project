using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormDeal : Form
    {
        public FormDeal()
        {
            InitializeComponent();
            ShowSupply();
            ShowDemand();
            ShowDealSet();
        }

        void ShowSupply()
        {
            comboBoxSupply.Items.Clear();
            foreach(SupplySet supplySet in Program.wtfDb.SupplySet)
            {
                string[] item = {supplySet.ID.ToString()+". ", "Риелтор: "+supplySet.RieltorSet.LastName+supplySet.RieltorSet.FirstName,
                    "Клиент: " + supplySet.ClientsSet.LastName+supplySet.ClientsSet.FirstName};
                comboBoxSupply.Items.Add(string.Join(" ", item));
            }
        }

        void ShowDemand()
        {
            comboBoxDemand.Items.Clear();
            foreach (DemandSet demandSet in Program.wtfDb.DemandSet)
            {
                string[] item = {demandSet.ID.ToString()+". ", "Риелтор: "+demandSet.RieltorSet.LastName+demandSet.RieltorSet.FirstName,
                    "Клиент: " + demandSet.ClientsSet.LastName+demandSet.ClientsSet.FirstName};
                comboBoxDemand.Items.Add(string.Join(" ", item));
            }
        }

        private void comboBoxSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }

        private void comboBoxDemand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }

        void Deductions()
        {
            if (comboBoxSupply.SelectedItem!= null &&comboBoxDemand.SelectedItem!=null)
            {
                SupplySet supplySet = Program.wtfDb.SupplySet.Find(Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]));
                DemandSet demandSet = Program.wtfDb.DemandSet.Find(Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]));
                double customerCompanyDeductions = supplySet.Prise * 0.03;
                if (demandSet.RieltorSet.DealShare!=null)
                {
                    double AgentCustomerDeductions = customerCompanyDeductions * Convert.ToDouble(demandSet.RieltorSet.DealShare) / 100.00;
                    textBoxAgentCustomerDeductions.Text = AgentCustomerDeductions.ToString("0.00");
                }
                else
                {
                    double agentCustomerDeductions = customerCompanyDeductions * 0.45;
                    textBoxAgentCustomerDeductions.Text = agentCustomerDeductions.ToString("0.00");
                }

            }
            else
            {
                textBoxCustomerCompanyDeductions.Text = "";
                textBoxAgentCustomerDeductions.Text = "";
            }
            if (comboBoxSupply.SelectedItem != null)
            {
                SupplySet supplySet = Program.wtfDb.SupplySet.Find(Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]));
                double sellerCompanyDeductions;
                if (supplySet.RealEstateSet.Type==0)
                {
                    sellerCompanyDeductions = 36000 + supplySet.Prise * 0.01;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                else if (supplySet.RealEstateSet.Type == 1)
                {
                    sellerCompanyDeductions = 30000 + supplySet.Prise * 0.01;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                else
                {
                    sellerCompanyDeductions = 30000 + supplySet.Prise * 0.02;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                if (supplySet.RieltorSet.DealShare !=null)
                {
                    double agentSellerDeductions = sellerCompanyDeductions * Convert.ToDouble(supplySet.RieltorSet.DealShare) / 100.00;
                    textBoxAgentSellerDeductions.Text = agentSellerDeductions.ToString("0.00");
                }
                else
                {
                    double agentSellerDeductions = sellerCompanyDeductions * 0.45;
                    textBoxAgentSellerDeductions.Text = agentSellerDeductions.ToString("0.00");
                }
            }
            else
            {
                textBoxSellerCompanyDeductions.Text = "";
                textBoxAgentSellerDeductions.Text = "";
                textBoxCustomerCompanyDeductions.Text = "";
                textBoxAgentCustomerDeductions.Text = "";
            }
        }

        void ShowDealSet()
        {
            listViewDealSet.Items.Clear();
            foreach (DealSet deal in Program.wtfDb.DealSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                   { deal.SupplySet.ClientsSet.LastName,
                    deal.SupplySet.RieltorSet.LastName,
                    deal.DemandSet.ClientsSet.LastName,
                    deal.DemandSet.RieltorSet.LastName,
                    "г. "+deal.SupplySet.RealEstateSet.Address_City+", ул. "+deal.SupplySet.RealEstateSet.Address_Street+
                    ", д. "+ deal.SupplySet.RealEstateSet.Address_House+", кв. "+ deal.SupplySet.RealEstateSet.Address_Number,
                    deal.SupplySet.Prise.ToString()
                   });
                item.Tag = deal;
                listViewDealSet.Items.Add(item);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxDemand.SelectedItem != null && comboBoxSupply.SelectedItem != null)
            {
                DealSet deal = new DealSet();
                deal.IDSlupply = Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]);
                deal.IDDemand = Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]);
                Program.wtfDb.DealSet.Add(deal);
                Program.wtfDb.SaveChanges();
                ShowDealSet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewDealSet.SelectedItems.Count==1)
            {
                DealSet deal = listViewDealSet.SelectedItems[0].Tag as DealSet;
                deal.IDSlupply= Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]);
                deal.IDDemand = Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]);
                Program.wtfDb.SaveChanges();
                ShowDealSet();
            }
        }

        private void listViewDealSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDealSet.SelectedItems.Count==1)
            {
                DealSet deal = listViewDealSet.SelectedItems[0].Tag as DealSet;
                comboBoxSupply.SelectedIndex = comboBoxSupply.FindString(deal.IDSlupply.ToString());
                comboBoxDemand.SelectedIndex = comboBoxDemand.FindString(deal.IDSlupply.ToString());
            }
            else
            {
                comboBoxSupply.SelectedItem = null;
                comboBoxDemand.SelectedItem = null;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if(listViewDealSet.SelectedItems.Count==1)
                {
                    DealSet deal = listViewDealSet.SelectedItems[0].Tag as DealSet;
                    Program.wtfDb.DealSet.Remove(deal);
                    Program.wtfDb.SaveChanges();
                    ShowDealSet();
                }
                comboBoxSupply.SelectedItem = null;
                comboBoxDemand.SelectedItem = null;
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelAgentSellerDeductions_Click(object sender, EventArgs e)
        {

        }
    }

}
