using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuMoney.DAL
{
    class Depenses_DAL
    {
        public int ID_DEPENSE { get; set; }
        public int ID_GENS_QUI_DEPENSE { get; set; }
        public float ARGENT { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }

        public Depenses_DAL(int id, int gensQuiDepesent, float argentdepense, DateTime? createdAt)
        {
            ID_DEPENSE = id;
            ID_GENS_QUI_DEPENSE = gensQuiDepesent;
            ARGENT = argentdepense;
            Created_at = createdAt;
        }

        public Depenses_DAL(int id, int gensQuiDepesent, float argentdepense, DateTime? createdAt, DateTime? updatedAt)
        {
            ID_DEPENSE = id;
            ID_GENS_QUI_DEPENSE = gensQuiDepesent;
            ARGENT = argentdepense;
            Created_at = createdAt;
            Updated_at = updatedAt;
        }

        internal void Insert(SqlConnection connexion)
        {
            using (var commande = new SqlCommand())
            {
                commande.Connection = connexion;
                commande.CommandText = "insert into Depenses(id_depense,id_gens_qui_depense,argent_depense,created_at)" +
                    " values (@ID_DEPENSE,@ID_GENS_QUI_DEPENSE,@ARGENT,@Created_at";
                commande.Parameters.Add(new SqlParameter("@ID_DEPENSE", ID_DEPENSE));
                commande.Parameters.Add(new SqlParameter("@ID_GENS_QUI_DEPENSE", ID_GENS_QUI_DEPENSE));
                commande.Parameters.Add(new SqlParameter("@ARGENT", ARGENT));
                commande.Parameters.Add(new SqlParameter("@Created_at", Created_at));

                commande.ExecuteNonQuery();
            }
        }
    }
}
