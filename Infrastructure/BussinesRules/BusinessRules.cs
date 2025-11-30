using Npgsql;
using System.Text.RegularExpressions;

namespace CatalogoZap.Rules;

public interface IBussinesRules
{
    bool ValidNumber (string PhoneNumber);
    bool MinMaxValue(float price);
    //List<GetProductsReturn> FilterGetProductsReturn (List<GetProductsReturn> listProducts);
    bool ValidImage(IFormFile img);
    bool MaxFreePlan (string Connection, Guid idUser);
}

public class BusinessRules : IBussinesRules
{
    public bool ValidNumber (string PhoneNumber)
    {
        var Validation = @"^\s*\(?(\d{2})\)?[\s-]*9\d{4}[\s*]*\d{4}\s*$";
        bool result = Regex.IsMatch(PhoneNumber, Validation);
        return result;
    }

    public bool MinMaxValue(float price)
    {
        bool result = false;
        if (price >= 0.50 && price <= 10000)
        {
            result = true;
        }
        return result;
    }

    //public List<GetProductsReturn> FilterGetProductsReturn (List<GetProductsReturn> listProducts)
    //{
        //List<GetProductsReturn> listResult = new List<GetProductsReturn> { };
        //try
        //{
            //foreach (var item in listProducts)
            //{
                //if (item.Avaliable == true)
                //{
                    //listResult.Add(item);
                //}
            //}

            //return listResult;
        //}
        //catch (Exception ex)
        //{
            //Console.WriteLine($"err: {ex.Message} ");
            //return listResult;
        //}
    //}

    public bool ValidImage(IFormFile img)
    {
        const long MaxByte = 1 * 1024 * 1024;
        if (img.Length > MaxByte)
        {
            return false;
        }
        return true;
    }
    
    public bool MaxFreePlan (string Connection, Guid idUser)
    {
        bool result = false;

        try
        {
            using (var BD = new NpgsqlConnection(Connection))
            {
                BD.Open();
                string sqlCommand = "SELECT COUNT(p.id) FROM products p INNER JOIN profiles u ON p.user_id = u.id WHERE user_id = @user_id AND u.premium = FALSE";

                using (var command = new NpgsqlCommand(sqlCommand, BD))
                {
                    command.Parameters.AddWithValue("user_id", idUser);
                    var scalarResult = command.ExecuteScalar();
                    long cont = scalarResult != null ? Convert.ToInt64(scalarResult) : 0;

                    if(cont < 10)
                    {
                        result = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"something went wrong! error: {ex.Message}");
        }

        return result;
    }

}