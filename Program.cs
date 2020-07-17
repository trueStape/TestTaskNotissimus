using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestTaskNotissimus
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connection = builder.GetConnectionString("DefaultConnection");
            var path = builder.GetSection("Paths")["PathToXmlFile"];

            var dataBaseHelper = new OfferRepository(connection);
            var xmlDocument = File.ReadAllText(path);

            try
            { 
                await dataBaseHelper.AddOffer(xmlDocument); 
                var offers = await dataBaseHelper.GetOffersAsync();
                Print(offers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ReadKey();
        }

        private static void Print(IEnumerable<OfferEntity> offers)
        {
            foreach (var offer in offers)
            {
                Console.WriteLine(
                    $"id : {offer.Id},\n" +
                    $"type : {offer.Type},\n" +
                    $"url : {offer.Url},\n" +
                    $"price : {offer.Price},\n" +
                    $"currencyId : {offer.CurrencyId},\n" +
                    $"categoryId : {offer.CategoryId},\n" +
                    $"picture : {offer.Picture},\n" +
                    $"delivery : {offer.Delivery},\n" +
                    $"localDeliveryCost : {offer.LocalDeliveryCost},\n" +
                    $"Description : {offer.Description},\n" +
                    $"XML : {offer.CustomFields}\n");
            }
        }
    }
}