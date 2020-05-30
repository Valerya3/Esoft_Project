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
    public partial class FormRieltor : Form
    {
        public FormRieltor()
        {
            InitializeComponent();
            ShowRieltor();
        }

        private void FormRieltor_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // создаем новый экземпляр класса Клиент
            RieltorSet rieltorSet = new RieltorSet();
            // делаем ссылку на объект, который находится в textBox-ax
            rieltorSet.FirstName = textBoxFirstName.Text;
            rieltorSet.MiddleName = textBoxMiddleName.Text;
            rieltorSet.LastName = textBoxLastName.Text;
            if (textBoxDealShare.Text != "")
            {
                rieltorSet.DealShare = Convert.ToInt32(textBoxDealShare.Text);
            }
            // добавляем в таблицу ClientsSet нового клиента clientSet
            Program.wtfDb.RieltorSet.Add(rieltorSet);
            //сохраняем изменения в модели wftDb
            Program.wtfDb.SaveChanges();
            ShowRieltor();
        }
        void ShowRieltor()
        {
            //придварительно отчищаем listView
            listViewRieltor.Items.Clear();
            //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (RieltorSet rieltorSet in Program.wtfDb.RieltorSet)
            {
                //создаем новый элемент в listView
                //для этого создаем новый массив строк 
                ListViewItem item = new ListViewItem(new string[]
                {
                    //указываем необходимые поля 
                    rieltorSet.ID.ToString(),rieltorSet.FirstName,rieltorSet.MiddleName,
                    rieltorSet.LastName, rieltorSet.DealShare.ToString()
                });
                //указываем по какому тегу будем брать элементы 
                item.Tag = rieltorSet;
                //добавляем элементы в listView для отображения
                listViewRieltor.Items.Add(item);
            }
            //выравниваем колонки в listView
            listViewRieltor.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //условие, если в listView выбран 1 элемент
            if (listViewRieltor.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                RieltorSet rieltorSet = listViewRieltor.SelectedItems[0].Tag as RieltorSet;
                //указываем, что может быть изменено
                rieltorSet.FirstName = textBoxFirstName.Text;
                rieltorSet.MiddleName = textBoxMiddleName.Text;
                rieltorSet.LastName = textBoxLastName.Text;
                rieltorSet.DealShare = Convert.ToInt32(textBoxDealShare.Text);
                //сохраняем изменения в модели wftDb
                Program.wtfDb.SaveChanges();
                //отображение в listView
                ShowRieltor();
            }
        }

        private void listViewRieltor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //условие, если выбран 1 элемент 
            if (listViewRieltor.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу 
                RieltorSet rieltorSet = listViewRieltor.SelectedItems[0].Tag as RieltorSet;
                // указываем, что может быть изменено
                textBoxFirstName.Text = rieltorSet.FirstName;
                textBoxMiddleName.Text = rieltorSet.MiddleName;
                textBoxLastName.Text = rieltorSet.LastName;
                textBoxDealShare.Text = rieltorSet.DealShare.ToString();
            }
            else
            {
                //условие, иначе, ели не выран ни один элемент , то задаем пустые поля
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            // попробуем совершить действие 
            try
            {
                //если выбран 1 элемент из listViev
                if (listViewRieltor.SelectedItems.Count == 1)
                {
                    //ищем этот элимент, сверяем его 
                    RieltorSet rieltorSet = listViewRieltor.SelectedItems[0].Tag as RieltorSet;
                    //удаляем из модели и базы данных
                    Program.wtfDb.RieltorSet.Remove(rieltorSet);
                    //сохраняем изменения 
                    Program.wtfDb.SaveChanges();
                    //отображаем обновленный список
                    ShowRieltor();
                }
                //отчищаем textBox-ы
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";
            }
            //если возникае ошибка, выводим всплывающее сообщение
            catch
            {
                //вызываем метод для всплывающего окна
                MessageBox.Show("Невозможно удалить, запись используется!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
