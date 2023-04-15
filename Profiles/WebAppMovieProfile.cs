using AutoMapper;
using WebAppMovie.Dtos;
using WebAppMovie.Models;

namespace WebAppMovie.Profiles
{
    public class WebAppMovieProfile: Profile
    {
        public WebAppMovieProfile()
        {
            CreateMap<Movie, MovieReadDto>();
        }
    }
}
