using Movies.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Frontend.ViewModels
{
    public class StorageViewModel : BaseViewModel
    {
        private string storageName;

        private string url;

        private DateTime dateModified;

        private DateTime dateAdded;

        public StorageViewModel() { }

        public StorageViewModel(Storage storage)
        {
            this.Id = storage.Id;
            this.storageName = storage.StorageName;
            this.url = storage.Url;
            this.dateModified = storage.DateModified;
            this.dateAdded = storage.DateAdded;
        }

        public int Id { get; set; }

        public string StorageName
        {
            get
            {
                return this.storageName;
            }
            set
            {
                if (this.storageName != value)
                {
                    this.storageName = value;
                    OnPropertyChanged(nameof(StorageName));
                }
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                if (this.url != value)
                {
                    this.url = value;
                    OnPropertyChanged(nameof(Url));
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
                if (this.dateModified != value || this.dateModified < DateTime.Now)
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
