namespace _Project.Scripts.Gameplay.Money
{
    public class Wallet
    {
        private int _currentMoney;

        public void AddMoney(int amountAddedMoney)
        {
            _currentMoney += amountAddedMoney;
        }

        public void SpendMoney(int amountSpentMoney)
        {
            if (_currentMoney - amountSpentMoney <= 0)
                return;

            _currentMoney -= amountSpentMoney;
        }
    }
}