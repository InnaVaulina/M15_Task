using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using M14_Library;

namespace M15_Task
{
    public class TemplateSelectorClientList : DataTemplateSelector
    {
        public DataTemplate PersonTemplate { get; set; }
        public DataTemplate OrganisationTemplate { get; set; }
        public DataTemplate NullTemplate { get; set; }


        /// <summary>
        /// выбор шаблона для отображения клиента в списке
        /// </summary>
        /// <param name="item">тип Client и производные</param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "M13_Library.Organization":
                        return OrganisationTemplate;
                    case "M13_Library.Person":
                        return PersonTemplate;
                    default: return NullTemplate;
                }
            }
            return NullTemplate;
        }
    }




    public class TemplateSelectorClient : DataTemplateSelector
    {

        public DataTemplate NullTemplate { get; set; }

        public DataTemplate ChangePersonTemplate { get; set; }

        public DataTemplate ChangeOrganisationTemplate { get; set; }

        public DataTemplate PersonConsultantTemplate { get; set; }

        public DataTemplate OrganisationConsultantTemplate { get; set; }

        /// <summary>
        /// выбор шаблона для отображения модели представления данных о клиенте
        /// </summary>
        /// <param name="item">тип AVMClient и его производные</param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item != null) 
            {
                string type = item.GetType().ToString();
                switch (type)
                { 
                    case "M15_Task.AVMClient": 
                        return NullTemplate;
                    case "M15_Task.AVMOrganizationClient": 
                        return OrganisationConsultantTemplate;
                    case "M15_Task.AVMOrganizationClientChange": 
                        return ChangeOrganisationTemplate;
                    case "M15_Task.AVMPersonClient": 
                        return PersonConsultantTemplate;
                    case "M15_Task.AVMPersonClientChange": 
                        return ChangePersonTemplate;
                    default: return NullTemplate;
                }    
            }
            return NullTemplate;
        }
    }



    public class TempAccountSelector : DataTemplateSelector
    {
        public DataTemplate AccountTemplate { get; set; }

        public DataTemplate DepositAccountTemplate { get; set; }

        /// <summary>
        /// выбор шаблона для отображения счета
        /// </summary>
        /// <param name="item"> тип Account и его производные</param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "M13_Library.Account":
                        return AccountTemplate;
                    case "M13_Library.DepositAccount":
                        return DepositAccountTemplate;
                    default: return null;
                }
            }
            return null;
        }
    }


    public class TemplateUserSelector : DataTemplateSelector
    {
        public DataTemplate ConsultantCommandTemplate { get; set; }
        public DataTemplate ManagerForNewClientCommandTemplate { get; set; }
        public DataTemplate AccountManagerCommandTemplate { get; set; }
        public DataTemplate DepositManagerCommandTemplate { get; set; }

        /// <summary>
        /// выбор шаблона для отображения функций менеджера
        /// </summary>
        /// <param name="item">тип AVMUser и его производные</param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                string type = item.GetType().ToString();
                switch (type)
                {
                    case "M15_Task.AVMConsultant":
                        return ConsultantCommandTemplate;
                    case "M15_Task.AVMManagerForNewClient":
                        return ManagerForNewClientCommandTemplate;
                    case "M15_Task.AVMAccountManager":
                        return AccountManagerCommandTemplate;
                    case "M15_Task.AVMDepositManager":
                        return DepositManagerCommandTemplate;
                    default: return null;
                }
            }
            return null;
        }
    }

}
