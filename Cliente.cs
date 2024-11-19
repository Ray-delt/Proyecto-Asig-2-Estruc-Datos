using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_teorica
{
    internal class Cliente
    {
        private string _nombre;
        private string _apellido;
        private string _ci; // CI: Cédula de Identidad
        private string _tipoVehiculo; // Puede ser "camioneta" o "auto"
        private string _modeloVehiculo;
        private string _placa;
        private int _menb;


        public Cliente(string nombre, string apellido, string ci, string tipoVehiculo, string modeloVehiculo, string placa, int menbresia)
        {
            _nombre = nombre;
            _apellido = apellido;
            _ci = ci;
            _tipoVehiculo = tipoVehiculo;
            _modeloVehiculo = modeloVehiculo;
            _placa = placa;
            _menb = menbresia;
        }
        public int ObtenerMen()
        {
            return _menb;
        } 
        public string ObtenerVeh()
        {
            return _modeloVehiculo;
        }
        public override string ToString()
        {
            return $"Nombre: {_nombre}, Apellido: {_apellido}, CI: {_ci}, Tipo de Vehículo: {_tipoVehiculo}, Modelo: {_modeloVehiculo}, Placa: {_placa}, N de menbresia: {_menb}";
        }
    }
}
