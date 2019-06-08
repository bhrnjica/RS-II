using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//This is modified example from the official ML.NET Samples repository: https://github.com/dotnet/machinelearning-samples
namespace PreporukaFilovaDemo
{
    /// <summary>
    /// Program implementira sistem preporuke na osnovu skupa korisnika i filmova. Model masinskog učenja treniran je 
    /// na skupu za treniranje koji se sastoji od 100 000 ocjena koji je napravilo 610 korisnika nad 9568 fimova.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //STEP 1: Formiranje objekta tipa MLContext za dijeljenje kroz sve faze izgradnje ml modela 
            MLContext mlcontext = new MLContext();

            // STEP 2: Ucitavanje podataka za treniranje 'movie recommendation' modela
            //The schema for training data is defined by type 'TInput' in LoadFromTextFile<TInput>() method.
            IDataView trainingDataView = mlcontext.Data.LoadFromTextFile<MovieRating>("../../../../data/recommendation-ratings-train.csv", hasHeader: true, separatorChar: ',');

            //Za analizu podataka pogodno je koristiti DataView extenziju unutar ML.NET
            //var difUsers = trainingDataView.GetColumn<float>("movieId").Distinct().Count();

            //STEP 3: Transformacija podataka enkodiranjem dva "featurea" userId i movieID. Ove enkodirane ulazne varijable 
            // u ml algoritmu ce se tretirati kao ulaz za treniranje Matrice Faktorizacije
            var dataProcessingPipeline = mlcontext.Transforms.Conversion.MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: nameof(MovieRating.userId))
                           .Append(mlcontext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: nameof(MovieRating.movieId)));

            //Podesavanje opcija za Trenera Matrice Faktorizacije
            //Matrica Faktorizacije se nalazi u posebnom NuGet paketu 
            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = "userIdEncoded";
            options.MatrixRowIndexColumnName = "movieIdEncoded";
            options.LabelColumnName = "Label";
            options.NumberOfIterations = 20;
            options.ApproximationRank = 100;

            //STEP 4: Definisanje trenera za sistem preporuke aziran na matrici faktorizacije 
            var trainingPipeLine = dataProcessingPipeline.Append(mlcontext.Recommendation().
                Trainers.MatrixFactorization(options));

            //STEP 5: Treniranje modela sa podacima za treniranje
            Console.WriteLine("=============== Treniranje modela preporuke ===============");
            ITransformer model = trainingPipeLine.Fit(trainingDataView);


            //STEP 6: Izračunavanje performansi modela nakon  
            Console.WriteLine("=============== Evaluacija modela ===============");
            IDataView testDataView = mlcontext.Data.LoadFromTextFile<MovieRating>("../../../../data/recommendation-ratings-test.csv", hasHeader: true, separatorChar: ',');
            var prediction = model.Transform(testDataView);
            var metrics = mlcontext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
            Console.WriteLine("Metrika za evaluaciju modela: KorijenSrednjeKvadratneGreske:" + metrics.RootMeanSquaredError);

            //STEP 7: Testiranje treniranog modela na primjeru filma za određenog korisnika
            var predictionengine = mlcontext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);

            /*
             * Predviđanje ocjene za film koju korisnik može da da je izmedju 1 i 5. Viša ocjena predstavlja bolju preporuku.
             */
            /* Make a single movie rating prediction, the scores are for a particular user and will range from 1 - 5. 
               The higher the score the higher the likelyhood of a user liking a particular movie.
               You can recommend a movie to a user if say rating > 3.5.*/
            var movieratingprediction = predictionengine.Predict(
                new MovieRating()
                {
                    //primjer predikcije za  userId = 6, movieId = 10 (GoldenEye)
                    userId = 6,
                    movieId = 10
                }
                );

            Movie movieService = new Movie();
            Console.WriteLine("Za korisnikaId:" + 6 + " predikcija ocjene (1 - 5 stars) za film:" + Get(10).movieTitle + " je:" + Math.Round(movieratingprediction.Score, 1));

            //STEP 8: Priprema za isporuku
            //model se zapisuje u obliku datoteke
            mlcontext.Model.Save(model,trainingDataView.Schema, "../../../../savedModel/movierecommender.zip");

            Console.WriteLine("=============== Kraj ML procesa, hit any key to finish ===============");
            Console.ReadLine();


            //
            Console.WriteLine("Hello World!");
        }

        #region Helpers for Moview data

        public static Movie Get(int id)
        {
            Lazy<List<Movie>> _movies = new Lazy<List<Movie>>(() => 
                LoadMovieData("../../../../data/recommendation-movies.csv"));
            return _movies.Value.Single(m => m.movieId == id);
        }

        private static List<Movie> LoadMovieData(String moviesdatasetpath)
        {
            var result = new List<Movie>();
            Stream fileReader = File.OpenRead(moviesdatasetpath);
            StreamReader reader = new StreamReader(fileReader);
            try
            {
                bool header = true;
                int index = 0;
                var line = "";
                while (!reader.EndOfStream)
                {
                    if (header)
                    {
                        line = reader.ReadLine();
                        header = false;
                    }
                    line = reader.ReadLine();
                    string[] fields = line.Split(',');
                    int movieId = Int32.Parse(fields[0].ToString().TrimStart(new char[] { '0' }));
                    string movieTitle = fields[1].ToString();
                    result.Add(new Movie() { movieId = movieId, movieTitle = movieTitle });
                    index++;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }

            return result;
        }
        #endregion
    }
}
