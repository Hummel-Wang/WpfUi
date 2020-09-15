using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp3.Commands;
using WpfApp3.Models;

namespace WpfApp3.ViewModels
{
    public class MenuTreeViewModel : ViewModelBase
    {
        private object _selectedItem;

        public ObservableCollection<MovieCategory> MovieCategories { get; }

        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public MenuTreeViewModel()
        {
            Movie[] movies = new Movie[3];
            for(int i =0; i <3; i++)
            {
                movies[i] = new Movie(i.ToString(),"Demo" + i.ToString(), "dir" + i.ToString());
            }
            MovieCategories = new ObservableCollection<MovieCategory>
            {
                new MovieCategory("1","Action",movies),
                new MovieCategory("2","Comedy",movies),
                new MovieCategory("3","Demo", null)
            };
        }
    }

    public class MovieCategory
    {
        public MovieCategory(string id,string name, Movie[] movies)
        {
            Id = id;
            Name = name;
            Movies = movies == null ? new ObservableCollection<Movie>() : new ObservableCollection<Movie>(movies);
        }

        public string Id { get; }
        public string Name { get; }

        public ObservableCollection<Movie> Movies { get; }
    }


    public class Movie
    {
        public Movie(string id, string name, string director)
        {
            Id = id;
            Name = name;
            Director = director;
        }

        public string Id { get; }
        public string Name { get; }

        public string Director { get; }
    }

}
