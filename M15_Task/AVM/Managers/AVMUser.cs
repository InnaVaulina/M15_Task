using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{
    public class AVMUser
    {
        User user;
        /// <summary>
        /// менеджер или консультант
        /// </summary>
        public User User
        {
            get { return user; }
        }

        string userHelloMessage;
        /// <summary>
        /// приветственное сообщение
        /// </summary>
        public string UserHelloMessage
        {
            get { return userHelloMessage; }
        }


        AVMClient client;
        /// <summary>
        /// выбран клиент для работы
        /// </summary>
        public AVMClient Client 
        {
            get { return client; }
            set { client = value; }
        }
    
       
        WeirdCommand saveClientChanges;                 // сохранить изменения в данных клиента
        public WeirdCommand SaveClientChanges { get { return saveClientChanges; } }


        public AVMUser(User user) 
        { 
            this.user = user; 
            this.userHelloMessage = user.HelloMessage();


            saveClientChanges = new WeirdCommand(o => { Client.SaveChanges(); });
            
        }



    }
}
