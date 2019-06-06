using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreporukaFilovaDemo
{
    public class MovieRating
    {
        [LoadColumn(0)]
        public float userId;

        [LoadColumn(1)]
        public float movieId;

        [LoadColumn(2)]
        public float Label;
    }

    public class Movie
    {
        public int movieId;

        public String movieTitle;
    }

    class MovieRatingPrediction
    {
        public float Label;

        public float Score;
    }
}
