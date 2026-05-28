using ATM_Simulator.Models;

namespace ATM_Simulator.Services {
  public interface ICanPerformBankingOperations {
    double GetBalance(string cardNumber);
    TransactionResult Deposit(string cardNumber, double amount);
    TransactionResult Withdraw(string cardNumber, double amount);
    TransactionResult Transfer(string fromCardNumber, string toCardNumber, double amount);
  }
}
