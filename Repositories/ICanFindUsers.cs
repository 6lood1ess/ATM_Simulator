using ATM_Simulator.Models;
using System.Collections.Generic;

namespace ATM_Simulator.Repositories {
  public interface ICanFindUsers {
    User FindByCardNumber(string cardNumber);
    User FindByCredentials(string cardNumber, string pinCode);
    List<User> GetAllUsers();
    void UpdateUserBalance(string cardNumber, double newBalance);
  }
}
