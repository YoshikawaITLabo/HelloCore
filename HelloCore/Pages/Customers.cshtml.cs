using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace HelloCore.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly IConfiguration _config;

        public CustomersModel(IConfiguration config)
        {
            _config = config;
        }

        public List<Customer> CustomerList { get; set; } = new List<Customer>();

        public void OnGet()
        {
            string? connStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT Id, Name, Tel FROM Customers WHERE DeleteFlag = 0 ORDER BY Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer c = new Customer();
                        c.Id = reader.GetInt32(0);
                        c.Name = reader.GetString(1);
                        if (reader.IsDBNull(2))
                        {
                            c.Tel = "";
                        }
                        else
                        {
                            c.Tel = reader.GetString(2);
                        }
                        CustomerList.Add(c);
                    }
                }
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Tel { get; set; } = "";
    }
}