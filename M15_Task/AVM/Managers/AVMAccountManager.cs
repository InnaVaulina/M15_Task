using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{

    
    internal class AVMAccountManager : AVMUser
    {
        public AccountManager Manager { get { return User as AccountManager; } }

        public event MistakeMessageHendler MistakeMessageNotify;


        float amount;
        /// <summary>
        /// сумма дс для пополнения или снятия со счета, а также перевода дс
        /// </summary>
        public float Amount 
        {
            get { return amount; } 
            set { amount = value; }
        }

        string putAccountNomber;
        /// <summary>
        /// номер счета для перевода другому клиенту
        /// </summary>
        public string PutAccountNomber
        {
            get { return putAccountNomber; }
            set { putAccountNomber = value; }
        }



        WeirdCommand addAmountToCach;                 // пополнить кассу
        public WeirdCommand AddAmountToCach { get { return addAmountToCach; } }


        WeirdCommand newAccount;                      // открыть счет
        public WeirdCommand NewAccount { get { return newAccount; } }

        WeirdCommand closeAccount;                    // закрыть счет
        public WeirdCommand CloseAccount { get { return closeAccount; } }

        
        WeirdCommand putMoneyToAccount;               // пополнить счет
        public WeirdCommand PutMoneyToAccount { get { return putMoneyToAccount; } }

        WeirdCommand takeMoneyFromAccount;            // снять со счета
        public WeirdCommand TakeMoneyFromAccount { get { return takeMoneyFromAccount; } }

        WeirdCommand transfer;                        // перевод на другой счет
        public WeirdCommand Transfer { get { return transfer; } }



        public AVMAccountManager(User user) : base(user) 
        {
            PutAccountNomber = "00000";       // по умолчанию касса
            Amount = 0;

            addAmountToCach = new WeirdCommand
                ((parametr) => 
                {                    
                    float amount = (float)parametr;                    
                    Manager.MPutMoneyToCash(amount);                                     
                });

            newAccount = new WeirdCommand
                (o => { Manager.MNewAccount(); });

            closeAccount = new WeirdCommand
                (o =>
                {
                    try 
                    {
                        Manager.MCloseAccount();
                    }
                    catch (Exception e)
                    {
                        MistakeMessageNotify?.Invoke(this, new MistakeEventArgs($"Ошибка: >> {e.Message} {e.GetType().ToString()}"));
                    }
                });

            putMoneyToAccount = new WeirdCommand
                (o => 
                { 
                    try
                    {
                        Manager.MPutMoney(Amount);
                    }
                    catch (Exception e)
                    {
                        MistakeMessageNotify?.Invoke(this, new MistakeEventArgs($"Ошибка: >> {e.Message} {e.GetType().ToString()}"));
                    }
                });

            takeMoneyFromAccount = new WeirdCommand
                (o =>
                {                     
                    try
                    {
                        Manager.MTakeMoney(Amount);
                        Manager.MTakeMoneyFromCash(Amount);
                    }
                    catch (Exception e)
                    {
                        MistakeMessageNotify?.Invoke(this, new MistakeEventArgs($"Ошибка: >> {e.Message} {e.GetType().ToString()}"));
                    }
                });


            transfer = new WeirdCommand
                (o =>
                {                    
                        try 
                        {
                        Manager.MTransfer(PutAccountNomber, Amount);
                        }
                        catch (Exception e)
                        {
                            MistakeMessageNotify?.Invoke(this, new MistakeEventArgs($"Ошибка: >> {e.Message} {e.GetType().ToString()}"));
                        }
                                           
                });

        }
    }
}
