using SQLite;

namespace SqlStressTest
{
    public class BaseDBModel 
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
    }
}
