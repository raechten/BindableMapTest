using System.Collections.ObjectModel;
using BindableMapTest.Models;
using BindableMapTest.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System;

namespace BindableMapTest.ViewModels
{
	public class MapViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private ObservableCollection<IMapModel> _monkeys = new ObservableCollection<IMapModel>();
		public ObservableCollection<IMapModel> Monkeys 
		{ 
			get { return _monkeys; } 
			set 
			{ 
				_monkeys = value;

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Monkeys"));
				}
			}
		}

		private ICommand _showDetailCommand;
		public ICommand ShowDetailCommand
		{
			get { return _showDetailCommand = _showDetailCommand ?? new Command(m => ShowDetail((IMapModel)m)); }
		}

		private ICommand _loadMonkeysCommand;
		public ICommand LoadMonkeysCommand
		{
			get { return _loadMonkeysCommand = _loadMonkeysCommand ?? new Command(LoadMonkeys); }
		}

		private ICommand _addMonkeyCommand;
		public ICommand AddMonkeyCommand
		{
			get { return _addMonkeyCommand = _addMonkeyCommand ?? new Command(AddMonkey); }
		}

		private void ShowDetail(IMapModel selectedItem)
		{

		}

		private void LoadMonkeys()
		{
			Monkeys.Add(new Monkey {
				Name = "Baboon",
				Location = new Location { Latitude = 34.027897, Longitude = -118.301869 },
				Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
			});

			Monkeys.Add(new Monkey {
				Name = "Capuchin Monkey",
				Location = new Location { Latitude = 34.047797, Longitude = -118.321869 },
				Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
			});

			Monkeys.Add(new Monkey {
				Name = "Blue Monkey",
				Location = new Location { Latitude = 34.007897, Longitude = -118.300069 },
				Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
			});


			Monkeys.Add(new Monkey {
				Name = "Squirrel Monkey",
				Location = new Location { Latitude = 34.107897, Longitude = -118.292869 },
				Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
			});
		}

		private void AddMonkey()
		{
			var rnd = new Random();
			Monkeys.Add(new Monkey
			{
				Name = "Monkey " + rnd.Next(1000),
				Location = new Location { Latitude = 34.0 + rnd.NextDouble()/10.0, Longitude = -118.0 + rnd.NextDouble()/10.0 },
				Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
				ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
			});
		}
	}
}

