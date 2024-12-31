using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System.Text;


namespace DatabaseDemo.Pages
{

    public class IndexModel : PageModel
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BikeStores;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public string fulltable = "";

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }


        public void OnPostConnectToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Perform database operations
                SqlCommand command = new SqlCommand("SELECT * FROM store", connection);
                SqlDataReader reader = command.ExecuteReader();
                fulltable = tableDisplay(reader);
            }//connection destroyed
        }



        public string tableDisplay(SqlDataReader reader)
        {
            StringBuilder sb = new StringBuilder();
            int id;
            string name;

            if (reader.HasRows)
            {
                sb.Append("<h1>here is the table</h1>\r\n");
                sb.Append("<table style=\"border: 1px solid black;border - collapse: collapse;\" >");
                sb.Append("<thead><tr><th width = \"55px\">Id</th><th width = \"100px\">Name</th></tr></thead>");
                sb.Append("<tbody style=\"border: 1px solid black;border - collapse: collapse;\">");
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    name = reader.GetString(1);
                    sb.Append($"<tr style=\"border: 1px solid black;border - collapse: collapse;\"><td>{id}</td><td>{name}</td></tr>");
                }

                sb.Append("</tbody></table>");
            }
            else
            {
                sb.Append("<p>No stores available.</p>");
            }
            reader.Close();

           // Html.Raw(sb.ToString());

            return sb.ToString();
        }
    }
}
