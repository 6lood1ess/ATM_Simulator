using ATM_Simulator.Models;
using System.Collections.Generic;
using System.Linq;

namespace ATM_Simulator.Repositories {
  public class UserRepository : ICanFindUsers {
    private readonly List<User> _users;

    public UserRepository() {
      _users = new List<User> {
        new User("Криштиано Роналдо", "8584739463025264", "6767", 5),
        new User("Никита Захаров", "5837274969570736", "1915", 4064),
        new User("Брюс Уэйн", "3958729600658263", "0707", 1039458)
      };
    }

    public User FindByCardNumber(string cardNumber) {
      return _users.FirstOrDefault(user => user.CardNumber == cardNumber);
    }

    public User FindByCredentials(string cardNumber, string pinCode) {
      return _users.FirstOrDefault(user => user.CardNumber == cardNumber && user.PinCode == pinCode);
    }

    public List<User> GetAllUsers() {
      return _users.ToList();
    }

    public void UpdateUserBalance(string cardNumber, double newBalance) {
      User user = FindByCardNumber(cardNumber);

      if (user != null) {
        user.Balance = newBalance;
      }
    }
  }
}
