using ATM_Simulator.Models;
using ATM_Simulator.Repositories;

namespace ATM_Simulator.Core {
  public class SessionManager {
    private readonly ICanFindUsers _userRepository;
    public string CurrentCardNumber;

    public SessionManager(ICanFindUsers userRepository) {
      _userRepository = userRepository;
      CurrentCardNumber = null;
    }

    public bool IsLoggedIn {
      get {
        return CurrentCardNumber != null;
      }
    }

    public string CurrentUserName {
      get {
        return _userRepository.FindByCardNumber(CurrentCardNumber)?.Name;
      }
    }

    public bool Login(string cardNumber, string pinCode) {
      User user = _userRepository.FindByCredentials(cardNumber, pinCode);

      if (user != null) {
        CurrentCardNumber = cardNumber;
        return true;
      }

      return false;
    }

    public void Logout() {
      CurrentCardNumber = null;
    }
  }
}
