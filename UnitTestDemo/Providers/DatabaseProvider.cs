using UnitTestDemo.Interfaces;

namespace UnitTestDemo.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public Dictionary<int, string> _fakeDatabase = new Dictionary<int, string>() { 
            {1, "Kiwi"}, 
            {2, "Pear"},
            {3, "Orange"},
            {4, "Watermelon"},
        };

        public string GetFruitFromDatabase(int id)
        {
            if (!_fakeDatabase.ContainsKey(id))
                return null;

            return _fakeDatabase[id];
        }
    }
}
