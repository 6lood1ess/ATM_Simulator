using ATM_Simulator.Repositories;
using ATM_Simulator.Services;
using System;

namespace ATM_Simulator.Core {
  public class ATMSingleton {
    private static ATMSingleton _instance;
    private static readonly object _lock = new object();

    public string Name;
    public string Version;

    public ICanFindUsers UserRepository;
    public ICanPerformBankingOperations BankingService;
    public SessionManager SessionManager;

    private ATMSingleton() {
      Name = "MoneyMoneyMoney";
      Version = "777";

      UserRepository = new UserRepository();
      BankingService = new BankingService(UserRepository);
      SessionManager = new SessionManager(UserRepository);
    }

    public static ATMSingleton Instance {
      get {
        if (_instance == null) {
          lock (_lock) {
            if (_instance == null) {
              _instance = new ATMSingleton();
            }
          }
        }

        return _instance;
      }
    }

    public void InitializeATM() {
      Console.WriteLine($"Банкомат {Name} версии {Version} инициализирован");
    }
  }
}
