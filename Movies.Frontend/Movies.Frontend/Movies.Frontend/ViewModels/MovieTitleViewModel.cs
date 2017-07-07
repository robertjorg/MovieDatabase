using Movies.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.ViewModels
{
    public class MovieTitleViewModel : BaseViewModel
    {
        private string titleReleaseDate;

        private string title;

        private string movieDesc;

        private DateTime? releaseDate;

        private string imdbUrl;

        private DateTime dateModified;

        private DateTime dateAdded;

        private string storageType;

        public MovieTitleViewModel() { }

        public MovieTitleViewModel(MovieTitle movieTitle)
        {
            this.Id = movieTitle.Id;
            this.titleReleaseDate = movieTitle.TitleReleaseDate;
            this.title = movieTitle.Title;
            this.movieDesc = movieTitle.MovieDesc;
            this.releaseDate = movieTitle.ReleaseDate;
            this.imdbUrl = movieTitle.ImdbUrl;
            this.storageType = movieTitle.StorageType;
            this.dateModified = movieTitle.DateModified;
            this.dateAdded = movieTitle.DateAdded;
        }

        public int Id { get; set; }

        public string TitleReleaseDate
        {
            get
            {
                return this.titleReleaseDate;
            }
            set
            {
                this.titleReleaseDate = Title + ReleaseDate.ToString() + StorageType;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if(this.title != value)
                {
                    this.title = value;
                    this.OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string MovieDesc
        {
            get
            {
                return this.movieDesc;
            }
            set
            {
                if(this.movieDesc != value)
                {
                    this.movieDesc = value;
                    this.OnPropertyChanged(nameof(MovieDesc));
                }
            }
        }

        public string StorageType
        {
            get
            {
                return this.storageType;
            }
            set
            {
                if(this.storageType != value)
                {
                    this.storageType = value;
                    this.OnPropertyChanged(nameof(StorageType));
                }
            }
        }

        public DateTime? ReleaseDate
        {
            get
            {
                if(this.releaseDate == null)
                {
                    return DateTime.Today;
                }
                return this.releaseDate;
            }
            set
            {
                if(this.releaseDate != value)
                {
                    this.releaseDate = value;
                    this.OnPropertyChanged(nameof(ReleaseDate));
                }
            }

        }

        public string ImdbUrl
        {
            get
            {
                return this.imdbUrl;
            }
            set
            {
                if(this.imdbUrl != value)
                {
                    this.imdbUrl = value;
                    this.OnPropertyChanged(nameof(ImdbUrl));
                }
            }
        }

        public DateTime DateModified
        {
            get
            {
                return this.dateModified;
            }
            set
            {
                if(this.dateModified != value || this.dateModified < DateTime.Now)
                {
                    this.dateModified = DateTime.Now;
                    this.OnPropertyChanged(nameof(DateModified));
                }
            }
        }

        public DateTime DateAdded
        {
            get
            {
                return this.dateAdded;
            }
            set
            {
                if (this.dateAdded == null)
                {
                    this.dateAdded = DateTime.Now;
                    this.OnPropertyChanged(nameof(DateAdded));
                }
            }
        }
    }
}
