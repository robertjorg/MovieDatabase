﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.DataModel
{
    using System.Linq;

    using Movie.Classes;

    public interface IMovieRepository
    {
        IQueryable<User> GetAllUsers();

        User GetSingleUser(int id);

        IQueryable<MoviesOwned> GetMoviesForUser(int userId);

        MoviesOwned GetSingleMovieOwned(int userId, int id);

        MovieTitles GetTitleForMovie(int movieTitlesId);

        IQueryable<MovieTitles> GetMovieTitles();

        MovieTitles GetTitleForOwnedMovie(int userId, int moviesOwnedId, int id);

        IQueryable<StorageType> GetStorageType();

        StorageType GetSingleStorageType(int id);

        // Inserts
        bool Add(MovieTitles title);

        bool Add(User user);

        // Deletes
        bool DeleteMovieTitle(int id);

        bool DeleteUser(int id);

        // Save
        bool SaveAll();

    }
}
