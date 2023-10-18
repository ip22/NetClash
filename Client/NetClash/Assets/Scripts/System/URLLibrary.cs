public class URLLibrary
{
    public const string MAIN = "http://localhost/lDatabase/";
    public const string AUTHORIZATION = "/Auth/authorization.php";
    public const string REGISTRATION = "/Auth/registration.php";

    // можно так
    //public static string Autorization { get { return MAIN + AUTHORIZATION; } }

    public const string GETDECKINFO = "/Game/GedDeckInfo.php";
    public const string SETSELECTEDCARDS = "/Game/setUserSelectedCards.php";
}
