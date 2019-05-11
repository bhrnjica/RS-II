using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DataBindingDemo.Models;
using System.Linq;

namespace DataBindingDemo.ViewModels
{
    public class ToDoViewModel : ObservableObject
    {
        private string m_ToDoZadatak;
        private ToDoStavka m_selectedItem= new ToDoStavka();
        private readonly ObservableCollection<ToDoStavka> m_History = new ObservableCollection<ToDoStavka>();

        public string ToDoZadatak
        {
            get { return m_ToDoZadatak; }
            set
            {
                m_ToDoZadatak = value;
                RaisePropertyChangedEvent("ToDoZadatak");
            }
        }

        public ToDoStavka SelectedItem
        {
            get { return m_selectedItem; }
            set
            {
                m_selectedItem = value;
                RaisePropertyChangedEvent("SelectedItem");
            }
        }


        public IEnumerable<ToDoStavka> History
        {
            get { return m_History; }
        }

        public ICommand AddToDoCommand
        {
            get { return new DelegateCommand(AddToDoText); }
        }

        public ICommand DeleteToDoCommand
        {
            get { return new DelegateCommand(DeleteToDoText); }
        }

        private void AddToDoText()
        {
            
            if (string.IsNullOrWhiteSpace(ToDoZadatak))
                return;
            var stavka = new ToDoStavka() { Naziv = ToDoZadatak, Prioritet = "High" };
            AddToHistory(stavka);
            ToDoZadatak = string.Empty;
        }
        private void DeleteToDoText()
        {
            if (SelectedItem != null)
            {
                m_History.Remove(SelectedItem);
            }

        }

        private void AddToHistory(ToDoStavka item)
        {
            var itm = m_History.Where(x => x.Naziv == ToDoZadatak).FirstOrDefault();
            if (itm == null)
            {
                m_History.Add(item);
            } 
            
        }
    }
}
