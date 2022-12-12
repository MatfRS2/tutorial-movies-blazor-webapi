﻿
using MoviesMauiApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesMauiApp.ViewModels
{
    public class FilmGetDto
    {
        public int FilmId { get; set; }
        public string? Naslov { get; set; }
        public DateTime DatumPocetkaPrikazivanja { get; set; }
        public int ZanrId { get; set; }
        public decimal Ulozeno { get; set; }
        public Zanr Zanr { get; set; }

    }
}