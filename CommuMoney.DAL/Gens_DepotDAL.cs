using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace CommuMoney.DAL
{
    public class Gens_DepotDAL : Depot_DAL<Gens_DAL>
    {
        public override void Delete(Gens_DAL GensADelete)
        {
            dbConnect();
            commande.CommandText = "update Gens set updated_at=GetDate() where id_gens=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", GensADelete.ID));

            var nbLignes = (int)commande.ExecuteNonQuery();

            if(nbLignes != 1)
            {
                throw new Exception($"Impossible de supprimer la personne avec l'ID {GensADelete.ID}");
            }
            dbClose();
        }

        public override List<Gens_DAL> GetAll()
        {
            dbConnect();

            commande.CommandText = "select id_gens, name, created_at from Gens";

            var reader = commande.ExecuteReader();
            var listeDeGens = new List<Gens_DAL>();

            while (reader.Read())
            {
                var gens = new Gens_DAL(reader.GetInt32(0),
                                       reader.GetDateTime(1),
                                       reader.GetString(2));

                listeDeGens.Add(gens);
            }

            dbClose();

            return listeDeGens;
        }

        public override Gens_DAL GetByID(int ID)
        {
            dbConnect();
            commande.CommandText = "select * from Gens where id_gens=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", ID));
            var reader = commande.ExecuteReader();

            Gens_DAL gens;

            if (reader.Read())
            {
                gens = new Gens_DAL(reader.GetInt32(0),//ID 
                    reader.GetDateTime(1),//Quand ça a été créé
                    reader.GetDateTime(2),//Quand ça a été update (Si ça a été update, sinon ça renvoie null)
                    reader.GetString(3));//Le nom
            }
            else
            {
                throw new Exception($"Aucune personne trouvé avec l'ID {ID}");
            }

            dbClose();
            return gens;
        }

        public override Gens_DAL Insert(Gens_DAL gensAInsert)//Je comprends juste pas cette fonction
        {
            dbConnect();

            commande.CommandText = "insert into Gens(created_at) values (GetDate()); select scope_identity()";
            var ID = Convert.ToInt32((decimal)commande.ExecuteScalar());

            gensAInsert.m_created_at = GetByID(ID).m_created_at;

            dbClose();

            return gensAInsert;

        }

        public override Gens_DAL Update(Gens_DAL gens)//Je comprends pas trop à quoi sert le update si on ne fait rien sur la table en question
        {

            //update le nom ? 
            // Dans tous les cas il faut mettre le updated_at à jour
            dbConnect();

            commande.CommandText = "update Gens set updated_at=getDate() where id_gens=@ID";
            commande.Parameters.Add(new SqlParameter("@ID", gens.ID));

            var nbLignes = (int)commande.ExecuteNonQuery();

            if (nbLignes != 1)
            {
                throw new Exception($"Impossible de mettre à jour la personne avec l'ID {gens.ID}");
            }
            gens.m_updated_at = GetByID(gens.ID).m_updated_at;

            dbClose();

            return gens;
        }
    }
}
