using ATM_Simulator.Models;
using ATM_Simulator.Repositories;

namespace ATM_Simulator.Services {
  public class BankingService : ICanPerformBankingOperations {
    private readonly ICanFindUsers _userRepository;

    public BankingService(ICanFindUsers userRepository) {
      _userRepository = userRepository;
    }

    public double GetBalance(string cardNumber) {
      User user = _userRepository.FindByCardNumber(cardNumber);
      return user?.Balance ?? 0;
    }

    public TransactionResult Deposit(string cardNumber, double amount) {
      User user = _userRepository.FindByCardNumber(cardNumber);

      if (user == null) {
        return new TransactionResult(false, "Пользователь не найден");
      }

      if (amount <= 0) {
        return new TransactionResult(false, "Сумма должна быть больше 0");
      }

      double newBalance = user.Balance + amount;
      _userRepository.UpdateUserBalance(cardNumber, newBalance);

      return new TransactionResult(true, $"Внесено {amount} руб.", newBalance);
    }

    public TransactionResult Withdraw(string cardNumber, double amount) {
      User user = _userRepository.FindByCardNumber(cardNumber);

      if (user == null) {
        return new TransactionResult(false, "Пользователь не найден");
      }

      if (amount <= 0) {
        return new TransactionResult(false, "Сумма должна быть больше 0");
      }

      if (amount > user.Balance) {
        return new TransactionResult(false, $"Недостаточно средств для операции");
      }

      double newBalance = user.Balance - amount;
      _userRepository.UpdateUserBalance(cardNumber, newBalance);

      return new TransactionResult(true, $"Снято {amount} руб.", newBalance);
    }

    public TransactionResult Transfer(string fromCardNumber, string toCardNumber, double amount) {
      User fromUser = _userRepository.FindByCardNumber(fromCardNumber);
      User toUser = _userRepository.FindByCardNumber(toCardNumber);

      if (fromUser == null) {
        return new TransactionResult(false, "Отправитель не найден");
      }

      if (toUser == null) {
        return new TransactionResult(false, "Получатель не найден");
      }

      if (amount <= 0) {
        return new TransactionResult(false, "Сумма должна быть больше 0");
      }

      if (amount > fromUser.Balance) {
        return new TransactionResult(false, $"Недостаточно средств для операции");
      }

      double newFromBalance = fromUser.Balance - amount;
      double newToBalance = toUser.Balance + amount;

      _userRepository.UpdateUserBalance(fromCardNumber, newFromBalance);
      _userRepository.UpdateUserBalance(toCardNumber, newToBalance);

      return new TransactionResult(true, $"Переведено {amount} руб. клиенту {toUser.Name}", newFromBalance);
    }
  }
}
