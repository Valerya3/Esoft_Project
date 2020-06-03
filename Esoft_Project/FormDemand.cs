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
    public partial class FormDemand : Form
    {
        public FormDemand()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            ShowClients();
            ShowRieltor();
            ShowDemandSet();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0)
            {
                listViewApartament.Visible = true;
                labelMaxRooms.Visible = true;
                textBoxMaxRooms.Visible = true;
                labelMinRooms.Visible = true;
                textBoxMinRooms.Visible = true;
                labelMinFloor.Visible = true;
                textBoxMinFloor.Visible = true;
                labelMaxFloor.Visible = true;
                textBoxMaxFloor.Visible = true;

                listViewHouse.Visible = false;
                listViewLand.Visible = false;
                labelMaxFloors.Visible = false;
                textBoxMaxFloors.Visible = false;
                labelMinFloors.Visible = false;
                textBoxMinFloors.Visible = false;

                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text= "";
                textBoxMaxFloor.Text = "";
                textBoxMinFloor.Text = "";
                textBoxMaxPrise.Text = "";
                textBoxMinPrise.Text = "";
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                listViewHouse.Visible = true;
                labelMaxFloors.Visible = true;
                textBoxMaxFloors.Visible = true;
                labelMinFloors.Visible = true;
                textBoxMinFloors.Visible = true;
                labelMaxRooms.Visible = true;
                textBoxMaxRooms.Visible = true;
                labelMinRooms.Visible = true;
                textBoxMinRooms.Visible = true;


                listViewApartament.Visible = false;
                listViewLand.Visible = false;
                labelMinFloor.Visible = false;
                textBoxMinFloor.Visible = false;
                labelMaxFloor.Visible = false;
                textBoxMaxFloor.Visible = false;

                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinFloors.Text = "";
                textBoxMaxFloors.Text = "";
                textBoxMaxPrise.Text = "";
                textBoxMinPrise.Text = "";
            }
            else if (comboBoxType.SelectedIndex==2)
            {
                listViewLand.Visible = true;

                listViewApartament.Visible = false;
                listViewHouse.Visible = false;

                labelMinFloor.Visible = false;
                textBoxMinFloor.Visible = false;
                labelMaxFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                labelMaxFloors.Visible = false;
                textBoxMaxFloors.Visible = false;
                labelMinFloors.Visible = false;
                textBoxMinFloors.Visible = false;
                labelMaxRooms.Visible = false;
                textBoxMaxRooms.Visible = false;
                labelMinRooms.Visible = false;
                textBoxMinRooms.Visible = false;

                textBoxMaxPrise.Text = "";
                textBoxMinPrise.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
            }
            
        }


        void ShowRieltor()
        {
            comboBoxRieltor.Items.Clear();
            foreach(RieltorSet rieltorSet in Program.wtfDb.RieltorSet)
            {
                string[] item = {rieltorSet.ID.ToString() + "." , rieltorSet.FirstName,
                    rieltorSet.MiddleName, rieltorSet.LastName};
                comboBoxRieltor.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            comboBoxClient.Items.Clear();
            foreach (ClientsSet clientsSet in Program.wtfDb.ClientsSet)
            {
                string[] item = { clientsSet.ID.ToString() + ".", clientsSet.FirstName,
                    clientsSet.MiddleName, clientsSet.LastName };
                comboBoxClient.Items.Add(string.Join(" ", item));
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxRieltor.SelectedItem != null && comboBoxClient != null)
            {
                DemandSet demand = new DemandSet();
                demand.IDRieltor = Convert.ToInt32(comboBoxRieltor.SelectedItem.ToString().Split('.')[0]);
                demand.IDClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                demand.MinPrise = Convert.ToInt64(textBoxMinPrise.Text);
                demand.MaxPrise = Convert.ToInt64(textBoxMaxPrise.Text);
                demand.MinArea = Convert.ToInt64(textBoxMinArea.Text);
                demand.MaxArea = Convert.ToInt64(textBoxMaxArea.Text);

                if (comboBoxType.SelectedIndex == 0)
                {
                    demand.Type = 0;
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    demand.Type = 1;
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                }
                else
                {
                    demand.Type = 2;
                }
                Program.wtfDb.DemandSet.Add(demand);
                Program.wtfDb.SaveChanges();
                ShowDemandSet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void ShowDemandSet()
        {
            listViewApartament.Items.Clear();
            listViewHouse.Items.Clear();
            listViewLand.Items.Clear();
            foreach (DemandSet demand in Program.wtfDb.DemandSet)
            {
                if (demand.Type == 0)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {

                        demand.IDRieltor.ToString(),
                        demand.RieltorSet.LastName+""+demand.RieltorSet.FirstName+""+demand.RieltorSet.MiddleName,
                        demand.IDClient.ToString(),
                        demand.ClientsSet.LastName+""+demand.ClientsSet.FirstName+""+demand.ClientsSet.MiddleName,
                        demand.MinPrise.ToString(),
                        demand.MaxPrise.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        demand.MinRooms.ToString(),
                        demand.MaxRooms.ToString(),
                        demand.MinFloor.ToString(),
                        demand.MaxFloor.ToString(),
                    });
                    item.Tag = demand;
                    listViewApartament.Items.Add(item);
                }
               else if(demand.Type == 1)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.IDRieltor.ToString(),
                        demand.RieltorSet.LastName+""+demand.RieltorSet.FirstName+""+demand.RieltorSet.MiddleName,
                        demand.IDClient.ToString(),
                        demand.ClientsSet.LastName+""+demand.ClientsSet.FirstName+""+demand.ClientsSet.MiddleName,
                        demand.MinPrise.ToString(),
                        demand.MaxPrise.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        demand.MinFloors.ToString(),
                        demand.MaxFloors.ToString(),
                        demand.MinRooms.ToString(),
                        demand.MaxRooms.ToString(),
                    });
                    item.Tag = demand;
                    listViewHouse.Items.Add(item);
                }
                else 
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.IDRieltor.ToString(),
                        demand.RieltorSet.LastName+""+demand.RieltorSet.FirstName+""+demand.RieltorSet.MiddleName,
                        demand.IDClient.ToString(),
                        demand.ClientsSet.LastName+""+demand.ClientsSet.FirstName+""+demand.ClientsSet.MiddleName,
                        demand.MinPrise.ToString(),
                        demand.MaxPrise.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                    });
                    item.Tag = demand;
                    listViewLand.Items.Add(item);
                }
                listViewApartament.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewHouse.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listViewLand.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex==0)
            {
                if (listViewApartament.SelectedItems.Count==1)
                {
                    DemandSet demand = listViewApartament.SelectedItems[0].Tag as DemandSet;
                    demand.IDRieltor = Convert.ToInt32(comboBoxRieltor.SelectedItem.ToString().Split('.')[0]);
                    demand.IDClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrise = Convert.ToInt64(textBoxMinPrise.Text);
                    demand.MaxPrise = Convert.ToInt64(textBoxMaxPrise.Text);
                    demand.MinArea = Convert.ToInt64(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToInt64(textBoxMaxArea.Text);
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                    Program.wtfDb.SaveChanges();
                    ShowDemandSet();
                }
            }
            else if (comboBoxType.SelectedIndex==1)
            {
                if (listViewHouse.SelectedItems.Count==1)
                {
                    DemandSet demand = listViewHouse.SelectedItems[0].Tag as DemandSet;
                    demand.IDRieltor = Convert.ToInt32(comboBoxRieltor.SelectedItem.ToString().Split('.')[0]);
                    demand.IDClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrise = Convert.ToInt64(textBoxMinPrise.Text);
                    demand.MaxPrise = Convert.ToInt64(textBoxMaxPrise.Text);
                    demand.MinArea = Convert.ToInt64(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToInt64(textBoxMaxArea.Text);
                    demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    Program.wtfDb.SaveChanges();
                    ShowDemandSet();
                }
            }
            else
            {
                if (listViewLand.SelectedItems.Count==1)
                {
                    DemandSet demand = listViewLand.SelectedItems[0].Tag as DemandSet;
                    demand.IDRieltor = Convert.ToInt32(comboBoxRieltor.SelectedItem.ToString().Split('.')[0]);
                    demand.IDClient = Convert.ToInt32(comboBoxClient.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrise = Convert.ToInt64(textBoxMinPrise.Text);
                    demand.MaxPrise = Convert.ToInt64(textBoxMaxPrise.Text);
                    demand.MinArea = Convert.ToInt64(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToInt64(textBoxMaxArea.Text);
                    Program.wtfDb.SaveChanges();
                    ShowDemandSet();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewApartament.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewApartament.SelectedItems[0].Tag as DemandSet;
                        Program.wtfDb.DemandSet.Remove(demand);
                        Program.wtfDb.SaveChanges();
                        ShowDemandSet();
                    }

                    comboBoxRieltor.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxMaxArea.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxRooms.Text = "";
                    textBoxMaxFloor.Text = "";
                    textBoxMinFloor.Text = "";
                    textBoxMaxPrise.Text = "";
                    textBoxMinPrise.Text = "";
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewHouse.SelectedItems.Count==1)
                    {
                        DemandSet demand = listViewHouse.SelectedItems[0].Tag as DemandSet;
                        Program.wtfDb.DemandSet.Remove(demand);
                        Program.wtfDb.SaveChanges();
                        ShowDemandSet();
                    }
                    comboBoxRieltor.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxMaxPrise.Text = "";
                    textBoxMinPrise.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxFloors.Text = "";
                    textBoxMinFloors.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxRooms.Text = "";
                }
                else
                {
                    if (listViewLand.SelectedItems.Count==1)
                    {
                        DemandSet demand = listViewLand.SelectedItems[0].Tag as DemandSet;
                        Program.wtfDb.DemandSet.Remove(demand);
                        Program.wtfDb.SaveChanges();
                        ShowDemandSet();
                    }

                    comboBoxRieltor.SelectedItem = null;
                    comboBoxClient.SelectedItem = null;
                    textBoxMaxPrise.Text = "";
                    textBoxMinPrise.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinArea.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDemand_Load(object sender, EventArgs e)
        {

        }

        private void listViewApartament_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewHouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewLand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
