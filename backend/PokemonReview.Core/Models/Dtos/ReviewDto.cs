﻿namespace PokemonReviewApi.Models.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public byte Rating { get; set; }
    }
}
