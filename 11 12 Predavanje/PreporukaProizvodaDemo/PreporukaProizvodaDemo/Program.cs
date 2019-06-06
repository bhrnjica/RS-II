using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;

namespace PreporukaProizvodaDemo
{
    public class Copurchase_prediction
    {
        public float Score { get; set; }
    }

    public class ProductEntry
    {
        [KeyType(count: 262111)]
        public uint ProductID { get; set; }

        [KeyType(count: 262111)]
        public uint CoPurchaseProductID { get; set; }
    }

    /// <summary>
    /// Program implementira sistem preporuke za proizvode na osnovu frekvencije prodaje. Koristi matričnu faktorizaciju
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //STEP 1: Formiranje objekta tipa MLContext za dijeljenje kroz sve faze izgradnje ml modela  
            MLContext mlContext = new MLContext();

            // STEP 2: Ucitavanje podataka za treniranje koje su definisane preko tipa 'TInput' u LoadFromTextFile<TInput>() metodi.
            //        Ne zaboravite promjeniti dateteku amazon0302.txt sa onom koju možete peruzeti sa https://snap.stanford.edu/data/amazon0302.html
            var traindata = mlContext.Data.LoadFromTextFile(path: "../../../../data/Amazon0302.txt",
                                                      columns: new[]
                                                                {
                                                                    new TextLoader.Column("Label", DataKind.Single, 0),
                                                                    new TextLoader.Column(name:nameof(ProductEntry.ProductID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(0) }, keyCount: new KeyCount(262111)),
                                                                    new TextLoader.Column(name:nameof(ProductEntry.CoPurchaseProductID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(1) }, keyCount: new KeyCount(262111))
                                                                },
                                                      hasHeader: true,
                                                      separatorChar: '\t');

            //STEP 3: S obzirom da su podaci vec enkodirani nije potrebna dodatno enkodiranje, pa je samo potrebno navesti koji ML algoritam koristi sto je u ovom slučaju 
            //MatrxiFactorizationTrainer sa nekoliko dodatnih parametara:
            // LossFunction, Alpa, Lambda i nekoliko drugih poput K i C parametara a koji su prikazani ispode prilikom poziva trenera. 
            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = nameof(ProductEntry.ProductID);
            options.MatrixRowIndexColumnName = nameof(ProductEntry.CoPurchaseProductID);
            options.LabelColumnName = "Label";
            options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
            options.Alpha = 0.01;
            options.Lambda = 0.025;
            options.C = 0.00001;

            //Step 4: POzivanje MatrixFactorization trenera sa argumentima sadržanim u objektu options.
            var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);

            //STEP 5: Treniranje modela sa podacima za treniranje
            ITransformer model = est.Fit(traindata);

            //STEP 6: Formiranje engina za predviđanje ocjene proizvoda 63 koji bi se kupio sa proizvodom 3.
            //        Veća ocjena znači veća vjerojatnoća da se dotični proizvod kupi uz već kupljeni proizvod 
            var predictionengine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
            var prediction = predictionengine.Predict(
                new ProductEntry()
                {
                    ProductID = 3,
                    CoPurchaseProductID = 63
                });

            Console.WriteLine("\n Za ProductID = 3 i CoPurchaseProductID = 63 predikcija ocjene je " + Math.Round(prediction.Score, 1));
            Console.WriteLine("=============== Kraj proces, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
