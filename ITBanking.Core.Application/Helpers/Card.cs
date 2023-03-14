namespace ITBanking.Core.Application.Helpers;
public static class Card{
  private static readonly Random random = new Random();

  public static string GenCardNumber(){
    var cardNumber = new char[16].Select(x => random.Next(0, 10).ToString()[0]).ToArray();
    
    while(GenCardProvider(new string(cardNumber)) == "Unknown")
      cardNumber = new char[16].Select(x => random.Next(0, 10).ToString()[0]).ToArray();
    

    return FormatCardNumber(new string(cardNumber));
  }

  public static string GenCardPin()=> random.Next(100000000, 999999999).ToString();
  public static string GenCardCvv() => random.Next(200, 999).ToString();


  public static string GenCardExpiryDate(){
    int month = random.Next(1, 13);
    int year = random.Next(25, 40);

    return month.ToString("D2") + '/' + year.ToString();
  }

  public static string GenCardProvider(string cardNumber){
    if (cardNumber.Substring(0, 2) == "34" || cardNumber.Substring(0, 2) == "37")
      return "American Express";
    
    switch (cardNumber[0])
    {
      case '4':
        return "Visa";
      case '5':
        return "MasterCard";
      default:
        return "Unknown";
    }
  }

  private static string FormatCardNumber(string cardNumber)=> String.Join(" ", cardNumber.SplitInChunksOf(4));
  
  private static IEnumerable<string> SplitInChunksOf(this string s, int chunkSize){
    for (int i = 0; i < s.Length; i += chunkSize)
    {
      yield return s.Substring(i, Math.Min(chunkSize, s.Length - i));
    }
  }
}
