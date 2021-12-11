using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuMoney.DAL
{
    public class Remboursement_DAL
    {
        public int ID_REMBOURSEMENT { get; set; }
        public int ID_ARGENT_DEP { get; set; }
        public int ID_GENS_A_REMBOURSER { get; set; }
        public float ARGENT_GLOBAL { get; set; }
        public float ARGENT_MOYENNE { get; set; }
        public float DETTE { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Created_at { get; set; }

        public Remboursement_DAL(int id, int id_depenses, int id_gens, DateTime? created_at)
        {
            ID_REMBOURSEMENT = id;
            ID_ARGENT_DEP = id_depenses;
            ID_GENS_A_REMBOURSER = id_gens;
            Created_at = created_at;

        }

        public Remboursement_DAL(int id, int id_depenses, int id_gens, float argentGlobal, DateTime? created_at)
        {
            ID_REMBOURSEMENT = id;
            ID_ARGENT_DEP = id_depenses;
            ID_GENS_A_REMBOURSER = id_gens;
            ARGENT_GLOBAL = argentGlobal;
        }
        public Remboursement_DAL(int id, int id_depenses, int id_gens, float argentGlobal, float argentMoy, DateTime? created_at)
        {
            ID_REMBOURSEMENT = id;
            ID_ARGENT_DEP = id_depenses;
            ID_GENS_A_REMBOURSER = id_gens;
            ARGENT_GLOBAL = argentGlobal;
            ARGENT_MOYENNE = argentMoy;
        }
        public Remboursement_DAL(int id, int id_depenses, int id_gens, float argentGlobal, float argentMoy, float Dette, DateTime? created_at)
        {
            ID_REMBOURSEMENT = id;
            ID_ARGENT_DEP = id_depenses;
            ID_GENS_A_REMBOURSER = id_gens;
            ARGENT_GLOBAL = argentGlobal;
            ARGENT_MOYENNE = argentMoy;
            DETTE = Dette;
        }


    }
}
