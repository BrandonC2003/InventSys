using InventSys.Domain.Entities;
using InventSys.Domain.Enums;
using InventSys.Domain.Interfaces;
using InventSys.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace InventSys.Infrastructure.Services
{
    public class UsuarioService(InventSysDbContext context) : IUsuarioService
    {
        private readonly InventSysDbContext _context = context;

        public async Task ActualizarUsuarioAsync(int userId, Usuarios usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(userId) ?? 
                throw new KeyNotFoundException("Usuario no encontrado");

            usuarioExistente.IdRol = usuario.IdRol;
            usuarioExistente.Password = usuario.Password;
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.CorreoElectronico = usuario.CorreoElectronico;
            usuarioExistente.Estado = usuario.Estado;
            usuarioExistente.Activo = usuario.Activo;

            _context.Usuarios.Update(usuarioExistente);

            await _context.SaveChangesAsync();
        }

        public async Task CambiarClaveAsync(int userId, string newPassword)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(userId) ??
                throw new KeyNotFoundException("Usuario no encontrado");

            usuarioExistente.Password = newPassword;

            _context.Usuarios.Update(usuarioExistente);

            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoAsync(int userId, UserStatus userStatus)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(userId) ??
                throw new KeyNotFoundException("Usuario no encontrado");

            usuarioExistente.Estado = (byte)userStatus;

            _context.Usuarios.Update(usuarioExistente);
        }

        public async Task CrearUsuarioAsync(Usuarios usuario)
        {
            var newUsuario = new Usuario()
            {
                IdRol = usuario.IdRol,
                UserName = usuario.UserName,
                Password = usuario.Password,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                CorreoElectronico = usuario.CorreoElectronico,
                Estado = usuario.Estado,
                Activo = usuario.Activo
            };

            _context.Usuarios.Add(newUsuario);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> HayUsuariosRegistrado()
        {
            var cantidadUsuarios = await (
                from u in _context.Usuarios
                select u
                ).CountAsync();

            return cantidadUsuarios > 0;
        }

        public async Task<Usuarios> ObtenerUsuarioAsync(int userId)
        {
            var usuario = await (
                from u in _context.Usuarios
                where u.IdUsuario == userId
                select new Usuarios
                {
                    IdUsuario = u.IdUsuario,
                    IdRol = u.IdRol,
                    UserName = u.UserName,
                    Password = u.Password,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    CorreoElectronico = u.CorreoElectronico,
                    Estado = u.Estado,
                    Activo = u.Activo
                }
                ).FirstOrDefaultAsync() ??
                throw new KeyNotFoundException("Usuario no encontrado");


            return usuario;
        }

        public async Task<Usuarios> ObtenerUsuarioAsync(string userName)
        {
            var usuario = await(
                from u in _context.Usuarios
                where u.UserName == userName
                select new Usuarios
                {
                    IdUsuario = u.IdUsuario,
                    IdRol = u.IdRol,
                    UserName = u.UserName,
                    Password = u.Password,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    CorreoElectronico = u.CorreoElectronico,
                    Estado = u.Estado,
                    Activo = u.Activo
                }
                ).FirstOrDefaultAsync() ??
                throw new KeyNotFoundException("Usuario no encontrado");


            return usuario;
        }

        public async Task<List<Usuarios>> ObtenerUsuariosAsync()
        {
            var usuarios = await (
                from u in _context.Usuarios
                select new Usuarios
                {
                    IdUsuario = u.IdUsuario,
                    IdRol = u.IdRol,
                    UserName = u.UserName,
                    Password = u.Password,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    CorreoElectronico = u.CorreoElectronico,
                    Estado = u.Estado,
                    Activo = u.Activo
                }
                ).ToListAsync();

            return usuarios;
        }
    }
}
