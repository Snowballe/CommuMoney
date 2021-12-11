using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuMoney.DAL
{
    public class Depense_DepotDAL : Depot_DAL<Depenses_DAL>
    {
        public override void Delete(Depenses_DAL depense)
        {
            dbConnect();
            commande.CommandText = "delete from Depenses where id_depense=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", depense.ID_DEPENSE));
            var nbLigne = (int)commande.ExecuteNonQuery();

            if (nbLigne!=1)
            {
                throw new Exception($"Impossible de supprimer le point d'ID {depense.ID_DEPENSE}");
            }
            
            dbClose();
        }

      

        public override List<Depenses_DAL> GetAll()
        {
            dbConnect();

            commande.CommandText = "select id_depense, id_gens_qui_depense, argent_depense, created_at from Depenses";
            var reader = commande.ExecuteReader();

            var listeDesDepenses = new List<Depenses_DAL>();
            while (reader.Read())
            {
                var dep = new Depenses_DAL(reader.GetInt32(0),
                                            reader.GetInt32(1),
                                            reader.GetFloat(2),
                                            reader.GetDateTime(3));
                listeDesDepenses.Add(dep);
            }

            dbClose();
            return listeDesDepenses;

        }


        public override Depenses_DAL GetByID(int ID)
        {
            dbConnect();
            commande.CommandText = "select id_depense, id_gens_qui_depense, argent_depense, created_at from Depenses where id_depenses=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = commande.ExecuteReader();

            Depenses_DAL dep;

            if (reader.Read())
            {
                dep = new Depenses_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetFloat(2),
                                        reader.GetDateTime(3));
            }
            else
            {
                throw new Exception($"Pas de dépense de disponible avec l'ID {ID}");
            }

            dbClose();
            return dep;
        }

        

        public override Depenses_DAL Insert(Depenses_DAL depense)
        {
            dbConnect();

            commande.CommandText = "insert into Depenses(id_depense, id_gens_qui_depense, argent_depense) values (@ID_DEP, @ID_GENS_DEP, @ARGENT); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@ID_DEP", depense.ID_DEPENSE));
            commande.Parameters.Add(new SqlParameter("@ID_GENS_DEP", depense.ID_GENS_QUI_DEPENSE));
            commande.Parameters.Add(new SqlParameter("@ARGENT", depense.ARGENT));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            depense.ID_DEPENSE = ID;

            dbClose();

            return depense;
        }


        public override Depenses_DAL Update(Depenses_DAL depense)
        {
            dbConnect();

            commande.CommandText = "update Depenses set argent_depense=@ARGENT, id_gens_qui_depense=@ID_GENS, updated_at=getDate() where id_depense=@ID";
            commande.Parameters.Add(new SqlParameter("@ARGENT", depense.ARGENT));
            commande.Parameters.Add(new SqlParameter("@ID_GENS", depense.ID_GENS_QUI_DEPENSE));
            commande.Parameters.Add(new SqlParameter("@ID", depense.ID_DEPENSE));
            var nbLignes = (int)commande.ExecuteNonQuery();

            if (nbLignes != 1)
            {
                throw new Exception($"Impossible de mettre à jour la dépense avec l'ID {depense.ID_DEPENSE}");
            }

            dbClose();
            return depense;
        }
    }
}
