using System;
using System.Collections.Generic;
using System.Text;

namespace M13_Library
{


    public class Organization : Client
    {
        string name;               // Название
        string inn;                // налоговый номер
        string representative;     // представитель



        public Organization(
            string name,
            string inn,
            string representative) : base()
        {
            this.name = name;
            this.inn = inn;
            this.representative = representative;
        }


        public string OrganizationName
        {
            get { return this.name; }
            set { SetProperty<string>(ref name, value, nameof(OrganizationName)); }
        }

        public string INN
        {
            get { return this.inn; }
            set { SetProperty<string>(ref inn, value, nameof(INN)); }
        }

        public string Representative
        {
            get { return this.representative; }
            set { SetProperty<string>(ref representative, value, nameof(Representative)); }
        }

        public override string Name() { return $"{this.OrganizationName} ИНН {this.INN}"; }

        public override string ToString()
        {
            return $"{this.ClientId}:\n" +
                   $"{this.OrganizationName} " +
                   $"ИНН {this.INN} \n" +
                   $"Представитель: {this.representative}";
        }

    }
}
