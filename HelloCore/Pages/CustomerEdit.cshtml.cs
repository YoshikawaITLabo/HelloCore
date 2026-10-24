using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace HelloCore.Pages
{
    public class CustomerEditModel : PageModel
    {
        private readonly IConfiguration _config;

        public CustomerEditModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Tel { get; set; }

        public string Message { get; set; } = "";

        // 画面を開いたとき：URLの id で1件取得して表示
        public IActionResult OnGet(int id)
        {
            string? connStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT Id, Name, Tel FROM Customers WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() == false)
                        {
                            return NotFound();   // 該当IDなし
                        }

                        Id = reader.GetInt32(0);
                        Name = reader.GetString(1);
                        if (reader.IsDBNull(2))
                        {
                            Tel = "";
                        }
                        else
                        {
                            Tel = reader.GetString(2);
                        }
                    }
                }
            }

            return Page();
        }

        // 更新ボタンを押したとき
        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Message = "名前を入力してください。";
                return Page();
            }

            string? connStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE Customers SET Name = @Name, Tel = @Tel, UpdatedAt = SYSDATETIME() WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
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
        // 削除ボタンを押したとき
        public IActionResult OnPostDelete()
        {
            string? connStr = _config.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = "UPDATE Customers SET DeleteFlag = 1 WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("Customers");
        }


    }
}