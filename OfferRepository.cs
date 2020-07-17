using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TestTaskNotissimus
{
    public class OfferRepository
    {
        private readonly string _connectionString;
        public OfferRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task AddOffer(string xmlDocument)
        {
            await using (var conn = new SqlConnection(_connectionString))
            {
                await using (var command = new SqlCommand("AddOffer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    await conn.OpenAsync();
                    command.Parameters.AddWithValue("@xmlDocument", xmlDocument);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public async Task<List<OfferEntity>> GetOffersAsync()
        {
            await using var conn = new SqlConnection(_connectionString);
            {
                await using var command = new SqlCommand("GetOffers", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await conn.OpenAsync();
                await using var dataReader = await command.ExecuteReaderAsync();
                var offersList = new List<OfferEntity>();

                if (dataReader.HasRows)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var offer = new OfferEntity
                        {
                            Id = (int)dataReader["Id"],
                            Type = (string)dataReader["Type"],
                            Url = (string)dataReader["Url"],
                            Price = (int)dataReader["Price"],
                            CurrencyId = (string)dataReader["CurrencyId"],
                            CategoryId = (int)dataReader["CategoryId"],
                            Picture = (string)dataReader["Picture"],
                            Delivery = Convert.IsDBNull(dataReader["Delivery"]) ? null : (bool?)dataReader["Delivery"],
                            LocalDeliveryCost = Convert.IsDBNull(dataReader["LocalDeliveryCost"]) ? null : (int?)dataReader["LocalDeliveryCost"],
                            Description = (string)dataReader["Description"],
                            CustomFields = (string)dataReader["CustomFields"]
                        };
                        offersList.Add(offer);
                    }
                }
                return offersList;
            }
        }
    }
}