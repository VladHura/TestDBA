using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using TestDBA.Models;

namespace TestDBA.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int search_phone_number;
        private ObservableCollection<Abonent> abonents;
        private ObservableCollection<Address> addresses;
        private ObservableCollection<PhoneNumber> phone_numbers;
        private ObservableCollection<Streets> streets;
        private ObservableCollection<PhoneNumber> search_phone_numbers;
        private ICommand opensearchCommand;
        private ICommand openstreetsCommand;
        private ICommand searchCommand;
        private ICommand unloadCommand;
        public MainViewModel()
        {
            streets = new ObservableCollection<Streets>
            {
                new Streets { Street = "Означенная улица" },
                new Streets { Street = "Ленина улица" },
                new Streets { Street = "Петрова улица" }
            };
            addresses = new ObservableCollection<Address>
            {
                new Address { Street = Streets[0], Building = 1, Apartment = 3 },
                new Address { Street = Streets[1], Building = 3, Apartment = 22 },
                new Address { Street = Streets[2], Building = 10, Apartment = 17 }
            };
            abonents = new ObservableCollection<Abonent>
            {
                new Abonent { Address = Addresses[0], FirstName = "Олег", SecondName = "Иванов", LastName = "Олегович" },
                new Abonent { Address = Addresses[1], FirstName = "Владислав", SecondName = "Хураськин", LastName = "Эдуардович" },
                new Abonent { Address = Addresses[2], FirstName = "Петр", SecondName = "Первый", LastName = "Иванов" }
            };
            phone_numbers = new ObservableCollection<PhoneNumber>
            {
                new PhoneNumber { Abonent = Abonents[0], HomeNumber = 12142, MobileNumber = 424444, WorkNumber = 0000123 },
                new PhoneNumber { Abonent = Abonents[1], HomeNumber = 432521, MobileNumber = 96765, WorkNumber = 3241156 },
                new PhoneNumber { Abonent = Abonents[2], HomeNumber = 127773, MobileNumber = 431112, WorkNumber = 696421 }
            };
            search_phone_numbers = phone_numbers;
        }
        public ICommand OpenSearchWindowCommand
        {
            get
            {
                return opensearchCommand ?? (opensearchCommand = new RelayCommand(param => OpenSearchWindow()));
            }
        }
        public void OpenSearchWindow()
        {
            NumberSearchWindow searchWindow = new NumberSearchWindow();
            searchWindow.DataContext = this;
            searchWindow.ShowDialog();
        }
        public ICommand OpenStreetsWindowCommand
        {
            get
            {
                return openstreetsCommand ?? (openstreetsCommand = new RelayCommand(param => OpenStreetsWindow()));
            }
        }
        public void OpenStreetsWindow()
        {
            StreetsWindow streetsWindow = new StreetsWindow();
            streetsWindow.DataContext = this;
            streetsWindow.ShowDialog();
        }
        public ICommand SearchPhoneNumberCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand(param => SearchPhoneNumber()));
            }
        }
        public void SearchPhoneNumber()
        {
            search_phone_numbers = new ObservableCollection<PhoneNumber>();
            foreach (var number in phone_numbers)
            {
                if (number.MobileNumber == search_phone_number || number.WorkNumber == search_phone_number || number.HomeNumber == search_phone_number)
                {
                    search_phone_numbers.Add(number);
                }
            }
            if (search_phone_numbers.Count > 0)
            {
                MessageBox.Show("Номер успешно найден", "Поиск по номеру");
                OnPropertyChanged(nameof(Search_Phone_Numbers));
            }
            else
            {
                MessageBox.Show("Номер не найден", "Поиск по номеру");
            }
        }
        public ICommand UnloadCSVCommand
        {
            get
            {
                return unloadCommand ?? (unloadCommand = new RelayCommand(param => UnloadCSV()));
            }
        }
        public void UnloadCSV()
        {
            using (StreamWriter streamWriter = new StreamWriter($"report_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.csv"))
            {
                streamWriter.Write("FirstName,SecondName,LastName,Street,Building,WorkNumber,MobileNumber,HomeNumber");
                streamWriter.WriteLine();
                foreach (var PhoneNumber in PhoneNumbers)
                {
                    streamWriter.Write(PhoneNumber.Abonent.FirstName + ",");
                    streamWriter.Write(PhoneNumber.Abonent.SecondName + ",");
                    streamWriter.Write(PhoneNumber.Abonent.LastName + ",");
                    streamWriter.Write(PhoneNumber.Abonent.Address.Street.Street + ",");
                    streamWriter.Write(PhoneNumber.Abonent.Address.Building + ",");
                    streamWriter.Write(PhoneNumber.WorkNumber + ",");
                    streamWriter.Write(PhoneNumber.MobileNumber + ",");
                    streamWriter.Write(PhoneNumber.HomeNumber);
                    streamWriter.WriteLine();
                }
            }
            MessageBox.Show("Таблица выгружена в CSV файл", "Выгрузка CSV");
        }
        public int Search_Phone_Number
        {
            get { return search_phone_number; }
            set
            {
                search_phone_number = value;
                OnPropertyChanged(nameof(Search_Phone_Number));
            }
        }
        public ObservableCollection<PhoneNumber> Search_Phone_Numbers
        {
            get { return search_phone_numbers; }
            set
            {
                search_phone_numbers = value;
                OnPropertyChanged(nameof(Search_Phone_Numbers));
            }
        }
        public ObservableCollection<Abonent> Abonents
        {
            get { return abonents; }
            set
            {
                abonents = value;
                OnPropertyChanged(nameof(Abonents));
            }
        }
        public ObservableCollection<Address> Addresses
        {
            get { return addresses; }
            set
            {
                addresses = value;
                OnPropertyChanged(nameof(Addresses));
            }
        }
        public ObservableCollection<PhoneNumber> PhoneNumbers
        {
            get { return phone_numbers; }
            set
            {
                phone_numbers = value;
                OnPropertyChanged(nameof(PhoneNumbers));
            }
        }
        public ObservableCollection<Streets> Streets
        {
            get { return streets; }
            set
            {
                streets = value;
                OnPropertyChanged(nameof(Streets));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
