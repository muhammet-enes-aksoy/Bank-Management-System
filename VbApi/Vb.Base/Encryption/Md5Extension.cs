namespace Vb.Base.Encryption;

public class Md5Extension
{
 
    public static string Create(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();

        }
    }
    public static string GetHash(string input)
    {
        var hash = Create(input);
        return Create(hash);
    }
}