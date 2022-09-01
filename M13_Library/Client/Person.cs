using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M13_Library
{
    /// <summary>
    /// данные о клиентах
    /// </summary>
    public class Person : Client
    {
        string familyName;         // фамилия
        string firstName;          // имя
        string patronymicName;     // отчество


        public Person(
            string familyName,
            string firstName,
            string patronymicName) : base()
        {
            this.familyName = familyName;
            this.firstName = firstName;
            this.patronymicName = patronymicName;
        }


        public string FamilyName
        {
            get { return familyName; }
            set { SetProperty<string>(ref familyName, value, nameof(FamilyName)); }
        }
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty<string>(ref firstName, value, nameof(FirstName)); }
        }
        public string PatronymicName
        {
            get { return patronymicName; }
            set { SetProperty<string>(ref patronymicName, value, nameof(PatronymicName)); }
        }

        public override string Name()
        {
            return $"{this.FamilyName} " +
                   $"{this.FirstName} " +
                   $"{this.PatronymicName} ";
        }



        public override string ToString()
        {
            return base.ToString() +
                   $"\n{this.FamilyName} " +
                   $"{this.FirstName} " +
                   $"{this.PatronymicName}";
        }
    }
}
