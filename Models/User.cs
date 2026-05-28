namespace ATM_Simulator.Models {
  public class User {
    public string Name;
    public string CardNumber;
    public string PinCode;
    public double Balance;

    public User(string name, string cardNumber, string pinCode, double balance) {
      Name = name;
      CardNumber = cardNumber;
      PinCode = pinCode;
      Balance = balance;
    }
  }
}
