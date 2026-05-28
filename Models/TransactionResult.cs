namespace ATM_Simulator.Models {
  public class TransactionResult {
    public bool Success;
    public string Message;
    public double? NewBalance;

    public TransactionResult(bool success, string message, double? newBalance = null) {
      Success = success;
      Message = message;
      NewBalance = newBalance;
    }
  }
}
