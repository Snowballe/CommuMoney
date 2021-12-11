using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuMoney.DAL
{
    public class Remboursement_DepotDAL : Depot_DAL<Remboursement_DAL>
    {
        public override void Delete(Remboursement_DAL remboursement)
        {
            dbConnect();
            commande.CommandText = "delete from Remboursement where id_remboursement=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", remboursement.ID_REMBOURSEMENT));
            var nbLigne = (int)commande.ExecuteNonQuery();

            if (nbLigne!=1)
            {
                throw new Exception($"Impossible de supprimer le point d'ID {remboursement.ID_REMBOURSEMENT}");
            }
            
            dbClose();
        }

      

        public override List<Remboursement_DAL> GetAll()
        {
            dbConnect();

            commande.CommandText = "select id_remboursement, id_argent_depense, id_gens_a_rembourser, created_at from Remboursement";
            var reader = commande.ExecuteReader();

            var listeDesRemboursements = new List<Remboursement_DAL>();
            while (reader.Read())
            {
                var dep = new Remboursement_DAL(reader.GetInt32(0),
                                            reader.GetInt32(1),
                                            reader.GetInt32(2),
                                            reader.GetDateTime(3));
                listeDesRemboursements.Add(dep);
            }

            dbClose();
            return listeDesRemboursements;

        }


        public override Remboursement_DAL GetByID(int ID)
        {
            dbConnect();
            commande.CommandText = "select id_remboursement, id_argent_depense, id_gens_a_rembourser, created_at from Remboursement where id_remboursement=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = commande.ExecuteReader();

            Remboursement_DAL dep;

            if (reader.Read())
            {
                dep = new Remboursement_DAL(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetInt32(2),
                                        reader.GetDateTime(3));
            }
            else
            {
                throw new Exception($"Pas de remboursement de disponible avec l'ID {ID}");
            }

            dbClose();
            return dep;
        }

        

        public override Remboursement_DAL Insert(Remboursement_DAL remboursement)
        {
            dbConnect();

            commande.CommandText = "insert into Remboursement(id_remboursement, id_argent_depense,id_gens_a_rembourser ) values (@ID_REM, @ID_ARGENT_DEP, @GENS_REM); select scope_identity()";
            commande.Parameters.Add(new SqlParameter("@ID_REM", remboursement.ID_REMBOURSEMENT));
            commande.Parameters.Add(new SqlParameter("@ID_ARGENT_DEP", remboursement.ID_ARGENT_DEP));
            commande.Parameters.Add(new SqlParameter("@GENS_REM", remboursement.ID_GENS_A_REMBOURSER));

            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            remboursement.ID_REMBOURSEMENT = ID;

            dbClose();

            return remboursement;
        }


        public override Remboursement_DAL Update(Remboursement_DAL remboursement)
        {
            dbConnect();

            commande.CommandText = "update Remboursement set id_argent_depense=@ARGENT, id_gens_a_rembourser=@ID_GENS, updated_at=getDate() where id_remboursement=@ID";
            commande.Parameters.Add(new SqlParameter("@ARGENT", remboursement.ID_ARGENT_DEP));
            commande.Parameters.Add(new SqlParameter("@ID_GENS", remboursement.ID_GENS_A_REMBOURSER));
            commande.Parameters.Add(new SqlParameter("@ID", remboursement.ID_REMBOURSEMENT));
            var nbLignes = (int)commande.ExecuteNonQuery();

            if (nbLignes != 1)
            {
                throw new Exception($"Impossible de mettre à jour la dépense avec l'ID {remboursement.ID_REMBOURSEMENT}");
            }

            dbClose();
            return remboursement;
        }
    }
}
