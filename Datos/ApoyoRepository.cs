using Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Datos
{
    
    public class ApoyoRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Apoyo> _apoyos = new List<Apoyo>();

        public ApoyoRepository (ConnectionManager connection){
            _connection = connection._conexion;
        }

        public void Guardar(Apoyo apoyo){
            using (var command = _connection.CreateCommand())
            {

                Persona persona = apoyo.Persona;

                command.CommandText = @"Insert Into Persona (Cedula,Nombre,Apellido,Sexo,Edad,Departamento,Ciudad) 
                                        values (@Cedula,@Nombre,@Apellido,@Sexo,@Edad,@Departamento,@Ciudad)";
                command.Parameters.AddWithValue("@Cedula", persona.Cedula);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Apellido", persona.Apellido);
                command.Parameters.AddWithValue("@Sexo", persona.Sexo);
                command.Parameters.AddWithValue("@Edad", persona.Edad);
                command.Parameters.AddWithValue("@Departamento", persona.Departamento);
                command.Parameters.AddWithValue("@Ciudad", persona.Ciudad);
                var filas = command.ExecuteNonQuery();
                GuardarApoyo(apoyo);
                
            }
        }

        public void GuardarApoyo(Apoyo apoyo){
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Apoyo (FKCedula,Tipoapoyo,Vrapoyo) 
                                        values (@FKCedula,@Tipoapoyo,@Vrapoyo)";
                command.Parameters.AddWithValue("@FKCedula", apoyo.Persona.Cedula);
                command.Parameters.AddWithValue("@Tipoapoyo", apoyo.Tipoapoyo);
                command.Parameters.AddWithValue("@Vrapoyo", apoyo.Vrapoyo);
                var filas = command.ExecuteNonQuery();
                
            }
        }

        public List<Apoyo> ConsultarTodos()
        {
            SqlDataReader dataReader;
            List<Apoyo> apoyos = new List<Apoyo>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from Apoyo";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Apoyo apoyo = MapearApoyo(dataReader);
                        apoyos.Add(apoyo);
                    }
                }
            }
            return apoyos;
        }

        public Apoyo MapearApoyo(SqlDataReader dataReader){
            if(!dataReader.HasRows) return null;
            string cedula = (string)dataReader["FKCedula"];
            Apoyo apoyo = new Apoyo();
            apoyo.Persona = BuscarPorIdentificacion(cedula);
            apoyo.Tipoapoyo = (string)dataReader["Tipoapoyo"];
            apoyo.Vrapoyo = (decimal)dataReader["Vrapoyo"];
            return apoyo;

        }

        public Persona BuscarPorIdentificacion(string cedula)
        {
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select * from Persona where Cedula=@Cedula";
                command.Parameters.AddWithValue("@Cedula", cedula);
                dataReader = command.ExecuteReader();
                dataReader.Read();
                return MapearPersona(dataReader);
            }
        }

        public Persona MapearPersona(SqlDataReader dataReader){
           if(!dataReader.HasRows) return null;
           Persona persona = new Persona();
           persona.Cedula = (string)dataReader["Cedula"]; 
           persona.Nombre = (string)dataReader["Nombre"]; 
           persona.Apellido = (string)dataReader["Apellido"]; 
           persona.Sexo = (string)dataReader["Sexo"]; 
           persona.Edad = (int)dataReader["Edad"]; 
           persona.Departamento = (string)dataReader["Departamento"]; 
           persona.Ciudad = (string)dataReader["Ciudad"]; 
           return persona; 
            
        }

        
    }
}
