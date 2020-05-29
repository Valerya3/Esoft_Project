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
    public partial class formClient : Form
    {
        public formClient()
        {
            InitializeComponent();
            ShowClient();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // создаем новый экземпляр класса Клиент
            ClientsSet clientSet = new ClientsSet();
            // делаем ссылку на объект, который находится в textBox-ax
            clientSet.FirstName = textBoxFirstName.Text;
            clientSet.MiddleName = textBoxMiddleName.Text;
            clientSet.LastName = textBoxLastName.Text;
            clientSet.Phone = textBoxPhone.Text;
            clientSet.Email = textBoxEmail.Text;
            // добавляем в таблицу ClientsSet нового клиента clientSet
            Program.wtfDb.ClientsSet.Add(clientSet);
            //сохраняем изменения в модели wftDb
            Program.wtfDb.SaveChanges();
            ShowClient();
        }

        void ShowClient()
        {
            //придварительно отчищаем listView
            listViewClient.Items.Clear();
            //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach(ClientsSet clientsSet in Program.wtfDb.ClientsSet)
            {
                //создаем новый элемент в listView
                //для этого создаем новый массив строк 
                ListViewItem item = new ListViewItem(new string[]
                {
                    //указываем необходимые поля 
                    clientsSet.ID.ToString(),clientsSet.FirstName,clientsSet.MiddleName, 
                    clientsSet.LastName, clientsSet.Phone, clientsSet.Email
                });
                //указываем по какому тегу будем брать элементы 
                item.Tag = clientsSet;
                //добавляем элементы в listView для отображения
                listViewClient.Items.Add(item);
            }
            //выравниваем колонки в listView
            listViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //условие, если в listView выбран 1 элемент
            if (listViewClient.SelectedItems.Count==1)
            {
                //ищем элемент из таблицы по тегу
                ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                //указываем, что может быть изменено
                clientSet.FirstName = textBoxFirstName.Text;
                clientSet.MiddleName = textBoxMiddleName.Text;
                clientSet.LastName = textBoxLastName.Text;
                clientSet.Phone = textBoxPhone.Text;
                clientSet.Email = textBoxEmail.Text;
                //сохраняем изменения в модели wftDb
                Program.wtfDb.SaveChanges();
                //отображение в listView
                ShowClient();
            }
        }

        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //условие, если выбран 1 элемент 
            if(listViewClient.SelectedItems.Count==1)
            {
                //ищем элемент из таблицы по тегу 
                ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                // указываем, что может быть изменено
                textBoxFirstName.Text = clientSet.FirstName;
                textBoxMiddleName.Text=clientSet.MiddleName;
                textBoxLastName.Text = clientSet.LastName;
                textBoxPhone.Text = clientSet.Phone;
                textBoxEmail.Text = clientSet.Email;
            }
            else
            {
                //условие, иначе, ели не выран ни один элемент , то задаем пустые поля
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            // попробуем совершить действие 
            try
            {
                //если выбран 1 элемент из listViev
                if (listViewClient.SelectedItems.Count==1)
                {
                    //ищем этот элимент, сверяем его 
                    ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                    //удаляем из модели и базы данных
                    Program.wtfDb.ClientsSet.Remove(clientSet);
                    //сохраняем изменения 
                    Program.wtfDb.SaveChanges();
                    //отображаем обновленный список
                    ShowClient();
                }
                //отчищаем textBox-ы
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
            //если возникае ошибка, выводим всплывающее сообщение
            catch
            {
                //вызываем метод для всплывающего окна
                MessageBox.Show("Невозможно удалить, запись используется!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelLastName_Click(object sender, EventArgs e)
        {

        }
    }
}
