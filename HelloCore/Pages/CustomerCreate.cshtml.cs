using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace HelloCore.Pages
{
    public class CustomerCreateModel : PageModel
    {
        private readonly IConfiguration _config;

        public CustomerCreateModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Tel { get; set; }

        public string Message { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Message = "ñºëOÇì¸óÕÇµÇƒÇ≠ÇæÇ≥Ç¢ÅB";
                return Page();
            }

            string? connStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO Customers (Name, Tel) VALUES (@Name, @Tel)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    if (string.IsNullOrEmpty(Tel))
                    {
                        cmd.Parameters.AddWithValue("@Tel", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Tel", Tel);
                    }
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("Customers");
        }
    }
}