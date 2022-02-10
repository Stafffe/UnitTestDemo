using System.ComponentModel.DataAnnotations;
using UnitTestDemo.Interfaces;

namespace UnitTestDemo.Business
{
    public class FruitBusiness : IFruitBusiness
    {
        private readonly IDatabaseProvider _databaseProvider;

        public FruitBusiness(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        //Get the fruit with the id. If the name of the fruit is 5 letters or longer, reverse the name before returning
        public string GetFruit(int id)
        {
            if (id < 0)
                throw new ValidationException("Bad id. Must be a positive value");

            string fruit = _databaseProvider.GetFruitFromDatabase(id);

            if(string.IsNullOrWhiteSpace(fruit))
                throw new ValidationException($"Bad id. Could not find a fruit with id {id}.");

            if (fruit.Length >= 5)
                return new string(fruit.Reverse().ToArray());
            else
                return fruit;
        }
    }
}
