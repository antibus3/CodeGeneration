using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    //  Обьект для хранения информации об обьекте
    public class ObjectOfConsumption
    {
        public readonly string nameID;
        public readonly string personalAccount;
        public readonly DateTime dateTime;

        public ObjectOfConsumption (string nameID, string personalAccount, DateTime dateTime)
        {
            this.nameID = nameID;
            this.personalAccount = personalAccount;
            this.dateTime = dateTime;
        }
        public override bool Equals(object obj)
        {
            ObjectOfConsumption another = obj as ObjectOfConsumption;
            if (another == null) return false;
            return (this.dateTime.Equals(another.dateTime) && (this.nameID == another.nameID) && (this.personalAccount == another.personalAccount));
        }
    }
}
