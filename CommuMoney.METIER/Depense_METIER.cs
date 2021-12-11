using System;
using CommuMoney.DAL;

namespace CommuMoney.METIER
{
    public class Depense_METIER
    {
        public int ID_DEPENSE { get; set; }
        public int ID_GENS_QUI_DEPENSE { get; set; }
        public float ARGENT { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        public Depense_METIER(int id_gens_qui_depense, float argent, DateTime? created_at, DateTime? updated_at) => (ID_GENS_QUI_DEPENSE, ARGENT, Created_at, Updated_at) = (id_gens_qui_depense, argent, created_at, updated_at);
        public Depense_METIER(int id_depense, int id_gens_qui_depense, float argent, DateTime? created_at, DateTime? updated_at) => (ID_DEPENSE, ID_GENS_QUI_DEPENSE, ARGENT, Created_at, Updated_at) = (id_depense, id_gens_qui_depense, argent, created_at, updated_at);

        #region Insert
        public void Insert()
        {
            Depense
        }
        #endregion
    }
}
