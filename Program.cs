using ATM_Simulator.Core;
using ATM_Simulator.Models;
using System;

namespace ATM_Simulator {
  public class Program {
    private static ATMSingleton _atm;  // ATM - Automated Teller Machine

    private static void Main() {
      _atm = ATMSingleton.Instance;
      _atm.InitializeATM();

      Console.WriteLine($"Приветствуем в {_atm.Name}!");

      if (!PerformLogin()) {
        Console.WriteLine("Доступ запрещён. Нажмите Enter для выхода...");
        _ = Console.ReadKey();
        return;
      }

      RunMainMenu();

      Console.WriteLine("\nБлагодарим за использование банкомата!");
      _ = Console.ReadKey();
    }

    private static bool PerformLogin() {
      Console.WriteLine("| АВТОРИЗАЦИЯ |");

      Console.Write("Номер карты: ");
      string cardNumber = Console.ReadLine();

      Console.Write("PIN-код: ");
      string pinCode = Console.ReadLine();

      bool success = _atm.SessionManager.Login(cardNumber, pinCode);

      if (success) {
        Console.WriteLine($"\nДобро пожаловать, {_atm.SessionManager.CurrentUserName}! :)");
      } else {
        Console.WriteLine("\nНеверный номер карты или PIN-код");
      }

      return success;
    }

    private static void RunMainMenu() {
      bool stopSession = false;

      while (!stopSession) {
        Console.WriteLine("\n| МЕНЮ |" +
                          "\n1. Показать баланс" +
                          "\n2. Внести" +
                          "\n3. Снять" +
                          "\n4. Перевести" +
                          "\n5. Выйти");

        Console.Write("\nВыберите действие: ");
        string choice = Console.ReadLine();

        switch (choice) {
          case "1":
            ShowBalance();
            break;
          case "2":
            Deposit();
            break;
          case "3":
            Withdraw();
            break;
          case "4":
            Transfer();
            break;
          case "5":
            _atm.SessionManager.Logout();
            stopSession = true;
            Console.WriteLine("\nДо новых встреч!");
            break;
          default:
            Console.WriteLine("Выбрано неверное действие. Попробуйте ещё раз");
            break;
        }
      }
    }

    private static void ShowBalance() {
      string card = _atm.SessionManager.CurrentCardNumber;
      double balance = _atm.BankingService.GetBalance(card);

      Console.WriteLine($"\nБаланс: {balance} руб.");
    }

    private static void Deposit() {
      Console.Write("Введите сумму: ");

      if (!double.TryParse(Console.ReadLine(), out double amount)) {
        Console.WriteLine("Введите корректную сумму");
        return;
      }

      TransactionResult result = _atm.BankingService.Deposit(_atm.SessionManager.CurrentCardNumber, amount);

      if (result.Success) {
        Console.WriteLine($"{result.Message}. Новый баланс: {result.NewBalance} руб.");
      } else {
        Console.WriteLine($"{result.Message}");
      }
    }

    private static void Withdraw() {
      Console.Write("Введите сумму: ");

      if (!double.TryParse(Console.ReadLine(), out double amount)) {
        Console.WriteLine("Введите корректную сумму");
        return;
      }

      TransactionResult result = _atm.BankingService.Withdraw(_atm.SessionManager.CurrentCardNumber, amount);

      if (result.Success) {
        Console.WriteLine($"{result.Message}. Осталось: {result.NewBalance} руб.");
      } else {
        Console.WriteLine($"{result.Message}");
      }
    }

    private static void Transfer() {
      Console.Write("Введите номер карты получателя: ");
      string toCard = Console.ReadLine();

      Console.Write("Введите сумму: ");

      if (!double.TryParse(Console.ReadLine(), out double amount)) {
        Console.WriteLine("Введите корректную сумму");
        return;
      }

      TransactionResult result = _atm.BankingService.Transfer(_atm.SessionManager.CurrentCardNumber, toCard, amount);

      if (result.Success) {
        Console.WriteLine($"{result.Message}. Новый баланс: {result.NewBalance} руб.");
      } else {
        Console.WriteLine($"{result.Message}");
      }
    }
  }
}
