using MySql.Data.MySqlClient;
using System;

class SampleManager
{
private string connectionString = "Server=localhost;Port=3306;Database=MedicineSamples;User Id=mk;Password=Root123;";

    // Adding a new sample
    public void AddSample(string name, int quantity, string expirationDate)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Samples (name, quantity, expirationDate, expired) VALUES (@name, @quantity, @expirationDate, expired)";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Book added successfully.");
            }
        }
    }

    // Updating sample details
    public void UpdateSample(int sample_id, string name, int quantity, string expirationDate, bool expired)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Samples SET name = @name, quantity = @quantity, expirationDate = @expirationDate, expired = @expired WHERE sample_id = @sample_id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
                cmd.Parameters.AddWithValue("@expired", expired);
                cmd.Parameters.AddWithValue("@sample_id", sample_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sample updated successfully.");
            }
        }
    }

    public void UpdateName(int sample_id, string name)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Samples SET name = @name WHERE sample_id = @sample_id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@sample_id", sample_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sample updated successfully.");
            }
        }
    }

    public void UpdateQuantity(int sample_id, int quantity)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Samples SET quantity = @quantity WHERE sample_id = @sample_id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@sample_id", sample_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sample updated successfully.");
            }
        }
    }

    public void UpdateDate(int sample_id, string expirationDate)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Samples SET expirationDate = @expirationDate WHERE sample_id = @sample_id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@expirationDate", expirationDate);
                cmd.Parameters.AddWithValue("@sample_id", sample_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sample updated successfully.");
            }
        }
    }

    public void UpdateExpired(int sample_id, bool expired)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE Samples SET expired = @expired WHERE sample_id = @sample_id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@expired", expired);
                cmd.Parameters.AddWithValue("@sample_id", sample_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sample updated successfully.");
            }
        }
    }


    // Deleting a sample
    public void DeleteSample(int sample_id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM Samples WHERE sample_id = @sample_id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@sample_id", sample_id);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Sample deleted successfully.");
            }
        }
    }
}
class Run
{
    static void Main(string[] args)
    {
        SampleManager manager = new SampleManager();

        while (true)
        {
            Console.WriteLine("Sample Manager Menu:");
            Console.WriteLine("1. Add Sample");
            Console.WriteLine("2. Update Sample");
            Console.WriteLine("3. Delete Sample");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string? choice = Console.ReadLine();  // Marking as nullable with '?'
            
            if (choice == null)
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }
            
            switch (choice)
            {
                case "1":
                    Console.Write("Enter sample name: ");
                    string name = Console.ReadLine() ?? string.Empty;  // Provide default empty string if null
                    Console.Write("Enter quantity: ");
                    int quantity = int.Parse(Console.ReadLine() ?? "0"); // Provide default value if null
                    Console.Write("Enter expiration date (YYYY-MM-DD): ");
                    string expirationDate = Console.ReadLine() ?? string.Empty;
                    manager.AddSample(name, quantity, expirationDate);
                    break;
                case "2":
                    Console.Write("Enter sample ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.Write("Enter new name (or leave blank to keep current): ");
                        string newName = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter new quantity (or leave blank to keep current): ");
                        string qtyInput = Console.ReadLine() ?? string.Empty;
                        int newQuantity = string.IsNullOrEmpty(qtyInput) ? -1 : int.Parse(qtyInput);
                        Console.Write("Enter new expiration date (YYYY-MM-DD or leave blank to keep current): ");
                        string newDate = Console.ReadLine() ?? string.Empty;
                        Console.Write("Is it expired (true/false or leave blank to keep current): ");
                        string expiredInput = Console.ReadLine() ?? string.Empty;
                        bool expired = string.IsNullOrEmpty(expiredInput) ? false : bool.Parse(expiredInput);
                        
                        if (!string.IsNullOrEmpty(newName))
                            manager.UpdateName(id, newName);
                        if (newQuantity != -1)
                            manager.UpdateQuantity(id, newQuantity);
                        if (!string.IsNullOrEmpty(newDate))
                            manager.UpdateDate(id, newDate);
                        manager.UpdateExpired(id, expired);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format.");
                    }
                    break;
                case "3":
                    Console.Write("Enter sample ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteId))
                    {
                        manager.DeleteSample(deleteId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID format.");
                    }
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }


}

// class Program
// {
//     static void Main(string[] args)
//     {
//         string connectionString = "Server=localhost;Port=3306;Database=MedicineSamples;User Id=mk;Password=Root123;";
//         using (var connection = new MySqlConnection(connectionString))
//         {
//             try
//             {
//                 connection.Open();
//                 Console.WriteLine("Connection successful!");

//                 // Test your queries here
//                 using (var command = new MySqlCommand("SELECT * FROM Samples", connection))
//                 using (var reader = command.ExecuteReader())
//                 {
//                     while (reader.Read())
//                     {
//                         Console.WriteLine($"{reader["sample_id"]}, {reader["name"]}");
//                     }
//                 }
//             }
//             catch (MySqlException ex)
//             {
//                 Console.WriteLine($"Error: {ex.Message}");
//             }
//         }
//     }
// }