using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M13_Library;
using M14_Library;

namespace M15_Task
{
    internal class AVMManagerForNewClient :AVMUser
    {
        AVM mainAVM;  

        public ManagerForNewClient Manager { get { return User as ManagerForNewClient; } }



        WeirdCommand setViewForNewOrganizationCreate;
        /// <summary>
        /// // установить представление для создания клиента-юл
        /// </summary>
        public WeirdCommand SetViewForNewOrganizationCreate { get { return setViewForNewOrganizationCreate; } }

        WeirdCommand setViewForNewPersonCreate;
        /// <summary>
        /// // установить представление для создания клиента-фл
        /// </summary>
        public WeirdCommand SetViewForNewPersonCreate { get { return setViewForNewPersonCreate; } }


        public AVMManagerForNewClient(User user, AVM avm) : base(user) 
        {
            mainAVM = avm;


            setViewForNewOrganizationCreate = new WeirdCommand
                (o => { avm.ClientView = new AVMOrganizationClientChange(User, null); });

            setViewForNewPersonCreate = new WeirdCommand
                (o => { avm.ClientView = new AVMPersonClientChange(User, null); });
        }

    }
}
