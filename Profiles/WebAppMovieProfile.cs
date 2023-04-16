﻿using AutoMapper;
using WebAppMovie.Dtos;
using WebAppMovie.Models;

namespace WebAppMovie.Profiles
{
    public class WebAppMovieProfile: Profile
    {
        public WebAppMovieProfile()
        {
            CreateMap<Movie, MovieReadDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();
            CreateMap<Movie, MovieUpdateDto>();
        }
    }
}
