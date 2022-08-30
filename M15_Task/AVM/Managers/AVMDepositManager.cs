using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{
    internal class AVMDepositManager : AVMUser
    {
        public DepositManager Manager { get { return User as DepositManager; } }

        float amount;
        /// <summary>
        /// сумма дс для пополнения или снятия со счета, а также перевода дс
        /// </summary>
        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }


        WeirdCommand newDepositAccount;               // открыть счет
        public WeirdCommand NewDepositAccount { get { return newDepositAccount; } }

        WeirdCommand closeDepositAccount;                    // закрыть счет
        public WeirdCommand CloseDepositAccount { get { return closeDepositAccount; } }

        WeirdCommand putMoneyToDepositAccount;               // пополнить депозит
        public WeirdCommand PutMoneyToDepositAccount { get { return putMoneyToDepositAccount; } }

        WeirdCommand takeMoneyFromDepositAccount;            // снять с депозита
        public WeirdCommand TakeMoneyFromDepositAccount { get { return takeMoneyFromDepositAccount; } }


        public AVMDepositManager(User user) : base(user) 
        {
            Amount = 0;

            newDepositAccount = new WeirdCommand
                (o => { Manager.MNewDepositAccount(); });

            closeDepositAccount = new WeirdCommand
                (o => { Manager.MCloseDeposit(); });

            putMoneyToDepositAccount = new WeirdCommand
                (o => { Manager.MPutMoneyToDeposit(Amount); });

            takeMoneyFromDepositAccount = new WeirdCommand
                (o => { Manager.MGetMoneyFromDeposit(Amount); });

        }
    }
}
