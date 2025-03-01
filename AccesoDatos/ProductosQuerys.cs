﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Policy;

namespace AccesoDatos
{
    public class ProductosQuerys
    {
        public List<Productos> GetProductos()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Productos> listado = new List<Productos>();
            listado = con.Query<Productos>("SELECT Codigo,Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange FROM Productos ORDER BY Nombre ASC").ToList();
            return listado;
        }

        public bool ExisteNombre(string nombre) //comprueba si existe el producto devolviendo la cantidad de veces que existe
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            var existe = con.Query<int>("SELECT COUNT(Nombre) FROM PRODUCTOS WHERE Nombre='" + nombre + "'");
            if (existe.First() > 0) //si la cantidad es mayor a 0, significa que si existe
            {
                return true; //si existe, devuelve true
            }
            else
            {
                return false; //si no existe, devuelve false
            }
        }

        public bool InsertProducto(Productos producto)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int nuevo = con.Execute("INSERT INTO Productos(Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange) VALUES(@Nombre,@Descripcion,@IdMarca,@PrecioUnitario,@Activo,@UrlImange)", 
                new {Nombre=producto.Nombre,Descripcion=producto.Descripcion,IdMarca=producto.IdMarca,PrecioUnitario=producto.PrecioUnitario,Activo=producto.Activo,UrlImange=producto.UrlImange});
            if(nuevo > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Productos> ProductoPorCodigo(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Productos> datosProducto = new List<Productos>();
            datosProducto = con.Query<Productos>("SELECT Codigo,Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange FROM Productos WHERE Codigo=@Codigo",
                new { Codigo = id }).ToList();
            return datosProducto;
        }

        public List<Productos> GetCincoNovedades()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Productos> listado = new List<Productos>();
            listado = con.Query<Productos>("SELECT TOP 5 Codigo,Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange FROM Productos ORDER BY Codigo DESC").ToList();
            return listado;
        }

        public List<Productos> GetProductoMayorPrecio()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            List<Productos> listado = new List<Productos>();
            listado = con.Query<Productos>("SELECT TOP 1 Codigo,Nombre,Descripcion,IdMarca,PrecioUnitario,Activo,UrlImange FROM Productos ORDER BY PrecioUnitario DESC").ToList();
            return listado;
        }

        public bool DeleteProducto(int Codigo) //elimina un producto de acuerdo al numero de Codigo
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int baja = con.Execute("DELETE FROM Productos WHERE Codigo=@Codigo",
                new { Codigo = Codigo });
            if (baja > 0) //si la cantidad es mayor a 0, significa que se eliminó
            {
                return true; //si se eliminó, devuelve true
            }
            else
            {
                return false; //si no se pudo eliminar, devuelve false
            }
        }

        public bool UpdateProducto(int codigo, string nombre, string descripcion, int idMarca, decimal precioUnitario, int activo, string urlImange)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionPracticoMVC"].ConnectionString);
            int edita = con.Execute("UPDATE Productos SET Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,PrecioUnitario=@PrecioUnitario,Activo=@Activo,UrlImange=@UrlImange WHERE Codigo=@Codigo",
                new { Codigo = codigo, Nombre = nombre, Descripcion = descripcion, IdMarca = idMarca, PrecioUnitario = precioUnitario, Activo = activo, UrlImange = urlImange });
            if (edita > 0) //si la cantidad es mayor a 0, significa que se modificó
            {
                return true; //si se modificó, devuelve true
            }
            else
            {
                return false; //si no se pudo modificar, devuelve false
            }
        }
    }
}