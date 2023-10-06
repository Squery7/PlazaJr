using System.Data.SqlClient;

namespace API_REST.Dao
{
    public class LibAutDao
    {
        private SqlConnection con;

        public LibAutDao(SqlConnection con)
        {
            this.con = con;
        }



    }
}
